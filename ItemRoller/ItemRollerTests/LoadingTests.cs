using ItemRoller.Data_Structure;
using ItemRoller.Loaders;
using ItemRoller.Visitors;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ItemRollerTests
{
    [TestClass]
    public class LoadingTests
    {
        Table baseTable;

        [TestInitialize]
        public void TestInitialize()
        {
            TableRepository.LoadSingleFile(@"..\..\..\Tables\treasure table.json", new JSONLoader());
            TableRepository.LoadSingleFile(@"..\..\..\Tables\magic base.json", new JSONLoader());
            TableRepository.LoadAllMatchingStringFromDirectory(@"..\..\..\Tables", @"*special abilities*", new JSONLoader());

            baseTable = TableRepository.GetTableFromString("treasure table");
        }

        [TestMethod]
        public void BaseTableIsNotNull()
        {
            Assert.IsNotNull(baseTable);
        }

        [TestMethod]
        public void RollingLootReturnsItems()
        {
            var lootBag = new LootVisitor();
            Assert.IsNotNull(lootBag);
            
        }
    }
}
