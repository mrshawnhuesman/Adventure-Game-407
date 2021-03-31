namespace Adventure_Game_407
{
    public class Armor : Item
    {
        public int Strength { get; set; }

        public Armor()
        {
            Strength = StaticRandom.Instance.Next(20);
            GenerateName();
        }

        //Armor constructor that only take a parameter int strength with auto generated name
        public Armor(int strength)
        {
            Strength = strength;
            GenerateName();
        }

        //Armor constructor that takes 2 parameter: int strength and string name
        public Armor(int strength, string name)
        {
            Strength = strength;
            Name = name;
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