﻿﻿using System;
  using Adventure_Game_407.View;

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
            View.AskHeroForName(hero);
            PutHeroInDungeon(hero, dungeon);
            while (!hasGameEnded(hero))
            {
                hero.Room = dungeon.CurrentRoom;
                View.ShowDungeonMinimap(dungeon, hero);
                View.ShowMainMenu(dungeon, hero);
                CheckRoomForMonsters(hero);
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
                }
                else
                {
                    Console.WriteLine(currentRoom.Monster.Name + " is not aggressive.");       
                }
            }
            else
            {
                Console.WriteLine("No Monsters found.");
            }
        }

        private void PutHeroInDungeon(Hero hero, Dungeon dungeon)
        {
            // hero starts in entry room
            hero.Room = dungeon.CurrentRoom;
            View.ShowDungeonMinimap(dungeon, hero);
        }

        private bool hasGameEnded(Hero hero)
        {
            // hero has died or reached the exit
            if (!hero.IsAlive() || hero.Room.Type == 'X')
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}