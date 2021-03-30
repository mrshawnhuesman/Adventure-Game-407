namespace Adventure_Game_407
{
    public class Armor : Item
    {
        public int Strength { get; private set; }

        public Armor()
        {
            this.Strength = StaticRandom.Instance.Next(20);
        }

        public Armor(int strength)
        {
            this.Strength = strength;
        }
    }
}