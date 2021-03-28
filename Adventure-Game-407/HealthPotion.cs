using System;

namespace Adventure_Game_407
{
    public class HealthPotion : Item
    {    
        public int RestoreAmount { get; private set; }
        
        private Random _random = new Random();

        public HealthPotion()
        {
            // Randomly create restore amount from 5 to 50
            var rollForRestoreAmount = StaticRandom.Instance.Next(46);
            RestoreAmount = rollForRestoreAmount + 5;
        }
        
        public override void Use()
        {
            Owner.RestoreHealth(RestoreAmount);
        }
    }
}