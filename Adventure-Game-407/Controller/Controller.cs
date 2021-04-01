﻿﻿using Adventure_Game_407.View;

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
            //View.AskHeroToMoveInDungeon(hero, dungeon);
        }

        private void PutHeroInDungeon(Hero hero, Dungeon dungeon)
        {
            // hero starts in entry room
            hero.Room = dungeon.CurrentRoom;
            View.ShowDungeonMinimap(dungeon, hero);
        }
    }
}