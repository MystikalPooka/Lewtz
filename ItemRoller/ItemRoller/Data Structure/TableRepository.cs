using ItemRoller.Loaders;
using System.IO;
using System.Collections.Concurrent;

namespace ItemRoller.Data_Structure
{
    public static class TableRepository
    {
        private static ConcurrentDictionary<string, Table> tableDict = new ConcurrentDictionary<string, Table>();

        public static Table GetTableFromString(string tableName)
        {
            if(tableDict.ContainsKey(tableName))
            {
                return (Table)tableDict[tableName].Clone();
            }
            else return new Table("Table Not Found");
        }

        public static Table GetTypeSortedTableFromString(string tableName, ItemTypes types = ItemTypes.None)
        {
            var table = GetTableFromString(tableName);
            table.RemoveChildrenNotMatchingTypes(types);
            return table;
        }

        public static void AddTable(Table table)
        {
            tableDict.AddOrUpdate(table.Name, table, (k,v) => table);
        }
        
        public static void LoadAllMatchingStringFromDirectory(string directory, string searchString, IDatabaseLoader loader)
        {
            foreach (string filename in Directory.EnumerateFiles(directory, searchString))
            {
                LoadSingleFile(filename, loader);
            }
        }

        public static void LoadSingleFile(string filename, IDatabaseLoader loader)
        {
            AddTable(loader.LoadTableFromFile(filename));
        }
    }
}