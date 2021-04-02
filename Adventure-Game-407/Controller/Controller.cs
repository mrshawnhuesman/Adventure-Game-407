﻿﻿using System;
using System.Runtime.Remoting.Contexts;
using Adventure_Game_407.View;
using System.Collections.Generic; 

namespace Adventure_Game_407
{
    public class Controller
    {
        public CLIView View;
        
        public void StartGame()
        {
            Hero hero = new Hero(null, new Weapon(), new Armor(), 150);
            Dungeon dungeon = new Dungeon(); 
            View = new CLIView();
            View.AskForHeroName(hero);
            PutHeroInDungeon(hero, dungeon);
            while (!HasGameEnded(hero))
            {
                hero.Room = dungeon.CurrentRoom;
                View.ShowDungeonMinimap(dungeon, hero);
                PerformMainMenuChoice(hero, dungeon);
                CheckRoomForMonsters(hero);
            }
        }

        private void PerformMainMenuChoice(Hero hero, Dungeon dungeon)
        {
            var menuChoice = View.AskForMainMenuChoice(dungeon, hero);
            switch (menuChoice)
            {
                // View Inventory
                case 1:
                    PerformInventoryChoice(hero);
                    PerformMainMenuChoice(hero, dungeon);
                    break;
                // View Minimap
                case 2:
                    View.ShowDungeonMinimap(dungeon, hero);
                    PerformMainMenuChoice(hero, dungeon);
                    break;
                // Inspect Room
                case 3:
                    View.ShowRoomInformation(hero.Room);
                    PerformMainMenuChoice(hero, dungeon);
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
                        if (!monster.IsAlive())
                        {
                            dungeon.CurrentRoom.Monster = null;
                        }
                    }
                    break;
            }
        }

        private void PerformInventoryChoice(Hero hero)
        {
            var inventoryChoice = View.AskForInventoryChoice(hero);
            var inventory = hero.Inventory;
            if (inventoryChoice == inventory.Count + 1)
            {
                Console.WriteLine("Closing inventory menu.");
            }
            else
            {
                Console.WriteLine("You are looking at: ");
                var itemSelected = inventory[inventoryChoice];
                View.ShowItemInformation(itemSelected, inventoryChoice);
                
                Console.WriteLine("[1] - Use Item");
                Console.WriteLine("[2] - Drop Item");
                Console.WriteLine("[3] - Exit Menu");
                var itemChoice = View.AskUserInputInteger("Selection:", 1, 3);
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
                View.AskForInventoryChoice(hero);
            }
        }

        private void CheckRoomForMonsters(Hero hero)
        {
            var currentRoom = hero.Room;
            if (currentRoom.HasMonster())
            {
                Console.WriteLine("Monster " + currentRoom.Monster.Name +
                                  " has appeared!");
                if (currentRoom.CheckMonsterAggro() == 3)
                {
                    currentRoom.Monster.Fight(hero);
                    currentRoom.Monster.DropAllItems();
                }
                else
                {
                    Console.WriteLine(currentRoom.Monster.Name + " is not aggressive.");       
                }
            }
            else
            {
                if (currentRoom.Loot.Count > 0)
                {
                    GetLoot(hero);
                }
                Console.WriteLine("No Monsters found.");
            }
        }

        private void PutHeroInDungeon(Hero hero, Dungeon dungeon)
        {
            // hero starts in entry room
            hero.Room = dungeon.CurrentRoom;
            View.ShowDungeonMinimap(dungeon, hero);
        }

        private bool HasGameEnded(Hero hero)
        {
            // hero has died or reached the exit
            if (!hero.IsAlive())
            {
                Console.WriteLine("You died. Goodbye.");
                return true;
            }

            if (hero.Room.Type == 'X')
            {
                Console.WriteLine("You have exited the game. Goodbye.");
                return true;
            }

            return false;
        }

        //GetLoot(...) will show current loot drop and ask hero to pick item from loot
        private void GetLoot(Hero hero)        {
            List<Item> roomLoot = hero.Room.Loot;
            if (roomLoot.Count >= 1)
            {
                var lootChoice = View.AskHeroToGetLoot(hero);
                if (lootChoice < roomLoot.Count)
                {
                    for (int index = 0; index < roomLoot.Count; index++)
                    {
                        if (lootChoice < roomLoot.Count)
                        {
                            var itemToBePickedUp = hero.Room.Loot[lootChoice];
                            hero.PickUp(itemToBePickedUp);
                            roomLoot.Remove(itemToBePickedUp);
                            GetLoot(hero);
                        }
                    }
                }
                else if (lootChoice == roomLoot.Count)
                {
                    Console.WriteLine("Leaving Loot Behind..");
                }
                else
                {
                    Console.WriteLine("Invalid selection. Try Again");
                    GetLoot(hero);
                }
            }
                
            
        }
    }
}