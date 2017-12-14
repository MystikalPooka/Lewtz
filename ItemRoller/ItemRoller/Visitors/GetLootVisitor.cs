using System;
using System.Collections.Generic;
using ItemRoller.Data_Structure;
using System.Linq;

namespace ItemRoller.Visitors
{
    class GetLootVisitor : IVisitor
    {
        private IList<Component> lootBag;
        public List<Component> GetLootBag()
        {
            return (List<Component>)lootBag;
        }

        private static Random rand = new Random();
        public GetLootVisitor()
        {
            lootBag = new List<Component>();
        }

        public void Visit(Table table)
        {
            var children = table.GetChildren();

            if(children.Count != 0)
            {
                int maxProb = children.Max(x => x.Probability);
                int rolls = table.RollCount;
                for (int i = 0; i < rolls; ++i)
                {
                    int roll = rand.Next(0, maxProb);
                    foreach(Component comp in children)
                    {
                        if (comp.Probability >= roll)
                        {
                            comp.Accept(this);
                            if (comp.Name.ToLower().Contains("roll again"))
                            {
                                ++rolls;
                                continue;
                            }
                            else break;
                        }
                    }
                }
            }
            else
            {
                lootBag.Add(new MundaneItem(table.Name));
            }
        }

        public void Visit(Ability ability)
        {
            lootBag.Add(ability);
        }

        public void Visit(MagicItem item)
        {
            var itemToBuild = item.Clone() as MagicItem;
            //NEEDS TO BE BUILT ON THE OUTSIDE.
            lootBag.Add(itemToBuild);
        }

        public void Visit(MundaneItem item)
        {
            if(item.Name.ToLower() != "nothing")
            {
                lootBag.Add(item);
            }
        }
    }
}
