using ItemRoller.Loaders;
using System.Collections.Concurrent;
using System.IO;

namespace ItemRoller.Data_Structure
{
    public class TableDatabase : IDatabase
    {
        private ConcurrentDictionary<string, Table> tableDict = new ConcurrentDictionary<string, Table>();

        /// <summary>
        /// Get a table by name. 
        /// Returns a new Table with name "Table Not Found" if no table is found in the repository
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns>Table by name or Table("Table Not Found") if not found</returns>
        public Table GetTableFromString(string tableName)
        {
            if (tableDict.ContainsKey(tableName))
            {
                return (Table)tableDict[tableName].Clone();
            }
            else return new Table("Table Not Found");
        }

        /// <summary>
        /// Get a table from the repository that has all components of specified type removed
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        public Table GetTypeSortedTableFromString(string tableName, ItemTypes types = ItemTypes.None)
        {
            var table = GetTableFromString(tableName);
            table.RemoveChildrenNotMatchingTypes(types);
            return table;
        }

        public void AddTable(Table table)
        {
            tableDict.AddOrUpdate(table.Name, table, (k, v) => table);
        }

        public void LoadAllMatchingStringFromDirectory(string directory, string searchString, IDatabaseLoader loader)
        {
            foreach (string filename in Directory.EnumerateFiles(directory, searchString))
            {
                LoadSingleFile(filename, loader);
            }
        }

        public void LoadSingleFile(string filename, IDatabaseLoader loader)
        {
            AddTable(loader.LoadTableFromFile(filename));
        }
    }
}
