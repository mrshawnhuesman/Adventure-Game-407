using System;
using System.Collections.Generic;
using Microsoft.Win32;

namespace Adventure_Game_407
{
    public class Hero : Creature
    {
        private int inventoryCapacity = 10;
        private List<Treasure> inventory;

        public Hero()
        {
            
        }

        public void Use(Treasure treasure)
        {
            treasure.Use();
        }

        public void PickUp(Treasure treasure)
        {
            if (inventory.Count + 1 <= inventoryCapacity)
            {
                inventory.Add(treasure);
            }
            else
            {
                Console.WriteLine("You do not have the capacity to" +
                                  " pickup this item.");
            }
            inventory.Add(treasure);
        }
        
    }
}