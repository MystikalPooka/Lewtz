using ItemRoller.Data_Structure;

namespace ItemRoller.Loaders
{
    public interface IDatabaseLoader
    {
        Table LoadTableFromFile(string filename);
    }
}
