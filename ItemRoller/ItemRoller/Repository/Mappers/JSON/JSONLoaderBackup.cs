using System;
using System.Collections.Generic;
using System.Linq;
using LewtzTesting.Data_Structure;
using System.IO;
using Newtonsoft.Json.Linq;

namespace LewtzTesting.Loaders.JSON
{
    class JSONLoaderBackup
    {
        private string _filename;
        private Table tableToLoad;
        public void LoadTableFromFile(string filename, Table loadTable)
        {
            try
            {
                var json = File.ReadAllText(filename);
                JToken token = JToken.Parse(json);

                if (loadTable == null)
                {
                    loadTable = new Table(getNameFromFilename(filename));
                    tableToLoad = null;
                    _filename = null;
                }

                if (tableToLoad == null)
                {
                    tableToLoad = loadTable;
                }

                if (_filename == null)
                {
                    _filename = filename;
                }

                foreach (JObject obj in token)
                {
                    var probabilityList = getProbabilityListFromNode(obj);
                    foreach (JProperty prob in probabilityList)
                    {
                        Component loadedComponent = loadComponent(obj, prob); ;
                        if (loadedComponent == null) break;

                        var types = getProbabilityTypesFromJProperty(prob);
                        if ((types & tableToLoad.Types) != 0 || tableToLoad.Types == ItemTypes.None)
                        {
                            tableToLoad.Add(loadedComponent);
                            tableToLoad.SortTable();
                            TableRepository.AddTable(tableToLoad);
                        }
                    }
                }
            }
            catch(FileNotFoundException)
            {
                Console.WriteLine("File Not Found: " + filename);
            }
            loadTable.SortTable();
        }

        private Component loadComponent(JObject obj, JProperty probability)
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
                    compToAdd.Types |= getProbabilityTypesFromJProperty(probability);
                    break;
                case "table":
                    compToAdd = obj.ToObject<Table>();
                    compToAdd.Types |= getProbabilityTypesFromJProperty(probability);
                    compToAdd.Name = compToAdd.Name.Replace(", roll again", "");
                    
                    string newFilename = _filename.Replace(tableToLoad.Name.ToLower(), compToAdd.Name);
                    //new JSONLoader().LoadTableFromFile(newFilename, compToAdd as Table);
                    break;
                case "ability":
                    compToAdd = obj.ToObject<Ability>();
                    compToAdd.Types |= getProbabilityTypesFromJProperty(probability);
                    break;
                default:
                    Console.WriteLine("INCORRECT TYPE: " + obj.Value<string>("type"));
                    compToAdd = obj.ToObject<MundaneItem>();
                    break;
            }
            //compToAdd.Name += probability.Name.Replace("probability", "");
            compToAdd.Probability = (int)probability.Value;
            compToAdd.ParentTable = tableToLoad;
            return compToAdd;
        }

        private ItemTypes getProbabilityTypesFromJProperty(JProperty prop)
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

        private List<JProperty> getProbabilityListFromNode(JToken node)
        {
            var probabilities =
                   from p in node.Children<JProperty>()
                   where p.Name.StartsWith("probability")
                   select p;
            return probabilities.ToList();
        }
    }
}