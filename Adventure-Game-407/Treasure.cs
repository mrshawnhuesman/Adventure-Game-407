using System;
using System.Runtime.CompilerServices;

namespace Adventure_Game_407
{
    public abstract class Treasure
    {
        protected String Name { get; set; }
        protected Creature Owner { get; set; }

        public abstract void Use();
        
        public void Drop()
        {
            // stub
        }
    }
}