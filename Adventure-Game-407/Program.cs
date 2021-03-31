using System;
using System.Net.Mail;

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
            
            Console.WriteLine("Fight 1");
            Console.WriteLine("\n\n\n");
            var creature1 = new Monster("Will", new Weapon(), new Armor(), 150, 4  );
            var creature2 = new Monster("Anthony", new Weapon(), new Armor(), 150, 4  );
            creature1.Fight(creature2);
            
            Console.WriteLine("Fight 2");
            Console.WriteLine("\n\n\n");

            var offenseSkill = new MagicalSkill("Big Punch", "offensive", 5);
            var defenseSkill = new MagicalSkill("Big Shield", "defensive", 0.5);
            
            var magic1 = new MagicalMonster("Will", new Weapon(), new Armor(), 150, 4, 
                offenseSkill, defenseSkill );
            var magic2 = new MagicalMonster("Anthony", new Weapon(), new Armor(), 150, 4,
                offenseSkill, defenseSkill);
            magic1.Fight(magic2);
        }

    }
}