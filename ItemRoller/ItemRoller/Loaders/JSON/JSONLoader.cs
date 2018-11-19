using System;
using System.Collections.Generic;
using System.Linq;
using ItemRoller.Data_Structure;
using System.IO;
using Newtonsoft.Json.Linq;

namespace ItemRoller.Loaders
{
    public class JSONLoader : IDatabaseLoader
    {
        private string _filename;
        private Table tableToLoad;
        public Table LoadTableFromFile(string filename)
        {
            try
            {
                var json = File.ReadAllText(filename);
                JToken token = JToken.Parse(json);

                tableToLoad = new Table(getNameFromFilename(filename));
                _filename = filename;

                foreach (JObject obj in token)
                {
                    var probabilityList = probabilitiesFromNode(obj);
                    JObject objCopy = obj;
                    foreach (JProperty prob in probabilityList)
                    {
                        //objCopy.Add("probability", prob.Value);
                        Component loadedComponent = loadComponent(objCopy);
                        if (loadedComponent == null) break;
 
                        var types = probabilityTypes(prob);
                        loadedComponent.Types |= types;
                        var parentTypes = tableToLoad.Types;

                        if ((types & parentTypes) != 0 || tableToLoad.Types == ItemTypes.None)
                        {
                            //if the parent table has a magic type, 
                            //it should be matched to the loaded component's type

                            loadedComponent.ParentTable = tableToLoad;
                            loadedComponent.Probability = (int)prob.Value;

                            tableToLoad.Add(loadedComponent);
                            tableToLoad.SortTable();
                        }
                    }
                }
            }
            catch(FileNotFoundException)
            {
                Console.WriteLine("File Not Found: " + filename);
            }
            return tableToLoad;
        }

        private Component loadComponent(JObject obj)
        {
            Component compToAdd = null;

            var type = obj.GetValue("type").ToString().ToLower();
            switch (type)
            {
                case "item":
                    compToAdd = obj.ToObject<MundaneItem>();
                    break;
                case "magic item":
                    compToAdd = obj.ToObject<MagicItem>();
                    break;
                case "ability":
                    compToAdd = obj.ToObject<Ability>();
                    break;
                case "table":
                    compToAdd = obj.ToObject<Table>();
                    var tableName = compToAdd.Name.Replace(", roll again", "");
                    string newFilename = _filename.Replace(tableToLoad.Name.ToLower(), tableName);
                    compToAdd = new JSONLoader().LoadTableFromFile(newFilename);
                    if(compToAdd == null)
                    {
                        Console.WriteLine("ERROR: Table is null!" + tableName);
                    }
                    else compToAdd.Name = tableName;
                    break;
                default:
                    Console.WriteLine("INCORRECT TYPE: " + obj.Value<string>("type"));
                    compToAdd = obj.ToObject<MundaneItem>();
                    break;
            }

            return compToAdd;
        }

        private ItemTypes probabilityTypes(JProperty prop)
        {
            var probabilityName = prop.Name.Replace("probability", "");
            var probabilityType = "";

            probabilityType = "magic" + probabilityName;
            return (ItemTypes)Enum.Parse(typeof(ItemTypes), probabilityType, true);
        }

        private string getNameFromFilename(string filename)
        {
            int indexOfLastSlash = filename.LastIndexOf(@"\");
            int indexOfPeriod = filename.LastIndexOf(".");
            int length = indexOfPeriod - indexOfLastSlash - 1;
            var name = filename.Substring(indexOfLastSlash + 1, length);

            return name;
        }

        private List<JProperty> probabilitiesFromNode(JToken node)
        {
            var probabilities =
                   from p in node.Children<JProperty>()
                   where p.Name.StartsWith("probability")
                   select p;
            return probabilities.ToList();
        }
    }
}