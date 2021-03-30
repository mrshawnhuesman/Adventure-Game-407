namespace Adventure_Game_407
{
    public class Monster : Creature
    {
        private int aggression;
        private int InventoryCapacity = 10;
        public Dungeon CurrentDungeon { get; private set; }
        public Room CurrentRoom { get; private set; }

        //Monster constructor that takes parameter name, weapon, armor, and hitpoints
        public Monster(string name, Weapon weapon, Armor armor, int hitpoints)
        {
            Name = name;
            Weapon = weapon;
            Armor = armor;
            Hitpoints = hitpoints;
        }

        //IsAlive(...) returns the alive status; returns true if monster hitpoints > 0, else return false
        public bool IsAlive()
        {
            return Hitpoints > 0;
        }
    }    
}