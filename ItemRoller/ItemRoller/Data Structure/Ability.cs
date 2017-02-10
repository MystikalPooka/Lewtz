using System;
using ItemRoller.Visitors;

namespace ItemRoller.Data_Structure
{
    public class Ability : Component
    {
        public int Cost { get; set; }
        public int Bonus { get; set; }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override object Clone()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return Name + "  (Bonus: +" + Bonus + " | Cost: " + Cost + ") - " + Book;
        }
    }
}
