using ItemRoller.Data_Structure;
using ItemRoller.Loaders;
using ItemRoller.Visitors;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ItemRollerTests
{
    [TestClass]
    public class LoadingTests
    {
        [TestMethod]
        public void TestLoad()
        {
            TableRepository.LoadSingleFile(@"..\..\Tables\treasure table.json", new JSONLoader());
            TableRepository.LoadSingleFile(@"..\..\Tables\magic base.json", new JSONLoader());
            TableRepository.LoadAllMatchingStringFromDirectory(@"..\..\Tables", @"*special abilities*", new JSONLoader());
            var baseTable = TableRepository.GetTableFromString("treasure table");

            TableRepository.GetTableFromString("armor special abilities").Accept(new PrintEntireTreeVisitor());
            TableRepository.GetTableFromString("shield special abilities").Accept(new PrintEntireTreeVisitor());


            Assert.IsNotNull(baseTable);
        }
    }
}
