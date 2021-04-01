﻿﻿using System;
using System.Collections.Generic;

namespace Adventure_Game_407.View
{
    public class CLIView
    {
        public void ShowDungeonMinimap(Dungeon dungeon, Hero hero)
        {
            var rooms = dungeon.Rooms;
            var currentRoom = hero.Room;
            for (int i = 0; i < dungeon.Rows; i++)
            {
                for (int j = 0; j < dungeon.Cols; j++) {
                    if (i == currentRoom.Row && j == currentRoom.Col)
                    {
                        Console.Write("+");
                    }
                    else
                    {
                        Console.Write(rooms[i, j].Type);
                    }
                }
                Console.WriteLine();
            }

            ShowRoomInformation(currentRoom);
        }

        public void ShowRoomInformation(Room room)
        {
            Console.WriteLine("Current Room Type: " + room.Type);
        }

        public void AskHeroForName(Hero hero) {
            Console.WriteLine("Enter Hero Name: ");
            var name = Console.ReadLine();
            hero.Name = name;
        }
        
        //showHero(...) will show hero name, hit points, inventory, inventory space
        public void ShowHero(Hero hero)
        {
            Console.WriteLine(hero.Name + "(HP: " + hero.CurrentHitPoints + ")");
            ShowHeroInventory(hero);
            Console.WriteLine("Inventory Status: " + hero.Inventory.Count + " items in current inventory, " + (10 - hero.Inventory.Count) + " empty slot");
        }

        //showHeroInventory(...) will show current items in hero inventory
        public void ShowHeroInventory(Hero hero)
        {
            List<Item> Inventory = hero.Inventory;
            Item item;
            for (int index = 0; index < Inventory.Count; index++)
            {
                item = Inventory[index];
                Console.WriteLine("Item " + index + " : " + item.Name + "Type: " + item.GetType());                
            }
        }

        public int AskUserInputInteger(string stringInput)
        {
            int number = 0;
            Console.WriteLine(stringInput);
            try
            {                   
                number = int.Parse(Console.ReadLine());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                AskUserInputInteger(stringInput);
            }
            return number;
        }
    }
}