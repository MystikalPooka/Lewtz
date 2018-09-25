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
            TableDatabase db = new TableDatabase();

            db.LoadSingleFile(@"..\..\..\Tables\treasure table.json", new JSONLoader());

            var baseTable = db.GetTableFromString("treasure table");
            /*** TODO: FIX JSON TO HAVE INTERNAL ARRAYS RATHER THAN MULTIPLE FIELDS
              *(IE. "Probabilities": [
              * "minor":25,
              * "medium": 27,
              * "major":45
              * ]
            ***/
            Assert.IsNotNull(baseTable);
        }
    }
}
