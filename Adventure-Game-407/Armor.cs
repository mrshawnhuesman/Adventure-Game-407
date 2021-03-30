namespace Adventure_Game_407
{
    public class Armor : Item
    {
        public int Strength { get; }

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
    }
}