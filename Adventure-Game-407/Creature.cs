using System;

namespace Adventure_Game_407
{
    //creature class - super class of subclasses hero, monster, and magical monster
    public abstract class Creature
    {
        public Weapon Weapon { get; protected set; }        //creature weapon
        public Armor Armor { get; protected set; }          //creature armor
        public int MaxHitPoints { get; protected set; }     //creature maximum hit points
        public int CurrentHitPoints { get; protected set; } //creature current hit points
        public string Name { get; set; }        //creature name
        public Room Room { get; set; }          //creature current room location              
      
        //Abstract Fight(...) method will compute the result of a fight between 2 creatures
        public abstract void Fight(Creature opponent);
        
        //Abstract method for random move generator that will generate either a weapon attack, magic attack or magic reduction and compute, return its value        
        public abstract Tuple<int, int> GenerateMove(Creature opponent);

        //IsAlive(...) returns the alive status; returns true if creature hitpoints > 0, else return false
        public bool IsAlive() => CurrentHitPoints > 0;

        //Restore magical monster health point
        public void RestoreHealth(int amount)
        {
            CurrentHitPoints += amount;
            Console.WriteLine("Restoring " + amount + " hit points");
            if (CurrentHitPoints > MaxHitPoints)
            {
                CurrentHitPoints = MaxHitPoints;
            }        
        }

        //method that will compute and return the weapon damage based on the opponent armor strength and current creature weapon
        //also displays computation results to the console
        public int UseWeapon(Creature opponent)
        {          
            int accumulatedWeaponDamage = 0;
            int totalWeaponNumAttacks = Weapon.NumAttacks + Weapon.NumAttackBuff;
            for (int i = 0; i < totalWeaponNumAttacks; i++)                 //loop from 0 till the the max number of weapon attacks - 1
            {
                var chanceOfHit = StaticRandom.Instance.Next(1, 21);                  //placeholder for random number generator that generates int value between 1 and 21 (inclusive)
                if (chanceOfHit > opponent.Armor.Strength)                            //if opponent creature armor strength is less than the penetration damage
                {
                    var weaponDamage = Weapon.MaxDamage + Weapon.DamageBuff;
                    int randomWeaponDamage = StaticRandom.Instance.Next(1, weaponDamage);   //generate weapon damage between 1 and (combined value of max weapon damage and weapon damage buff)
                    Console.WriteLine(Name + " is attacking " + opponent.Name + " with " + Weapon.Name + " for "
                                      + randomWeaponDamage + " damage.");
                    accumulatedWeaponDamage += randomWeaponDamage;                         //total weapon damage after calculation  
                }
                else                                                                  //if the chance of hit is smaller than the opponent armor's strength, attack miss
                {
                    Console.WriteLine(Name + " is attacking " + opponent.Name + " with " + Weapon.Name + " for 0 damage. ");                      
                }
            }
            return accumulatedWeaponDamage;        //returns the total weapon damage
        }

        //Calculate the damage taken by a creature, set Hitpoints to 0 if the damage is >= creature hit points
        public void InflictDamage(int damage)
        {
            CurrentHitPoints -= damage;
            if (CurrentHitPoints < 0)
            {
                CurrentHitPoints = 0;
            }
        }
    }
}