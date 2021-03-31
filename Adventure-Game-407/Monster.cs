namespace Adventure_Game_407
{
    public class Monster : Creature
    {   
        private readonly int Aggression;    //monster agression level (1 is low, 3 is high)
        private readonly Weapon naturalWeapon = new Weapon (1, 2, false, "claw");   //default monster armor which is the natural armor
        private readonly Armor naturalArmor = new Armor (2, "tough skin");          //default monster armor which is the natural armor

        //Monster constructor that takes parameter name, equippable weapon, equippable armor, hitpoints, agression level, and monster type
        //Monster natural weapon values and natural armor values will be combined with equippable armor and equippable weapon
        //the name of the weapon or armor is based on the equippable armor or weapon
        public Monster(string name, Weapon weapon, Armor armor, int hitpoints, int aggression)
        {
            Name = name;
            Weapon = weapon;
            Weapon.NumAttacks += naturalWeapon.NumAttacks;     
            Weapon.MaxDamage += naturalWeapon.MaxDamage;            
            Armor = armor;
            Armor.Strength += naturalArmor.Strength;
            Hitpoints = hitpoints;
            Aggression = aggression;
        }
    }
}