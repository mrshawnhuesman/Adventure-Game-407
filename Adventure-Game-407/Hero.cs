using System;
using System.Collections.Generic;

#pragma warning disable IDE0038

namespace Adventure_Game_407
{
    // Hero class
    public class Hero : Creature
    {
        private const int InventoryCapacity = 10;
        public List<Item> Inventory { get; protected set; }
        public Treasure Gold = new Treasure(0);
  
        //Hero constructor that takes parameter name, weapon, armor, and hitpoints
        public Hero(string name, Weapon weapon, Armor armor, int hitpoints)
        {
            Name = name;
            Weapon = weapon;
            Armor = armor;
            MaxHitPoints = hitpoints;
            CurrentHitPoints = hitpoints;
            Inventory = new List<Item>(InventoryCapacity) { weapon, armor };
            Room = null;
        }

        //Hero Move during Fight is weapon attack
        public override Tuple<int, int> GenerateMove(Creature opponent)
        {
            int weaponDamage = UseWeapon(opponent);
            return new Tuple<int, int>(1, weaponDamage);          
        }

        //Implementation of Hero fight method
        //A creature will be defeated if either the current monster or the opponent's hitpoints dropped to 0
        public override void Fight(Creature opponent)
        {
            while (IsAlive() && opponent.IsAlive())                   //while loop that last until one of 2 creatures dies
            {
                Console.WriteLine("\n" + Name + "'s turn.");                

                Tuple<int, int> move;

                move = GenerateMove(opponent);        //generate Hero move                                 
                var attackDamage = move.Item2;
                         
                //if the opponent is magical monster and its damage reduction buff defense is > 0 (has a defensive buff), calculate final damage = attack damage - current magic defense 
                if (opponent is MagicalMonster && ((MagicalMonster)opponent).DamageReductionBuff > 0)
                {
                    var damageReduction = (int)(attackDamage * ((MagicalMonster)opponent).DamageReductionBuff / 100);
                    attackDamage -= damageReduction;
                    ((MagicalMonster)opponent).DamageReductionBuff = 0;     //reset opponent damage reduction buff to 0 after being used to reduce damage
                }

                //if final damage is not 0, compute the inflicted damage and display result to console 
                if (attackDamage != 0)
                {
                    opponent.InflictDamage(attackDamage);
                    Console.WriteLine(opponent.Name + " takes " + attackDamage + " damage and now has " + opponent.CurrentHitPoints + " HP.");
                }

                //if the opponent used the defensive skill that reduces damage 100% then display the message to console
                else
                {
                    Console.WriteLine(opponent.Name + " takes no damage from " + Name + "" + "'s attacks.");
                }

                //if the opponent still alive then opponent will fight back
                if (opponent.IsAlive()) opponent.Fight(this);

                //if the opponent dies, display win message to console
                else
                {   
                    Console.WriteLine("HERO - " + Name + " terminated " + opponent.Name);
                }
            }
        }      
           
        //PickUp method that will add an item to hero item inventory
        public void PickUp(Item item)
        {
            if (item is Treasure)                               //if it is a tresure, add gold amount to the hero treasure gold amount
            {
                Gold.GoldAmount += ((Treasure)item).GoldAmount;
            }
            else if (Inventory.Count + 1 <= InventoryCapacity)  //else if inventory is not full, add item to the inventory
            {
                Inventory.Add(item);
                item.Owner = this;                
            }
            else                                                //else display notification showing not enough capacity
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
