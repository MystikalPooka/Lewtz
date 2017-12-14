using ItemRoller.Loaders;

namespace ItemRoller.Data_Structure
{
    public interface IDatabase
    {
        void AddTable(Table table);
        Table GetTableFromString(string tableName);
        Table GetTypeSortedTableFromString(string tableName, ItemTypes types = ItemTypes.None);
        void LoadAllMatchingStringFromDirectory(string directory, string searchString, IDatabaseLoader loader);
        void LoadSingleFile(string filename, IDatabaseLoader loader);
    }
}