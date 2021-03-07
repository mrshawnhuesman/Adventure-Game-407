using System.Collections.Generic;
using Microsoft.Win32;

namespace Adventure_Game_407
{
    public class Hero : Creature
    {
        private int inventorySize = 10;
        private List<Treasure> inventory;

        public Hero()
        {
            
        }

        public void Use(Treasure treasure)
        {
            treasure.Use();
        }
    }
}