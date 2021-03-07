namespace Adventure_Game_407
{
    public class Creature
    {
        public Weapon Weapon { get; private set; }
        public Armor Armor { get; private set; }
        public int Hitpoints { get; private set; }

        public Creature()
        {
            Weapon = new Weapon();
            Armor = new Armor();
        }

        public void Fight()
        {
            // stub
        }

        public void IsAlive()
        {
            // stub
        }

        public void TakesDamage()
        {
            // stub
        }
        
        public void RestoreHealth(int amount)
        {
            Hitpoints += amount;
        }
    }
}