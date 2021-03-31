using System;


namespace Adventure_Game_407
{   
    //Skill class that consist of 2 type of magical skills: offensive and defensive
    public class MagicalSkill
    {     
        public int OffensiveDamage { get; }    //skill damage
        public int DefensiveBuff { get; }      //defensive buff value, amount of damage that will be absorbed (100 = miss / 0 damage, 50 = half of the damage is being absorbed) )
        public string Name { get; }            //name of the skill
        public string Type { get; }            //there are 2 type of Skills: offensive skill and defensive skill

        //MagicalSkil constructor for type offensive
        public MagicalSkill(string name, string type, int offensiveDamage)
        {
            Name = name;
            Type = type;
            if (type.Equals("offensive"))
            {
                OffensiveDamage = offensiveDamage;
            }    
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
        }
    }
}
