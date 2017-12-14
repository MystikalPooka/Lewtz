using ItemRoller.Visitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemRoller.Data_Structure
{
    class MagicItemBuilder : IItemBuilder
    {
        private IDatabase DBContext;

        public MagicItemBuilder(IDatabase db)
        {
            DBContext = db;
        }

        public bool Build(Item item)
        {
            RollAllAbilities(item);
            (item as MagicItem).SetItemTypesFromAppliedAbilities();
            RollSpecialAbilities(item);
            return true;
        }

        private void RollAllAbilities(Item item)
        {
            var abilityRollTable = DBContext.GetTypeSortedTableFromString("magic base", item.Types);
            if (abilityRollTable.Name != "Table Not Found" && abilityRollTable != null)
            {
                var abilitiesVisitor = new GetLootVisitor();
                abilityRollTable.Accept(abilitiesVisitor);

                abilityRollTable.RollCount = 1;
               (item as MagicItem).AddAbilities(abilitiesVisitor.GetLootBag());
            }
        }

        private void RollSpecialAbilities(Item item)
        {
            var abilitiesToRoll =
                                from ability in (item as MagicItem).Abilities
                                where ability.Name.ToLower().Contains("special abilities")
                                select ability;


            var abilitiesToAdd = new List<Component>();
            foreach (Component ability in abilitiesToRoll)
            {
                var typeToRoll = item.Types & ~(ItemTypes.Magic | ItemTypes.Magic_Major | ItemTypes.Magic_Medium | ItemTypes.Magic_Minor);
                var typeString = typeToRoll.ToString().ToLower() + " special abilities";
                var specialRollTable = DBContext.GetTypeSortedTableFromString(typeString, item.Types);

                if (specialRollTable != null)
                {
                    var abilities = new GetLootVisitor();
                    specialRollTable.Accept(abilities);
                    abilitiesToAdd.AddRange(abilities.GetLootBag());
                }
            }
            (item as MagicItem).AddAbilities(abilitiesToAdd);
            (item as MagicItem).RemoveAbilities(k => k.Name.ToLower().Contains("special abilities"));
        }
    }
}
