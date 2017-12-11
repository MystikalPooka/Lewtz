using ItemRoller.Data_Structure;
using ItemRoller.Loaders;
using ItemRoller.Visitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LewtzGUI.Data_Access
{
    public class RepositoryLoader
    {
        public void LoadAllTables()
        {
            TableRepository.LoadSingleFile(@"..\..\..\Tables\treasure table.json", new JSONLoader());
            TableRepository.LoadSingleFile(@"..\..\..\Tables\magic base.json", new JSONLoader());
            TableRepository.LoadAllMatchingStringFromDirectory(@"..\..\..\Tables", @"*special abilities*", new JSONLoader());
            var baseTable = TableRepository.GetTableFromString("treasure table");

            TableRepository.GetTableFromString("armor special abilities").Accept(new PrintEntireTreeVisitor());
            TableRepository.GetTableFromString("shield special abilities").Accept(new PrintEntireTreeVisitor());
        }
    }
}
