﻿﻿using System;
using System.Collections.Generic;

namespace Adventure_Game_407.View
{
    public class CLIView
    {
        public void ShowDungeonMinimap(Dungeon dungeon, Hero hero)
        {
            Console.WriteLine("");
            Console.WriteLine("Dungeon Minimap: ");
            var rooms = dungeon.Rooms;
            var currentRoom = hero.Room;
            for (int i = 0; i < dungeon.Rows; i++)
            {
                for (int j = 0; j < dungeon.Cols; j++)
                {
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

            Console.WriteLine("Your location is indicated by " + '+');
        }

        public void AskForHeroName(Hero hero)
        {
            Console.WriteLine("Enter Hero Name: ");
            var name = Console.ReadLine();
            hero.Name = name;
        }

        public int AskForMainMenuChoice(Dungeon dungeon, Hero hero)
        {
            Console.WriteLine("");
            // display HP and items in inventory
            ShowHeroInformation(hero);

            Console.WriteLine("[1] - View Inventory");
            Console.WriteLine("[2] - View Minimap");
            Console.WriteLine("[3] - Inspect Room");
            Console.Write("| ");
            Console.Write(dungeon.CanMoveUp() ? "[4] - Move Up     | " : "[4] - Unavailable | ");
            Console.Write(dungeon.CanMoveDown() ? "[5] - Move Down   | " : "[5] - Unavailable | ");
            Console.Write(dungeon.CanMoveLeft() ? "[6] - Move Left   | " : "[6] - Unavailable | ");
            Console.Write(dungeon.CanMoveRight() ? "[7] - Move Right  | " : "[7] - Unavailable | ");
            Console.WriteLine();
            if (hero.Room.HasMonster())
            {
                Console.Write("[8] - Fight Monster: " + hero.Room.Monster.Name);
            }
            Console.WriteLine();

            var menuChoice = AskUserInputInteger("Input choice: ", 1, 8);
            return menuChoice;
        }

        //showHeroInventory(...) will show current items in hero inventory
        // and ask the hero if they would like to use or drop items in their inventory
        public int AskForInventoryChoice(Hero hero)
        {
            Console.WriteLine("");
            List<Item> inventory = hero.Inventory;
            Item item;
            for (int index = 0; index < inventory.Count; index++)
            {
                item = inventory[index];
                ShowItemInformation(item, index);
            }

            var inventoryMessage = "Choose inventory item to use, drop, or equip. Enter " +
                                   "[" + (inventory.Count + 1) + "] to exit inventory menu.";
            var inventoryChoice = AskUserInputInteger(inventoryMessage, 0, inventory.Count + 1);

            return inventoryChoice;
        }

        public void ShowItemInformation(Item item, int index)
        {
            Console.WriteLine("");
            Console.WriteLine("Item {0}: {1, -15} Type: {2}", index, item.Name, item.GetType().Name);
            if (item is Weapon)
            {
                ShowWeaponInformation((Weapon)item);
            }
            else if (item is Armor)
            {
                ShowArmorInformation((Armor)item);
            }
        }

        public void ShowRoomInformation(Room room)
        {
            Console.WriteLine("");
            Console.WriteLine("Current Room Type: " + room.Type);
            Console.WriteLine("Current Room Loot: ");
            for (int i = 0; i < room.Loot.Count; i++)
            {
                Console.Write(room.Loot[i].Name + " ");
            }

            ShowMonsterInformation(room.Monster);
        }

        public void ShowMonsterInformation(Creature monster)
        {
            Console.WriteLine("");
            if (monster != null)
            {
                Console.WriteLine("Monster Info");
                Console.WriteLine("Name: " + monster.Name);
                if (monster is PhysicalMonster)
                {
                    Console.WriteLine("Type: Physical");
                }
                else
                {
                    Console.WriteLine("Type: Magical");
                    var offenseSkill = ((MagicalMonster)monster).DefensiveSkill;
                    Console.WriteLine("Offensive Ability: " + offenseSkill.Name);
                    Console.WriteLine("Offensive Damage: " + offenseSkill.OffensiveDamage);
                    var defenseSkill = ((MagicalMonster)monster).OffensiveSkill;
                    Console.WriteLine("Defensive Ability: " + defenseSkill.Name);
                    Console.WriteLine("Defensive Damage: " + defenseSkill.DefensiveBuff);
                }

                ShowWeaponInformation(monster.Weapon);
                ShowArmorInformation(monster.Armor);
            }
            else
            {
                Console.WriteLine("No monsters here");
            }
        }

        public void ShowWeaponInformation(Weapon weapon)
        {
            Console.WriteLine("");
            Console.WriteLine("Weapon Name: " + weapon.Name);
            Console.WriteLine("Max Weapon Damage: " + weapon.MaxDamage);
            Console.WriteLine("Number of Weapon Attacks: " + weapon.NumAttacks);
        }

        public void ShowArmorInformation(Armor armor)
        {
            Console.WriteLine("");
            Console.WriteLine("Armor Name: " + armor.Name);
            Console.WriteLine("Armor Defense Amount: " + armor.Strength);
        }

        //ShowHeroInformation(...) will show hero name, hit points, inventory, inventory space
        public void ShowHeroInformation(Hero hero)
        {
            Console.WriteLine("");
            Console.WriteLine(hero.Name + "(HP: " + hero.CurrentHitPoints + ")");
            Console.WriteLine("Inventory Status: " + hero.Inventory.Count + " items in current inventory, "
                              + (hero.Inventory.Capacity - hero.Inventory.Count) + " empty slot(s)");
        }

        //AskUserInputInteger(...) will ask user for an integer input 
        //display string that ask user to input an integer from 1 to the max-1 
        //if user selected the integer great than max, it will ask user to re-enter correct integer
        public int AskUserInputInteger(string stringInput, int min, int max)
        {
            int number = 0;
            Console.WriteLine(stringInput);
            try
            {
                number = int.Parse(Console.ReadLine());
                if (number < min || number > max)
                {
                    throw new ArgumentException("Input outside of range: " +
                                                min + '-' + max);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                AskUserInputInteger(stringInput, min, max);
            }
            return number;
        }

        //AskHeroToGetLoot(...) will first show the available loot for the hero to pick up then ask hero to select the loot
        public int AskHeroToGetLoot(Hero hero)
        {
            List<Item> heroLoot = hero.Room.Loot;
            ShowLoot(heroLoot);
            var lootChoice = AskUserInputInteger("What loot do you want?:", 1, heroLoot.Count + 1); //this will only ask Hero to pick one item only ?
            return lootChoice;
        }

        //Method to show current items in the loot
        public void ShowLoot(List<Item> heroLoot)
        {
            Console.WriteLine("Current Room Loot: ");
            for (int i = 0; i < heroLoot.Count; i++)
            {
                Console.Write(heroLoot[i].Name + " ");
            }
        }
    }       
}