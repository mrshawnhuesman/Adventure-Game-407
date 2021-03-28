using System;
using System.Collections.Generic;
using Microsoft.Win32;

namespace Adventure_Game_407
{
    public class Hero : Creature
    {
        private int InventoryCapacity = 10;
        public List<Item> Inventory { get; private set; }

        public Hero()
        {
            
        }

        public void PickUp(Item item)
        {
            if (Inventory.Count + 1 <= InventoryCapacity)
            {
                Inventory.Add(item);
                item.Owner = this;
            }
            else
            {
                Console.WriteLine("You do not have the capacity to" +
                                  " pickup this item.");
            }
        }
        
    }
}