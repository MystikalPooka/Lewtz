using ItemRoller.Data_Structure;

namespace ItemRoller.Visitors
{
    public class BuildItemVisitor : IVisitor
    {
        private IDatabase buildDBContext;
        public BuildItemVisitor(IDatabase database)
        {
            buildDBContext = database;
        }

        public void Visit(Table table)
        {
        }

        public void Visit(MundaneItem item)
        {
        }

        public void Visit(MagicItem item)
        {
            if (null == item || null == buildDBContext) return;
            var builder = new MagicItemBuilder(buildDBContext);
            builder.Build(item);
        }

        public void Visit(Ability ability)
        {
        }
    }
}
