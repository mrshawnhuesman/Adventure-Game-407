using System;
#pragma warning disable IDE0038

namespace Adventure_Game_407
{
    //creature class - super class of subclasses hero and monster
    public class Creature
    {
        protected Weapon Weapon { get; set; }    //creature weapon
        public Armor Armor { get; set; }      //creature armor
        protected int Hitpoints { get; set; }    //creature hit points
        protected string Name { get; set; }      //creature name
        protected Room Room { get; set; }        //creature current room location

        //IsAlive(...) returns the alive status; returns true if creature hitpoints > 0, else return false
        public bool IsAlive()
        {
            return Hitpoints > 0;
        }

        //Fight method will compute the result of a fight between 2 creatures
        //A creature will be defeated if the hitpoints dropped to 0
        public void Fight(Creature opponent)
        {
            int chanceOfHit, index, WeaponDamage, InflictedDamage, TotalDamage = 0;
            MagicalMonster currentMagicalMonster = null, opponentMagicalMonster = null;
            //MagicalSkill opponentDefensiveSkill = null, opponentOfffensiveSkill = null;
     
            if (this is MagicalMonster)                                     //if current creature is a magical monster get the status of their active magic skill
            {
                currentMagicalMonster = (MagicalMonster)this;
            }

            if (opponent is MagicalMonster)                                         //if the opponent is a magical monster get opponent status of their active magic skill
            {
                opponentMagicalMonster = (MagicalMonster)opponent;                       
            }

            while (IsAlive() && opponent.IsAlive())
            {
                var attackDamage = 0;

                // current fighter is magic
                if (currentMagicalMonster != null)
                {
                    currentMagicalMonster.CurrentMagicDefense = 0;

                    // number 1-3
                    var generateMove = StaticRandom.Instance.Next(1, 4);

                    if (generateMove == 1)
                    {
                        attackDamage = UseWeapon(opponent);
                    }
                    else if (generateMove == 2)
                    {
                        attackDamage = currentMagicalMonster.OffensiveSkill.OffensiveDamage;
                    }
                    else
                    {
                        currentMagicalMonster.CurrentMagicDefense = currentMagicalMonster.DefensiveSkill.DefensiveValue;
                    }
                }
                // current fighter is not magic
                else
                {
                    attackDamage = UseWeapon(opponent);
                }

                if (opponent.IsAlive()) opponent.Fight(this);
            }

            while (IsAlive() && opponent.IsAlive())                                 //while loop that will run until one of creatures is not alive
            {
                int DamageReduction = 0;
                
                for (index = 0; index < Weapon.NumAttacks(); index++)               //loop from 0 till the the max number of weapon attacks - 1 (per turn)
                {   
                    chanceOfHit = StaticRandom.Instance.Next(1, 21);                      //placeholder for random number generator that generates int value between 1 and 21 (inclusive)
                    if (chanceOfHit > opponent.Armor.Strength)                            //if opponent creature armor strength is less than the penetration damage
                    {
                        var weaponDamage = StaticRandom.Instance.Next(1, Weapon.MaxDamage);         //generate weapon damage between 1 and max weapon damage (inclusive)
                        
                        if (opponent is MagicalMonster)                                         //if the opponent is a magical monster, it will check if the defensive skill is activated or not
                        {
                            //if defensive skill is activated, calculate damage reduction (ex: Teleport, defensive multiplier is 1, it will absorb all weapon damage - miss)
                            DamageReduction = weaponDamage - (int)(weaponDamage * opponentMagicalMonster.DefensiveSkill.DefensiveValue);
                            Console.WriteLine(opponent.Name + " has casted defensive skill: " + opponentMagicalMonster.DefensiveSkill.Name + " ...");     
                        }

                        else if (this is MagicalMonster) {

                            //if current creature is a magical monster and has defensive skill                             
                            int offenseSkillAmt = 0;  //if current creature is a magical monster and has offensive skill
                            var rand = StaticRandom.Instance.Next(100);                             //generate random number - probability for current monster defensive or offensive skill activation
                            
                            if (rand >= 0 && rand < 50)                                           //if it rolls value between 0 to 30 exclusive then cast defensive skill                                                        
                            {
                                //offenseSkillAmt = currentMagicalMonster.OffensiveSkill.OffensiveDamage;
                                //opponent.Hitpoints =- offensiveSkillAmt;
                                TotalDamage = currentMagicalMonster.OffensiveSkill.OffensiveDamage;
                            } else if (rand >= 50 && rand < 80)
                            {
                                //cast defensive skill such as Teleport
                            } else
                            {
                                TotalDamage = weaponDamage;
                            }

                            Console.WriteLine(Name + " is casting offensive skill: " + selfActiveMagicSkill.Name + " ...");
                                                                            //if current magical monster offensive skill is activated, calculate damage (ex: Fireball damage is 30)
                        }
                        }
                        
                        else
                        {
                            TotalDamage = weaponDamage;
                        }

                        InflictedDamage = TotalDamage - DamageReduction;                                       //inflicted damage is weapon damage substract by damage reduction

                        opponent.Hitpoints =- InflictedDamage;                                                 //reduce opponent hit points by the amount of the damage inflicted
                        Console.WriteLine(Name + " hit " + opponent.Name + " for: " + InflictedDamage + "!");  //display the current creature name, opponent name, and the amount of damage inflicted
                    }
                    if (opponent.IsAlive()) opponent.Fight(this);                                              //if the opponent is still alive, then the opponent will fight back by calling Fight(...) method        
                }
            }
        }

        private int UseWeapon(Creature opponent)
        {
            var weaponDamage = 0;
            for (int i = 0; i < Weapon.NumAttacks(); i++)               //loop from 0 till the the max number of weapon attacks - 1 
            {
                var chanceOfHit = StaticRandom.Instance.Next(1, 21);                      //placeholder for random number generator that generates int value between 1 and 21 (inclusive)
                if (chanceOfHit > opponent.Armor.Strength)                            //if opponent creature armor strength is less than the penetration damage
                {
                    weaponDamage = StaticRandom.Instance.Next(1, Weapon.MaxDamage);         //generate weapon damage between 1 and max weapon damage (inclusive)
                }
            }
            return weaponDamage;
        }

        //Calculate the damage taken by a creature, set Hitpoints to 0 if the damage is >= creature hit points
        public void TakesDamage(int damage)
        {
            Hitpoints -= damage;
            if (Hitpoints < 0)
            {
                Hitpoints = 0;
            }
        }

        //RestoreHealth(...) increases the amount of hit points
        public void RestoreHealth(int amount)
        {
            Hitpoints += amount;
        }
    }
}