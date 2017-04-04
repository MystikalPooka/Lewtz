using ItemRoller.Data_Structure;

namespace ItemRoller.Repository
{
    public interface IDataMapper
    {
        void Save(string filename);
        Table Load(string filename);
    }
}
