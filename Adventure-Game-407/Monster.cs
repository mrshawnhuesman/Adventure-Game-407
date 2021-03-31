namespace Adventure_Game_407
{
    public class Monster : Creature
    {
        private readonly int Aggression;

        private readonly Weapon naturalWeapon;

        private readonly Armor naturalArmor;

        //Monster constructor that takes parameter name, weapon, armor, hitpoints, agression level, and monster type
        public Monster(string name, Weapon weapon, Armor armor, int hitpoints, int aggression)
        {
            Name = name;
            Weapon = weapon;
            Armor = armor;
            Hitpoints = hitpoints;
            Aggression = aggression;
        }
    }
}