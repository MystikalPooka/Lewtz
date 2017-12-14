using ItemRoller.Data_Structure;
using ItemRoller.Loaders;
using ItemRoller.Visitors;
using System;

namespace ItemRoller
{
    class Program
    {
        static void Main(string[] args)
        {
            TableDatabase database = new TableDatabase();
            database.LoadSingleFile(@"..\..\..\Tables\treasure table.json", new JSONLoader());
            database.LoadSingleFile(@"..\..\..\Tables\magic base.json", new JSONLoader());
            database.LoadAllMatchingStringFromDirectory(@"..\..\..\Tables", @"*special abilities*", new JSONLoader());
            var baseTable = database.GetTableFromString("treasure table");

            database.GetTableFromString("armor special abilities").Accept(new PrintEntireTreeVisitor());
            database.GetTableFromString("shield special abilities").Accept(new PrintEntireTreeVisitor());

            baseTable.Accept(new PrintEntireTreeVisitor());
            Console.WriteLine("\r\n===================\r\n");

            baseTable.RollCount = 16;

            Console.WriteLine("\r\n===================\r\n");
            
            var loot = new GetLootVisitor();
            baseTable.Accept(loot);

            foreach (Component comp in loot.GetLootBag())
            {
                comp.Accept(new BuildItemVisitor(database));
                Console.WriteLine("--------------------------------------");
                Console.WriteLine(comp);
                Console.WriteLine("--------------------------------------");
            }
            Console.ReadLine();
        }
    }
}