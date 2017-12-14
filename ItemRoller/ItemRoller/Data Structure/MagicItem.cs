using System;
using System.Collections.Generic;
using ItemRoller.Visitors;
using System.Linq;

namespace ItemRoller.Data_Structure
{
    public class MagicItem : Item
    {
        private List<Component> appliedAbilities;
        public IReadOnlyList<Component> Abilities => appliedAbilities as IReadOnlyList<Component>;

        protected MagicItem(MagicItem item)
        {
            appliedAbilities = new List<Component>();

            Types |= item.Types;
            Cost = item.Cost;
            Probability = item.Probability;
        }

        public MagicItem()
        {
            appliedAbilities = new List<Component>();
            Types |= ItemTypes.Magic;
        }

        public void SetItemTypesFromAppliedAbilities()
        {
            if(appliedAbilities.Count() > 0)
            {
                var lastAbility = appliedAbilities.Last();
                ItemTypes baseItemTypes = lastAbility.Types;
                Types |= (baseItemTypes & ~ItemTypes.Ability);

                Book = lastAbility.Book;
                Name = lastAbility.Name;
            }
        }

        public void AddAbilities(ICollection<Component> abilities)
        {
            appliedAbilities.AddRange(abilities);
        }

        public void AddAbility(Component ability)
        {
            appliedAbilities.Add(ability);
        }

        public void RemoveAbilities(Predicate<Component> removePred)
        {
            appliedAbilities.RemoveAll(removePred);
        }

        public void RemoveAbility(Component ability)
        {
            appliedAbilities.Remove(ability);
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override object Clone()
        {
            return new MagicItem(this);
        }

        public override string ToString()
        {
            var abilityString = appliedAbilities.Count > 0 ? "\r\n-  Abilities:  - \r\n" + GetAbilityNames() : "";
            return base.ToString() + " " + Types.ToString() + abilityString;
        }

        private string GetAbilityNames()
        {
            string abilities = "";
            foreach(Component ability in appliedAbilities)
            {
                abilities += " -> " + ability + "\r\n";
            }
            return abilities;
        }
    }
}