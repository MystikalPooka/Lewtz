using ItemRoller.Data_Structure;

namespace ItemRoller.Repository
{
    public interface IDataMapper
    {
        Table LoadTableFromFile(string filename);
    }
}
