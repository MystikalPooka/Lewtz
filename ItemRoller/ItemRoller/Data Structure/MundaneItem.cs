using ItemRoller.Visitors;
using System;

namespace ItemRoller.Data_Structure
{
    public class MundaneItem : Item
    {
        public MundaneItem()
        {
            Name = "Unnamed MundaneItem";
            Probability = 0;
            Book = "";
        }

        public MundaneItem(string name, int prob = 0, int cost = 0, string book = "")
        {
            Name = name;
            Probability = prob;
            Cost = cost;
            Book = book;
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override object Clone()
        {
            throw new NotImplementedException();
        }
    }
}