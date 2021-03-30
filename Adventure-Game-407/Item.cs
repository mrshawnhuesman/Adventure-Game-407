using System;

namespace Adventure_Game_407
{
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
                ((Hero) Owner).Inventory.Remove(this);
            }
        }
    }
}