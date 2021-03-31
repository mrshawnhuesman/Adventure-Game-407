using System;
using System.Collections.Generic;

namespace Adventure_Game_407
{
    public class MagicalMonster : Creature
    {
        public readonly int Aggression;
        public MagicalSkill OffensiveSkill { get; }
        public MagicalSkill DefensiveSkill { get; }
        public double DamageReductionBuff { get; set; }

        //MagicalMonster constructor that takes parameter name, weapon, armor, hitpoints, agression level, and monster type        
        public MagicalMonster(string name, Weapon weapon, Armor armor, int hitpoints, int aggression, MagicalSkill offensiveSkill, MagicalSkill defensiveSkill)
        {
            Name = name;
            Weapon = weapon;
            Armor = armor;
            MaxHitPoints = hitpoints;
            CurrentHitPoints = hitpoints;
            Aggression = aggression;
            DefensiveSkill = defensiveSkill;
            OffensiveSkill = offensiveSkill;        
        }        

        //Magical monster fight method
        //A creature will be defeated if either the current magical monster or the opponent's hitpoints dropped to 0
        public override void Fight(Creature opponent)        {          

            while (IsAlive() && opponent.IsAlive())                   //while loop that last until one of 2 creatures dies
            {
                Console.WriteLine("\n" + Name + "'s turn.");
                var attackDamage = 0;            
                Tuple<int, int> move;

                move = GenerateMove(opponent);          //generate random move                                 

                if (move.Item1 == 1)                    //if the name of the move is "weapon attack" then set attack damage with the weapon attack damage value returned by generateMove method
                {
                    attackDamage = move.Item2;
                    Console.WriteLine(Name + " cast offensive " + OffensiveSkill.Name + " for " + attackDamage + " damage");
                }
                else if (move.Item1 == 2)               //else set the damage reduction value to the damage reduction calculation returned by generateMove method  
                {              
                    var damageReduction = move.Item2;
                    ((MagicalMonster)this).DamageReductionBuff = damageReduction;
                    Console.WriteLine(Name + " cast defensive " + DefensiveSkill.Name + " for " + DefensiveSkill.DefensiveBuff + "% defense next round.");
                }
                else                                     //else if the name of the move is "magic attack" then set attack damage with the magic attack damage value returned by generateMove method               
                {
                    attackDamage = move.Item2;
                }

                //if final damage is not 0, compute the inflicted damage and display result to console 
                if (attackDamage != 0)
                {
                    opponent.InflictDamage(attackDamage);
                    Console.WriteLine(opponent.Name + " takes " + attackDamage + " damage and now has " + opponent.CurrentHitPoints + " HP.");
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
                    if (opponent is Hero)
                    {
                        Console.WriteLine("HERO - " + opponent.Name + " has failed....GAME OVER");
                    }
                    else
                    {
                        Console.WriteLine(Name + " terminated " + opponent.Name);
                    }
                }
            }
        }

        //random move generator that will generate either a weapon attack, magic attack or magic reduction and compute its value
        //move 1 = magical attack move, move 2 = damage reduction buff, move 3 = weapon attack move
        public override Tuple<int, int> GenerateMove(Creature opponent)
        {
            var generateRandomMove = StaticRandom.Instance.Next(1, 4);                   //generate random number 1 - 3
            int moveValue;                          
            Tuple<int, int> move;                                                        //placeholder that will hold the type of move (1 = magical attack, 2 defensive buff) and the value of the move (attack damage or buff value)                  

            if (generateRandomMove == 1)          //if random number is 1, generate move named "magic attack" and compute attack damage
            {
                moveValue = OffensiveSkill.OffensiveDamage;
                move = new Tuple<int, int>(1, moveValue);
            }
            else if (generateRandomMove == 2)     //if random number is 2, generate move named "damage reduction" and compute damageReduction  
            {
                moveValue = DefensiveSkill.DefensiveBuff;
                move = new Tuple<int, int>(2, moveValue);
            }
            else                                 //else, generate a move name "weapon attack" and compute attack damage
            {
                moveValue = UseWeapon(opponent);
                move = new Tuple<int, int>(3, moveValue);
            }   
            return move;
        }        
    }
}