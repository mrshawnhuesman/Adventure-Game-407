using System;
#pragma warning disable IDE0038

namespace Adventure_Game_407
{
    //creature class - super class of subclasses hero, monster, and magical monster
    public class Creature
    {
        protected Weapon Weapon { get; set; }   //creature weapon
        public Armor Armor { get; set; }        //creature armor
        protected int Hitpoints { get; set; }   //creature hit points
        public string Name { get; set; }        //creature name
        public Room Room { get; set; }          //creature current room location
    
        //IsAlive(...) returns the alive status; returns true if creature hitpoints > 0, else return false
        public bool IsAlive()
        {
            return Hitpoints > 0;
        }

        //Fight method will compute the result of a fight between 2 creatures
        //A creature will be defeated if the hitpoints dropped to 0
        public void Fight(Creature opponent)
        {
            MagicalMonster currentMagicalMonster = null, opponentMagicalMonster = null;

            if (this is MagicalMonster)
            {
                currentMagicalMonster = (MagicalMonster)this;
            }
            if (opponent is MagicalMonster)
            {
                opponentMagicalMonster = (MagicalMonster)opponent;
            }                      

            while (IsAlive() && opponent.IsAlive())                   //while loop that last until one of 2 creatures dies
            {
                Console.WriteLine(Name + "'s turn.");
                var attackDamage = 0;
                var finalDamage = 0;
                var damageReduction = 0;
                Tuple<string, int> move;
                string moveName;
        
                move = GenerateMove(this, opponent);                   //generate random move
                moveName = move.Item1;                                 //place holder for random move name    

                if (moveName.Equals("weapon attack"))                  //if the name of the move is "weapon attack" then set attack damage with the weapon attack damage value returned by generateMove method
                {
                    attackDamage = move.Item2;
                }
                else if (moveName.Equals("magic attack"))               //else if the name of the move is "magic attack" then set attack damage with the magic attack damage value returned by generateMove method
                {
                    attackDamage = move.Item2;
                    Console.WriteLine(Name + " cast offensive " + currentMagicalMonster.OffensiveSkill.Name
                    + " for " + attackDamage + " damage");
                }
                else                                                    //else set the damage reduction value to the damage reduction calculation returned by generateMove method
                {
                    damageReduction = move.Item2;
                    ((MagicalMonster)this).CurrentMagicDefense = damageReduction;                   
                    Console.WriteLine(Name + " cast defensive " + currentMagicalMonster.DefensiveSkill.Name + " for " + currentMagicalMonster.DefensiveSkill.DefensiveBuff  + "% defense next round.");
                }

                //if the opponent is magical monster and its magic defense is > 0, calculate final damage = attack damage - current magic defense 
                if (opponentMagicalMonster != null && opponentMagicalMonster.CurrentMagicDefense > 0)
                {
                    finalDamage = attackDamage - (int)(attackDamage * (opponentMagicalMonster.CurrentMagicDefense / 100));
                    opponentMagicalMonster.CurrentMagicDefense = 0;     //reset opponent magic defense to 0 after being used to reduce damage
                }

                //if final damage is not 0, compute the inflicted damage and display result to console 
                if (finalDamage != 0)
                {
                    opponent.TakesDamage(finalDamage);
                    Console.WriteLine(opponent.Name + " takes " + finalDamage + " damage and now has " + opponent.Hitpoints + " HP.");
                }

                //if the opponent used the defensive skill that reduces damage 100% then display the message to console
                else 
                {
                    Console.WriteLine(opponent.Name + " takes no damage from " + Name + "" + "'s attacks.");
                }




                //if the opponent still alive then opponent will fight back
                if (opponent.IsAlive()) opponent.Fight(this);

                //if the opponent dies, display win message to console
                else
                {
                    Console.WriteLine(Name + " wins!");
                }
            }
        }
        

        //method that will compute and return the weapon damage based on the opponent armor strength and current creature weapon
        //also displays computation results to the console
        private int UseWeapon(Creature opponent)
        {
            var weaponDamage = 0;
            var totalWeaponDamage = 0;
            for (int i = 0; i < Weapon.NumAttacks; i++)               //loop from 0 till the the max number of weapon attacks - 1
            {
                var chanceOfHit = StaticRandom.Instance.Next(1, 21);                  //placeholder for random number generator that generates int value between 1 and 21 (inclusive)
                if (chanceOfHit > opponent.Armor.Strength)                            //if opponent creature armor strength is less than the penetration damage
                {
                    weaponDamage = StaticRandom.Instance.Next(1, Weapon.MaxDamage);   //generate weapon damage between 1 and max weapon damage (inclusive)
                    Console.WriteLine(Name + " is attacking " + opponent.Name + " with " + Weapon.Name + " for "
                                      + weaponDamage + " damage.");
                    totalWeaponDamage += weaponDamage;                                //total weapon damage after calculation  
                }                
            }

            if (weaponDamage == 0)
            {
                Console.WriteLine(Name + "'s attack missed. ");
            }
            return weaponDamage;        //returns the total weapon damage
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

        //random move generator that will generate either a weapon attack, magic attack or magic reduction and compute its value
        public Tuple<string, int> GenerateMove(Creature currentCreature, Creature opponent)
        {               
            var generateRandomMove = StaticRandom.Instance.Next(1, 4);                   //generate random number 1 - 3
            Tuple<string, int> move;                                    
            int attackDamage, damageReduction;
            MagicalMonster magicalMonster;         
           
            if (currentCreature is MagicalMonster && generateRandomMove == 1)            //if current creature is a magical monster and random number is 1, generate move named "magic attack" and compute attack damage
            {
                magicalMonster = (MagicalMonster)currentCreature;
                attackDamage = magicalMonster.OffensiveSkill.OffensiveDamage;
                move = new Tuple<string, int>("magic attack", attackDamage);

            } else if (currentCreature is MagicalMonster && generateRandomMove == 2)     //if current creature is a magical monster and random number is 2, generate move named "damage reduction" and compute damageReduction  
            {
                magicalMonster = (MagicalMonster)currentCreature;
                damageReduction = magicalMonster.DefensiveSkill.DefensiveBuff;
                move = new Tuple<string, int>("damage reduction", damageReduction);
            } else                                                                      //if current creature is a hero or a monster, generate a move name "weapon attack" and compute attack damage
            {     
                attackDamage = UseWeapon(opponent);
                move =  new Tuple<string, int>("weapon attack", attackDamage);
            }

            return move;
        }

            /*
            if (generateMove == 1)                      //if generate move return 1, magical monster will use weapon attack
            {
                
            }
            else if (generateMove == 2)                 //if generate move return 2, magical monster casts magical attack
            {
                attackDamage = currentMagicalMonster.OffensiveSkill.OffensiveDamage;
                Console.WriteLine(Name + " cast offensive " + currentMagicalMonster.OffensiveSkill.Name
                + " for " + attackDamage + " damage");
            }
            else                                        //else magical monster casts defensive skill
            {

                currentMagicalMonster.CurrentMagicDefense = currentMagicalMonster.DefensiveSkill.DefensiveValue;
                usedDefense = true;
                Console.WriteLine(Name + " cast defensive " + currentMagicalMonster.DefensiveSkill.Name
                + " for " + currentMagicalMonster.DefensiveSkill.DefensiveValue * 100 + "% defense next round.");
            }
        }*/
    }
}