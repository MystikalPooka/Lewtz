using ItemRoller.Data_Structure;
using System;

namespace ItemRoller.Visitors
{
    public class PrintEntireTreeVisitor : IVisitor
    {
        public void Visit(Table table)
        {
            print(table, "", true);
        }

        private void print(Component comp, string indent, bool last)
        {
            Console.Write(indent);
            if (last)
            {
                Console.Write("\\-");
                indent += "  ";
            }
            else
            {
                Console.Write("|-");
                indent += "|  ";
            }
            Console.WriteLine(comp);

            if(comp is Table)
            {
                var children = ((Table)comp).GetChildren();
                for (int i = 0; i < children.Count; i++)
                    print(children[i], indent, i == children.Count - 1);
            }
        }

        public void Visit(MundaneItem item)
        {
        }

        public void Visit(MagicItem item)
        {
        }

        public void Visit(Ability ability)
        {
        }
    }
}