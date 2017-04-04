using ItemRoller.Data_Structure;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ItemRoller.Repository
{
    public class TableRepository : IRepository
    {
        private ConcurrentDictionary<string, Table> tableDict = new ConcurrentDictionary<string, Table>();

        public void LoadAllMatchingStringFromDirectory(string directory, string searchString, IDataMapper loader)
        {
            foreach (string filename in Directory.EnumerateFiles(directory, searchString))
            {
                LoadSingleFile(filename, loader);
            }
        }

        public void LoadSingleFile(string filename, IDataMapper loader)
        {
            AddTable(loader.Load(filename));
        }

        public void AddTable(Table table)
        {
            tableDict.AddOrUpdate(table.Name, table, (k, v) => table);
        }

        public IEnumerable<Table> GetTables()
        {
            return tableDict.Values.AsEnumerable();
        }

        public ICollection<Table> GetTableList()
        {
            return tableDict.Values;
        }

        public Table GetTableByName(string name)
        {
            if (tableDict.ContainsKey(name))
            {
                return (Table)tableDict[name].Clone();
            }
            else return new Table("Table Not Found");
        }

        public Table GetTypeSortedTableByName(string tableName, ItemTypes types)
        {
            var table = GetTableByName(tableName);
            table.RemoveChildrenNotMatchingTypes(types);
            return table;
        }
    }
}
