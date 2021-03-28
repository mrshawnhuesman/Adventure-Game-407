using System;
using System.Runtime.CompilerServices;

namespace Adventure_Game_407
{
    public class Treasure : Item
    {
        private int GoldAmount { get; }

        public Treasure(int goldAmount)
        {
            GoldAmount = goldAmount;
        }
    }
}