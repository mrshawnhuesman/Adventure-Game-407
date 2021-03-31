using System;

namespace Adventure_Game_407
{
    public class Weapon : Item
    {
        public int NumAttacks { get; set; }
        public int MaxDamage { get; set; }
        private readonly bool IsMagical;

        public Weapon()
        {
            var rollSwingsPerTurn = StaticRandom.Instance.Next(2);
            NumAttacks = rollSwingsPerTurn + 1;
            var rollDamage = StaticRandom.Instance.Next(5);
            MaxDamage = rollDamage + 1;
            var rollIsMagical = StaticRandom.Instance.Next(2);
            if (rollIsMagical == 1)
            {
                IsMagical = true;
            }
            else
            {
                IsMagical = false;
            }
            GenerateName();
        }

        //Weapon constructor that automatically assign the weapon name
        public Weapon(int numAttacks, int maxDamage, bool isMagical)
        {
            NumAttacks = numAttacks;
            MaxDamage = maxDamage;
            IsMagical = isMagical;
            GenerateName();
        }

        //Weapon constructure that takes 4 parameters : number of attacks, max weapon damage, weapon type, and weapon name
        public Weapon(int numAttacks, int maxDamage, bool isMagical, string name)
        {
            NumAttacks = numAttacks;
            MaxDamage = maxDamage;
            IsMagical = isMagical;
            Name = name;
        }

        public void GenerateName()
        {
            if (NumAttacks == 1 && MaxDamage < 3 && IsMagical == false)
                Name = "Teddy Bear";
            
            else if (NumAttacks == 1 && MaxDamage < 3 && IsMagical == false)
                Name = "Bread Stick";

            else if (NumAttacks == 2 && MaxDamage == 3 || MaxDamage == 4 && IsMagical == false)
                Name = "Hammer";

            else if (NumAttacks == 1 && MaxDamage == 3 || MaxDamage == 4 && IsMagical == false)
                Name = "Bow and Arrow";

            else if (NumAttacks == 2 && MaxDamage == 3 || MaxDamage == 4 && IsMagical == false)
                Name = "Lawn Mower";

            else if (NumAttacks == 1 && MaxDamage == 3 || MaxDamage == 4 && IsMagical)
                Name = "Magic Iron Fist";

            else if (NumAttacks == 2 && MaxDamage == 3 || MaxDamage == 4 && IsMagical)
                Name = "Magic Sword";
            
            else if (NumAttacks == 1 && MaxDamage == 5 && IsMagical)
                Name = "Magic Staff";
            
            else if (NumAttacks == 2 && MaxDamage == 5 && IsMagical == false)
                Name = "COVID-19";
            
            else if (NumAttacks == 2 && MaxDamage == 5 && IsMagical)
                Name = "Magic School Bus";
        }
    }
}