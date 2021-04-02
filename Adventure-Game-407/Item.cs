using System;

namespace Adventure_Game_407
{
    // Item abstract class
    public abstract class Item
    {
        public Creature Owner;
        public Room RoomOccupied;
        public string Name { get; protected set; }
          
        public virtual void Use()
        {
            Console.WriteLine("This item is not usable.");
        }

        public void Drop()
        {
            if (Owner is Hero)
            {
                // remove item from inventory
                ((Hero) Owner).Inventory.Remove(this);
            }
            
            // drop item into room
            RoomOccupied = ((Hero) Owner).Room;
        }
        
        public void RemoveItemFromInventory() {
            if (Owner is Hero)
            {
                var hero = ((Hero) Owner);
                hero.Inventory.Remove(hero.Inventory.Find(x => x.Owner is Hero));
            }
        }
    }
}