using System;
using System.Collections.Generic;
using ItemRoller.Visitors;
using System.Linq;

namespace ItemRoller.Data_Structure
{
    public class MagicItem : Item
    {
        private List<Component> appliedAbilities;

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

        public void Build()
        {
            RollAllAbilities();
            SetItemTypesFromAppliedAbilities();
            RollSpecialAbilities();
        }

        private void RollAllAbilities()
        {
            var buildTable = TableRepository.GetTypeSortedTableFromString("magic base", Types);
            if (buildTable.Name != "Table Not Found" && buildTable != null)
            {
                var abilitiesVisitor = new LootVisitor();
                buildTable.Accept(abilitiesVisitor);

                buildTable.RollCount = 1;
                appliedAbilities.AddRange(abilitiesVisitor.GetLootBag());
            }
        }

        private void SetItemTypesFromAppliedAbilities()
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

        private void RollSpecialAbilities()
        {
            var abilitiesToRoll =
                                from ability in appliedAbilities
                                where ability.Name.ToLower().Contains("special abilities")
                                select ability;


            var abilitiesToAdd = new List<Component>();
            foreach(Component ability in abilitiesToRoll)
            {
                var typeToRoll = Types & ~(ItemTypes.Magic | ItemTypes.Magic_Major | ItemTypes.Magic_Medium | ItemTypes.Magic_Minor);
                var typeString = typeToRoll.ToString().ToLower() + " special abilities";
                var rollTable = TableRepository.GetTypeSortedTableFromString(typeString, Types);

                if(rollTable != null)
                {
                    var abilities = new LootVisitor();
                    rollTable.Accept(abilities);
                    abilitiesToAdd.AddRange(abilities.GetLootBag());
                }
            }
            appliedAbilities.AddRange(abilitiesToAdd);
            appliedAbilities.RemoveAll(k => k.Name.ToLower().Contains("special abilities"));
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