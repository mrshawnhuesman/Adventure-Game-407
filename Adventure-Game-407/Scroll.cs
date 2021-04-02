namespace Adventure_Game_407
{
    // Scroll class
    public class Scroll : Item
    {
        // turns Room type into 'M' disabling all magic abilities
        public void Use(Hero hero)
        {
            hero.Room.Type = 'M';
        }
    }
}