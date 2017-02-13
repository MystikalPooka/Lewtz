using ItemRoller.Data_Structure;
using ItemRoller.Loaders;
using ItemRoller.Visitors;
using System;
using System.Text;

namespace ItemRoller
{
    class Program
    {
        static void Main(string[] args)
        {
            var tableRepo = new TableRepository();

            tableRepo.LoadSingleFile(@"..\..\..\Tables\treasure table.json", new JSONLoader());
            tableRepo.LoadSingleFile(@"..\..\..\Tables\magic base.json", new JSONLoader());
            tableRepo.LoadAllMatchingStringFromDirectory(@"..\..\..\Tables", @"*special abilities*", new JSONLoader());
            var baseTable = tableRepo.GetTableByName("treasure table");

            tableRepo.GetTableByName("armor special abilities").Accept(new PrintEntireTreeVisitor());
            tableRepo.GetTableByName("shield special abilities").Accept(new PrintEntireTreeVisitor());

            baseTable.Accept(new PrintEntireTreeVisitor());
            Console.WriteLine("\r\n===================\r\n");

            baseTable.RollCount = 16;

            Console.WriteLine("\r\n===================\r\n");
            
            var loot = new LootVisitor(tableRepo);
            baseTable.Accept(loot);


            StringBuilder lootStr = new StringBuilder("");
            foreach (Component comp in loot.GetLootBag())
            {
                lootStr.AppendLine("--------------------------------------");
                lootStr.AppendLine(comp.ToString());
                lootStr.AppendLine("--------------------------------------");
            }
            Console.WriteLine(lootStr);
            Console.ReadLine();
        }
    }
}