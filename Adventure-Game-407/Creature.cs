namespace Adventure_Game_407
{   
    //creature class - super class of subclasses hero and monster
    public class Creature
    {
        public Weapon Weapon { get; protected set; }    //creature weapon
        public Armor Armor { get; protected set; }      //creature armor
        public int Hitpoints { get; protected set; }    //creature hit points
        public string Name { get; protected set; }      //creature name

        public void Fight()
        {
            // stub
        }
            
        public void TakesDamage()
        {
            // stub
        }
        
        //RestoreHealth(...) increases the amount of hit points
        public void RestoreHealth(int amount)
        {
            Hitpoints += amount;
        }
    }
}