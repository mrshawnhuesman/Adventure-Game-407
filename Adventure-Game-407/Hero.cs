using System;
using System.Collections.Generic;
using Microsoft.Win32;

namespace Adventure_Game_407
{
    public class Hero : Creature
    {
        private int InventoryCapacity = 10;
        public List<Item> Inventory { get; private set; }
        public Dungeon CurrentDungeon { get; private set; }
        public Room CurrentRoom { get; private set; }

        //Hero constructor that takes parameter name, weapon, armor, and hitpoints
        public Hero(string name, Weapon weapon, Armor armor, int hitpoints)
        {
            Name = name;
            Weapon = weapon;
            Armor = armor;
            Hitpoints = hitpoints;
            Inventory.Add(weapon);
            Inventory.Add(armor);
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
                CurrentRoom.Loot.Add(removedItem);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.WriteLine("Unable to drop item from inventory index: " + inventoryNumber);
            }
        }

        //IsAlive(...) returns the alive status; returns true if hero hitpoints > 0, else return false
        public bool IsAlive()
        {
            return Hitpoints > 0;
        }

        //Fight 
        public void Fight(Monster enemy)
        {
            int damage, index;
            while (IsAlive() && enemy.IsAlive())
            {
                for (index = 0; index < Weapon.numAttacks(); index++)
                {
                    damage = StaticRandom.Instance.Next(1, 21);
                    if (damage > enemy.Armor.Strength)
                    {
                        enemy.Name.Strength -= damage;
                        Console.Write(enemy.Name + " got hit and lost: " + damage + " hit points !!!");
                    }
                    if (enemy.IsAlive()) enemy.Fight(this);

                    /*
                    generate a random number(say between 1 and 20) and if it is
                    greater than c’s armor value, you strike c for weapon.getDamage()
                    amount which is a random amount between 1 and the max that your
                    weapon can produce, tell c that you hit it for that many points of
                    damage so that it can reduce its hit points by that amount
                    if (c.isAlive()) c.fight(me);          
                    */
                }
            }
        }
    }
}
