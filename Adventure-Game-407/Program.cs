using System;
using System.Net.Mail;
using Adventure_Game_407.View;

namespace Adventure_Game_407
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            /*
            Dungeon dungeon = new Dungeon();
            var rooms = dungeon.Rooms;
            for (int i = 0; i < dungeon.Rows; i++)
            {
                for (int j = 0; j < dungeon.Cols; j++) {
                    Console.Write(rooms[i, j].Type);
                }
                Console.WriteLine();
            }
            
            for (int i = 0; i < dungeon.Rows; i++)
            {
                for (int j = 0; j < dungeon.Cols; j++) {
                    Console.Write(rooms[i,j].Loot.Count.ToString());
                }
                Console.WriteLine();
            }
            */
            
            /*
            Console.WriteLine("Fight 1");
            Console.WriteLine("\n\n\n");
            var creature1 = new Hero("Will", new Weapon(2, 3, false), new Armor(5), 20);
            var creature2 = new Monster("Anthony", new Weapon(1, 4, true), new Armor(5), 20, 4);
            creature1.Fight(creature2);
            */
            
            
            Console.WriteLine("Fight 2");
            Console.WriteLine("\n\n\n");

            var offenseSkill = new MagicalSkill("Big Punch", "offensive", 5);
            var defenseSkill = new MagicalSkill("Teleport", "defensive", 1.0);
            
            var creature1 = new Hero("Anthony", new Weapon(2, 3, false), new Armor(5), 20);
            
            var magic1 = new MagicalMonster("Will", new Weapon(1, 3, false), new Armor(5),
                20, 4, offenseSkill, defenseSkill );
            //var magic2 = new MagicalMonster("Anthony", new Weapon(1, 3, true), new Armor(5), 
            //    10, 4, offenseSkill, defenseSkill);
            magic1.Fight(creature1);
            
            var controller = new Controller();
            controller.StartGame();
        }

    }
}