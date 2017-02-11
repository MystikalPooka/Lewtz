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
            TableRepository.LoadSingleFile(@"..\..\Tables\treasure table.json", new JSONLoader());
            TableRepository.LoadSingleFile(@"..\..\Tables\magic base.json", new JSONLoader());
            TableRepository.LoadAllMatchingStringFromDirectory(@"..\..\Tables", @"*special abilities*", new JSONLoader());
            var baseTable = TableRepository.GetTableFromString("treasure table");

            TableRepository.GetTableFromString("armor special abilities").Accept(new PrintEntireTreeVisitor());
            TableRepository.GetTableFromString("shield special abilities").Accept(new PrintEntireTreeVisitor());

            baseTable.Accept(new PrintEntireTreeVisitor());
            Console.WriteLine("\r\n===================\r\n");

            baseTable.RollCount = 16;

            Console.WriteLine("\r\n===================\r\n");
            
            var loot = new LootVisitor();
            baseTable.Accept(loot);

            foreach (Component comp in loot.GetLootBag())
            {
                Console.WriteLine(comp);
                Console.WriteLine("--------------------------------------");
            }
            Console.ReadLine();
        }
    }
}