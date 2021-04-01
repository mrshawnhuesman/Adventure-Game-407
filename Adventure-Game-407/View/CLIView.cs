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
            
            Console.WriteLine("Your location is indicated by " + '+');
        }

        public void AskHeroForName(Hero hero) {
            Console.WriteLine("Enter Hero Name: ");
            var name = Console.ReadLine();
            hero.Name = name;
        }
        
        public void ShowMainMenu(Dungeon dungeon, Hero hero)
        {
            Console.WriteLine("");
            // display HP and items in inventory
            ShowHeroInformation(hero);
            
            Console.WriteLine("[1] - View Inventory");
            Console.WriteLine("[2] - View Minimap");
            Console.WriteLine("[3] - Inspect Room");
            Console.Write("| ");
            Console.Write(dungeon.CanMoveUp()    ? "[4] - Move Up     | " : "[4] - Unavailable | ");
            Console.Write(dungeon.CanMoveDown()  ? "[5] - Move Down   | " : "[5] - Unavailable | ");
            Console.Write(dungeon.CanMoveLeft()  ? "[6] - Move Left   | " : "[6] - Unavailable | ");
            Console.Write(dungeon.CanMoveRight() ? "[7] - Move Right  | " : "[7] - Unavailable | ");
            Console.WriteLine();
            if (hero.Room.HasMonster())
            {
                Console.Write("[8] - Fight Monster: " + hero.Room.Monster.Name);
            }
            Console.WriteLine();

            var menuChoice = AskUserInputInteger("Input choice: ", 1, 8);

            switch (menuChoice)
            {
                // View Inventory
                case 1:
                    ShowHeroInventory(hero);
                    ShowMainMenu(dungeon, hero);
                    break;
                // View Minimap
                case 2:
                    ShowDungeonMinimap(dungeon, hero);
                    ShowMainMenu(dungeon, hero);
                    break;
                // Inspect Room
                case 3:
                    ShowRoomInformation(hero.Room);
                    ShowMainMenu(dungeon, hero);
                    break;
                // Move Up
                case 4:
                    if (dungeon.CanMoveUp())
                    {
                        dungeon.MoveUp();
                    }
                    break;
                // Move Down
                case 5:
                    if (dungeon.CanMoveDown())
                    {
                        dungeon.MoveDown();
                    }
                    break;
                // Move Left
                case 6:
                    if (dungeon.CanMoveLeft())
                    {
                        dungeon.MoveLeft();
                    }
                    break;
                // Move Right
                case 7:
                    if (dungeon.CanMoveRight())
                    {
                        dungeon.MoveRight();
                    }
                    break;
                case 8:
                    if (hero.Room.HasMonster())
                    {
                        // fight monster
                        var monster = hero.Room.Monster;
                        hero.Fight(monster);
                        // remove monster from room if it has died
                        if (monster.CurrentHitPoints >= 0)
                        {
                            dungeon.CurrentRoom.Monster = null;
                        }
                    }
                    break;
            }

        }

        //showHeroInventory(...) will show current items in hero inventory
        // and ask the hero if they would like to use or drop items in their inventory
        private void ShowHeroInventory(Hero hero)
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

            if (inventoryChoice == inventory.Count + 1)
            {
                Console.WriteLine("Closing inventory menu.");
            }
            else
            {
                Console.WriteLine("You are looking at: ");
                var itemSelected = inventory[inventoryChoice];
                ShowItemInformation(itemSelected, inventoryChoice);
                
                Console.WriteLine("[1] - Use Item");
                Console.WriteLine("[2] - Drop Item");
                Console.WriteLine("[3] - Exit Menu");
                var itemChoice = AskUserInputInteger("Selection:", 1, 3);
                switch (itemChoice)
                {
                    case 1:
                        itemSelected.Use();
                        break;
                    case 2:
                        itemSelected.Drop();
                        break;
                    case 3:
                        Console.WriteLine("Exiting menu");
                        break;
                }
                ShowHeroInventory(hero);
            }
        }

        private void ShowItemInformation(Item item, int index)
        {
            Console.WriteLine("");
            Console.WriteLine("Item {0}: {1, -15} Type: {2}", index, item.Name, item.GetType().Name);
            if (item is Weapon)
            {
                ShowWeaponInformation((Weapon)item);
            }
            else if (item is Armor)
            {
                ShowArmorInformation((Armor) item);
            }
        }
        
        private void ShowRoomInformation(Room room)
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

        private void ShowMonsterInformation(Creature monster)
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
                    var offenseSkill = ((MagicalMonster) monster).DefensiveSkill;
                    Console.WriteLine("Offensive Ability: " + offenseSkill.Name);
                    Console.WriteLine("Offensive Damage: " + offenseSkill.OffensiveDamage);
                    var defenseSkill = ((MagicalMonster) monster).OffensiveSkill;
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
        
        //showHero(...) will show hero name, hit points, inventory, inventory space
        private void ShowHeroInformation(Hero hero)
        {
            Console.WriteLine("");
            Console.WriteLine(hero.Name + "(HP: " + hero.CurrentHitPoints + ")");
            Console.WriteLine("Inventory Status: " + hero.Inventory.Count + " items in current inventory, " 
                              + (hero.Inventory.Capacity - hero.Inventory.Count) + " empty slot(s)");
        }

        private int AskUserInputInteger(string stringInput, int min, int max)
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
    }
}