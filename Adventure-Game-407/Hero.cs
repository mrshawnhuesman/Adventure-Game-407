using System;
using System.Collections.Generic;
using Microsoft.Win32;

namespace Adventure_Game_407
{
    public class Hero : Creature
    {
        private readonly int InventoryCapacity = 10;
        public List<Item> Inventory { get; set; }
  
        //Hero constructor that takes parameter name, weapon, armor, and hitpoints
        public Hero(string name, Weapon weapon, Armor armor, int hitpoints)
        {
            Name = name;
            Weapon = weapon;
            Armor = armor;
            Hitpoints = hitpoints;
            Inventory = new List<Item>(InventoryCapacity) { weapon, armor };         
        }

        //PickUp method that will add an item to hero item inventory
        public void PickUp(Item item)
        {
            if (Inventory.Count + 1 <= InventoryCapacity)
            {
                Inventory.Add(item);
                item.Owner = this;
            }
            else
            {
                Console.WriteLine("You do not have the capacity to pickup: " + item.Name);
            }
        }

        //DropItem method that will remove item from hero item inventory and add it to the current room loot 
        public void DropItem(int inventoryNumber)
        {
            try
            {
                var removedItem = Inventory[inventoryNumber];
                Inventory.RemoveAt(inventoryNumber);
                Room.Loot.Add(removedItem);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.WriteLine("Unable to drop item from inventory index: " + inventoryNumber);
            }
        }        
    }
}
