using ItemRoller.Loaders;
using System.IO;
using System.Collections.Concurrent;
using System;
using ItemRoller.Data_Structure;

namespace LewtzGUI.Data_Access
{
    public class TableRepository
    {
        public TableDatabase DatabaseContext;
        public TableRepository()
        {
            DatabaseContext = new TableDatabase();
            DatabaseContext.LoadSingleFile(@"..\..\..\ItemRoller\Tables\treasure table.json", new JSONLoader());
            DatabaseContext.LoadSingleFile(@"..\..\..\ItemRoller\Tables\magic base.json", new JSONLoader());
            DatabaseContext.LoadAllMatchingStringFromDirectory(@"..\..\..\ItemRoller\Tables", @"*special abilities*", new JSONLoader());
            //var baseTable = DatabaseContext.GetTableFromString("treasure table");
        }

    }
}