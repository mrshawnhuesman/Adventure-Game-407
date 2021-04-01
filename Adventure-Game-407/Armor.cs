#pragma warning disable IDE0060

namespace Adventure_Game_407
{
    public class Armor : Item
    {
        public int Strength { get; set;  }

        public Armor()
        {
            Strength = StaticRandom.Instance.Next(20);
            GenerateName();
        }

        public Armor(int strength)
        {
            Strength = strength;
            GenerateName();
        }

        public Armor(int strength, string name)
        {
            Strength = strength;
            GenerateName();
        }

        public void GenerateName()
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
        public int CompareTo(Armor armor)
        {
            var currentArmor = this.Strength;
            if (currentArmor > armor.Strength)
                return 1;
            else if (currentArmor == this.Strength)
                return 0;
            return -1;
        }
    }
}