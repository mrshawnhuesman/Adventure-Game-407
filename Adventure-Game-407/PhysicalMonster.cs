using System;
#pragma warning disable IDE0038

namespace Adventure_Game_407
{
    public class PhysicalMonster : Creature
    {   
        private readonly int Aggression;    //monster agression level (1 is low, 3 is high)
        private readonly Weapon naturalWeapon = new Weapon (1, 2, false, "claw");   //default monster armor which is the natural armor
        private readonly Armor naturalArmor = new Armor (1, "tough skin");          //default monster armor which is the natural armor

        //Monster constructor that takes parameter name, equippable weapon, equippable armor, hitpoints, agression level, and monster type
        //Monster natural weapon values and natural armor values will be combined with equippable armor and equippable weapon
        //the name of the weapon or armor is based on the equippable armor or weapon
        public PhysicalMonster(string name, Weapon weapon, Armor armor, int hitpoints, int aggression)
        {
            Name = name;
            Weapon = weapon;
            Weapon.NumAttacks += naturalWeapon.NumAttacks;     
            Weapon.MaxDamage += naturalWeapon.MaxDamage;            
            Armor = armor;
            Armor.Strength += naturalArmor.Strength;
            MaxHitPoints = hitpoints;
            CurrentHitPoints = hitpoints;
            Aggression = aggression;
        }         

        //Implementation of physical monster fight method
        //A creature will be defeated if either the current monster or the opponent's hitpoints dropped to 0
        public override void Fight(Creature opponent)
        {
            while (IsAlive() && opponent.IsAlive())                   //while loop that last until one of 2 creatures dies
            {
                Console.WriteLine("\n" + Name + "'s turn.");
                var attackDamage = 0;
         
                Tuple<int, int> move;

                move = GenerateMove(opponent);        //generate random move                                 

                if (move.Item1 == 1)                  //if move type is 1 - weapon attack then set attack damage with the weapon attack damage value returned by generateMove method
                {
                    attackDamage = move.Item2;          
                }
                else                                  //else, move type is 2 - restore hit points the name of th set attack damage with the magic attack damage value returned by generateMove method
                {
                    var hitPointsToBeRestored = move.Item2;
                    Console.WriteLine(Name + " restoring " + hitPointsToBeRestored + " hit points.");
                    RestoreHealth(hitPointsToBeRestored);
                }
                
                //if the opponent is magical monster and its damage reduction buff defense is > 0 (has a defensive buff), calculate final damage = attack damage - current magic defense 
                if (opponent is MagicalMonster && ((MagicalMonster)opponent).DamageReductionBuff > 0)
                {
                    var damageReduction = (int)(attackDamage * ((MagicalMonster)opponent).DamageReductionBuff / 100);
                    attackDamage -= damageReduction;
                    ((MagicalMonster)opponent).DamageReductionBuff = 0;     //reset opponent damage reduction buff to 0 after being used to reduce damage
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
                        Console.WriteLine("HERO - " + opponent.Name + " has failed....GAME OVER");                        
                }
            }
        }

        //random move generator that will generate a move
        //move 1 = weapon attack move, move 2 = restore hitpoints
        public override Tuple<int, int> GenerateMove(Creature opponent)
        {
            var generateRandomMove = StaticRandom.Instance.Next(1, 3);                 //generate random number 1 - 3
            int moveValue;
            Tuple<int, int> move;                                                      //placeholder that will hold the type of move (1 = magical attack, 2 defensive buff) and the value of the move (attack damage or buff value)                  

            if (generateRandomMove == 1)          //if random move number is 1, compute damage from using weapon and return the type of move and the value of the damage
            {
                moveValue = UseWeapon(opponent);
                move = new Tuple<int, int>(1, moveValue);
            }
            else                                  //else, this is the monster move that will restore its hp, compute the random hit points to be restored and return the type of move and the value of the hitpoints to be restored
            {
                moveValue = StaticRandom.Instance.Next(1, 3);                         //generate random number 1 or 2  
                moveValue = (int) (moveValue / 10 * this.CurrentHitPoints);
                move = new Tuple<int, int>(2, moveValue);               
            }            
            return move;
        } 
    }
}