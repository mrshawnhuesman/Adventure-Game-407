using System;

namespace Adventure_Game_407
{
    //creature class - super class of subclasses hero and monster
    public class Creature
    {
        protected Weapon Weapon { get; set; }    //creature weapon
        protected Armor Armor { get; set; }      //creature armor
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
            int penetrationDamage, index;
            while (IsAlive() && opponent.IsAlive())                                 //while loop that will run until one of creatures is not alive
            {
                for (index = 0; index < Weapon.NumAttacks(); index++)            //loop from 0 till the the max number of weapon attacks - 1
                {
                    penetrationDamage = StaticRandom.Instance.Next(1, 21);                      //placeholder for random number generator that generates int value between 1 and 21 (inclusive)
                    if (penetrationDamage > opponent.Armor.Strength)                                         //if opponent creature armor strength is less than the penetration damage
                    {
                        int inflictedDamage = StaticRandom.Instance.Next(1, Weapon.MaxDamage);               //generate weapon damage between 1 and max weapon damage (inclusive)
                        opponent.Hitpoints -= inflictedDamage;                                                        //reduce opponent hit points by the amount of the damage inflicted
                        Console.WriteLine(Name + " hit " + opponent.Name + " for: " + inflictedDamage + "!");   //display the current creature name, opponent name, and the amount of damage inflicted
                    }
                    if (opponent.IsAlive()) opponent.Fight(this);                                                     //if the opponent is still alive, then the opponent will fight back by calling Fight(...) method        
                }
            }
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