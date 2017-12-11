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
        public void LoadTables()
        {
            TableRepository.LoadSingleFile(@"..\..\..\Tables\treasure table.json", new JSONLoader());
            TableRepository.LoadSingleFile(@"..\..\..\Tables\magic base.json", new JSONLoader());
            TableRepository.LoadAllMatchingStringFromDirectory(@"..\..\..\Tables", @"*special abilities*", new JSONLoader());

            var baseTable = TableRepository.GetTableFromString("treasure table");
            Assert.IsNotNull(baseTable);
        }
    }
}
