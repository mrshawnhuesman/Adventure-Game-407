#pragma warning disable IDE0060

namespace Adventure_Game_407
{
    // Armor class
    public class Armor : Item
    {
        public int Strength { get; set;  }  

        // Default Armor constructor that generates a random Strength from 1 - 20
        public Armor()
        {
            Strength = StaticRandom.Instance.Next(20);
            Strength += 1;
            GenerateName();
        }

        // Armor constructor that sets the Strength and Name
        public Armor(int strength)
        {
            Strength = strength;
            GenerateName();
        }

        public Armor(int strength, string name)
        {
            Strength = strength;
            Name = name;
        }

        private void GenerateName()
        {
            if (Strength <= 20 || Strength >= 15)
                Name = "Knight Armor";
            if (Strength >= 10)
                Name = "Turtle Shell";
            else if (Strength >= 5)
                Name = "Tough Skin";
            else
                Name = "Weak Armor";
        }
        private int CompareTo(Armor armor)
        {
            var currentArmor = Strength;
            if (currentArmor > armor.Strength)
                return 1;
            if (currentArmor == Strength)
                return 0;
            return -1;
        }

        public override void Use()
        {
            var ownerArmor = Owner.Armor;
            Owner.Armor = this;
            if (Owner is Hero)
            {
                ((Hero) Owner).Inventory.Add(ownerArmor);
            }
            else
            {
                ownerArmor.Drop();
            }
        }
    }
}