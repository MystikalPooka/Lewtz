using ItemRoller.Data_Structure;

namespace ItemRoller.Loaders
{
    public interface IDataMapper
    {
        Table LoadTableFromFile(string filename);
    }
}
