using ItemRoller.Loaders;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemRoller.Data_Structure
{
    public class TableRepository
    {
        public ConcurrentDictionary<string, Table> tableDict = new ConcurrentDictionary<string, Table>();

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
            return GetTables().Where(p => p.Name.Equals(name)).First();
        }
    }
}
