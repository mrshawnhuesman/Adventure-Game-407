using System;

namespace Adventure_Game_407
{
    public class Weapon : Item
    {
        private int SwingsPerTurn;
        private int Damage;
        private bool IsMagical;

        public Weapon()
        {
            var rollSwingsPerTurn = StaticRandom.Instance.Next(2);
            SwingsPerTurn = rollSwingsPerTurn + 1;
            var rollDamage = StaticRandom.Instance.Next(5);
            Damage = rollDamage + 1;
            var rollIsMagical = StaticRandom.Instance.Next(2);
            if (rollIsMagical == 1)
            {
                IsMagical = true;
            }
            else
            {
                IsMagical = false;
            }
        }

        public Weapon(int swingsPerTurn, int damage, bool isMagical)
        {
            SwingsPerTurn = swingsPerTurn;
            Damage = damage;
            IsMagical = isMagical;
        }
    }
}