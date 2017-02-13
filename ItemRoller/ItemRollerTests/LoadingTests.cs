using ItemRoller.Data_Structure;
using ItemRoller.Loaders;
using ItemRoller.Visitors;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ItemRollerTests
{
    [TestClass]
    public class LoadingTests
    {
        TableRepository BaseRepo;

        [TestInitialize]
        public void TestInitialize()
        {
            BaseRepo = new TableRepository();
            var loader = new JSONLoader();

            BaseRepo.LoadSingleFile(@"..\..\..\Tables\treasure table.json", loader);
            BaseRepo.LoadSingleFile(@"..\..\..\Tables\magic base.json", loader);
            BaseRepo.LoadAllMatchingStringFromDirectory(@"..\..\..\Tables", @"*special abilities*", loader);
        }

        [TestMethod]
        public void RepositoryIsNotEmpty()
        {
            var tables = BaseRepo.GetTables();
            Assert.IsNotNull(tables);
        }

        [TestMethod]
        public void RollingLootReturnsItems()
        {
            var lootBag = new LootVisitor(BaseRepo);
            Assert.IsNotNull(lootBag);
            CollectionAssert.AllItemsAreNotNull(lootBag.GetLootBag());
        }
    }
}
