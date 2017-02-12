using ItemRoller.Visitors;
using System.Collections.Generic;
using System.Linq;

namespace ItemRoller.Data_Structure
{
    public class Table : Component
    {
        private List<Component> children;

        public int RollCount { get; set; }

        public Table()
        {
            Name = "Not Named";
            Probability = 0;
            Book = "";
            RollCount = 1;
            children = new List<Component>();
        }

        public Table(string name, int prob = 0, string book = "", int rollCount = 1)
        {
            Name = name;
            Probability = prob;
            Book = book;
            children = new List<Component>();
            RollCount = rollCount;
        }

        protected Table(Table copyTable)
        {
            Name = copyTable.Name;
            Probability = copyTable.Probability;
            Book = copyTable.Book;
            RollCount = copyTable.RollCount;
            ParentTable = copyTable.ParentTable;
            children = copyTable.children;
        }

        public void Add(Component comp)
        {
            children.Add(comp);
        }

        public void Remove(Component comp)
        {
            children.Remove(comp);
        }

        public IList<Component> GetChildren()
        {
            return children.AsReadOnly();
        }

        public void SetChildren(List<Component> children)
        {
            this.children = children;
        }

        public void SortTable()
        {
            children.Sort((x, y) => x.Probability.CompareTo(y.Probability));
        }

        public void RemoveChildrenNotMatchingTypes(ItemTypes type)
        {
            children = children.Where(comp => (comp.Types & type) > 0).ToList();
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return base.ToString() + " | # Entries: " + children.Count;
        }

        public override object Clone()
        {
            return new Table(this);
        }
    }
}