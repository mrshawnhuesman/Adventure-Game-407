namespace Adventure_Game_407
{
    public class Scroll : Item
    {
        // turns type into 'M'
        // all magic abilities do not possess extra damage or swings
        
        public Scroll() {}

        public void Use(Hero hero)
        {
            hero.Room.Type = 'M';
        }
    }
}