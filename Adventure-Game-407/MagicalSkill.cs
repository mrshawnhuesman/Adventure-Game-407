using System;


namespace Adventure_Game_407
{   
    //Skill class that consist of 2 type of magical skills: offensive and defensive
    public class MagicalSkill
    {
        public bool Used;                     //marker for skill if it has been used or not
        public int OffensiveDamage { get; }   //skill damage
        public double DefensiveValue { get; }    //defensive skill value, how much weapon damage will be absorbed (100% = 1, is a miss)
        public string Name { get; }           //name of the skill
        public string Type { get; }           //there are 2 type of Skills: offensive skill and defensive skill

        //MagicalSkil constructor for type offensive
        public MagicalSkill(string name, string type, int offensiveDamage)
        {
            Name = name;
            Type = type;
            if (type.Equals("offensive"))
            {
                OffensiveDamage = offensiveDamage;
            }       
            Used = false;
        }

        //MagicalSkil constructor for type defensive
        public MagicalSkill(string name, string type, double defenseMultiplier)
        {
            Name = name;
            Type = type;
            if (type.Equals("defensive"))      
            {
                DefensiveValue = defenseMultiplier;
            }
            Used = false;
        }
    }
}
