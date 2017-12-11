using ItemRoller.Data_Structure;

namespace ItemRoller.Visitors
{
    public interface IVisitor
    {
        void Visit(Table table);
        void Visit(MundaneItem item);
        void Visit(MagicItem item);
        void Visit(Ability ability);
    }
}