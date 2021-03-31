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
            int chanceOfHit, index, WeaponDamage, InflictedDamage, TotalDamage, DamageReduction = 0;
            bool usedDefense = false;
            MagicalMonster currentMagicalMonster = null, opponentMagicalMonster = null;
     
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
                Console.WriteLine(Name + "'s turn.");
                var attackDamage = 0;

                // current fighter is magic
                if (currentMagicalMonster != null)
                {
                    currentMagicalMonster.CurrentMagicDefense = 0;
                    usedDefense = false;

                    // number 1-3
                    var generateMove = StaticRandom.Instance.Next(1, 4);

                    if (generateMove == 1)
                    {
                        attackDamage = UseWeapon(opponent);
                    }
                    else if (generateMove == 2)
                    {
                        attackDamage = currentMagicalMonster.OffensiveSkill.OffensiveDamage;
                        Console.WriteLine(Name + " cast offensive " + currentMagicalMonster.OffensiveSkill.Name
                        + " for " + attackDamage + " damage");
                    }
                    else
                    {
                        
                        currentMagicalMonster.CurrentMagicDefense = currentMagicalMonster.DefensiveSkill.DefensiveValue;
                        usedDefense = true;
                        Console.WriteLine(Name + " cast defensive " + currentMagicalMonster.DefensiveSkill.Name
                        + " for " + currentMagicalMonster.DefensiveSkill.DefensiveValue * 100 + "% defense next round.");
                    }
                }
                // current fighter is not magic
                else
                {
                    attackDamage = UseWeapon(opponent);
                }

                if (opponentMagicalMonster != null)
                {
                    attackDamage = attackDamage -
                                      (int) (attackDamage * opponentMagicalMonster.CurrentMagicDefense);
                }

                if (attackDamage != 0)
                {
                    opponent.TakesDamage(attackDamage);
                    Console.WriteLine(opponent.Name + " takes " + attackDamage + " damage and now has " +
                                      opponent.Hitpoints + " HP.");
                }
                else if (!usedDefense)
                {
                    Console.WriteLine(opponent.Name + " takes no damage from " + Name + "" +
                                      "'s attacks.");
                }

                if (opponent.IsAlive()) opponent.Fight(this);
                else
                {
                    Console.WriteLine(Name + " wins!");
                }
            }
        }

        private int UseWeapon(Creature opponent)
        {
            var weaponDamage = 0;
            var totalWeaponDamage = 0;
            for (int i = 0; i < Weapon.NumAttacks; i++)               //loop from 0 till the the max number of weapon attacks - 1 
            for (int i = 0; i < Weapon.NumAttacks(); i++)               //loop from 0 till the the max number of weapon attacks - 1 
            {
                var chanceOfHit = StaticRandom.Instance.Next(1, 21);                      //placeholder for random number generator that generates int value between 1 and 21 (inclusive)
                if (chanceOfHit > opponent.Armor.Strength)                            //if opponent creature armor strength is less than the penetration damage
                {
                    weaponDamage = StaticRandom.Instance.Next(1, Weapon.MaxDamage);         //generate weapon damage between 1 and max weapon damage (inclusive)
                    Console.WriteLine(Name + " is attacking " + opponent.Name + " with " + Weapon.Name + " for "
                                      + weaponDamage + " damage.");
                    totalWeaponDamage += weaponDamage;
                }
                
            }

            if (weaponDamage == 0)
            {
                Console.WriteLine(Name + "'s attack missed. ");
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