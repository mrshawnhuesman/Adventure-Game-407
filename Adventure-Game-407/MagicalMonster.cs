namespace Adventure_Game_407
{
    public class MagicalMonster : Creature
    {
        private readonly int Aggression;
        private List<Skill> MagicSkills { get; set; }

        //MagicalMonster constructor that takes parameter name, weapon, armor, hitpoints, agression level, and monster type
        public MagicalMonster(string name, Weapon weapon, Armor armor, int hitpoints, int aggression)
        {
            Name = name;
            Weapon = weapon;
            Armor = armor;
            Hitpoints = hitpoints;
            Aggression = aggression;           
        }
    }
}