using System.Collections.Generic;

namespace Adventure_Game_407
{
    public class MagicalMonster : Creature
    {
        private readonly int Aggression;
        //public double DefensiveValue { get; set; }
        public MagicalSkill OffensiveSkill { get; set; }
        public MagicalSkill DefensiveSkill { get; set; }
        public double CurrentMagicDefense { get; set; }

        //MagicalMonster constructor that takes parameter name, weapon, armor, hitpoints, agression level, and monster type
        public MagicalMonster(string name, Weapon weapon, Armor armor, int hitpoints, int aggression, MagicalSkill offensiveSkill, MagicalSkill defensiveSkill)
        {
            Name = name;
            Weapon = weapon;
            Armor = armor;
            Hitpoints = hitpoints;
            Aggression = aggression;
            DefensiveSkill = defensiveSkill;
            OffensiveSkill = offensiveSkill;
        }       
    }
}