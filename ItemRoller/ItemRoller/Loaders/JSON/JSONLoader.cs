using ItemRoller.Data_Structure;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
                    foreach (JProperty prob in probabilityList)
                    {
                        Component loadedComponent = loadComponent(obj); ;
                        if (loadedComponent == null) break;

                        var types = probabilityTypes(prob);
                        loadedComponent.Types |= types;
                        if ((types & tableToLoad.Types) != 0 || tableToLoad.Types == ItemTypes.None)
                        {
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
                    //new JSONLoader().LoadTableFromFile(newFilename, compToAdd as Table);
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