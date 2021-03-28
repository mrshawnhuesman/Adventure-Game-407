using System;
using System.Collections.Generic;

namespace Adventure_Game_407
{
    public class Room
    {
        public List<Item> Loot { get; } = new List<Item>();
        public char Type { get; }

        private Random _random = new Random();
        
        
        public Room(char type)
        {
            type = Char.ToUpper(type);
            if (type == 'R' || type == 'M' || type == 'E' || type == 'X' ||
                type == ' ')
            {
                Type = type;
            }
            else
            {
                throw new ArgumentException("Invalid type");
            }

            if (type != ' ')
            {
                GenerateLoot();
            }
        }

        private void GenerateLoot()
        {
            /* Randomly generate loot that will be in room.
             * The first loot has a 1/2 chance to be in the room.
             * Each consecutive loot has a 1/2 chance to be in
             * the room given the previous loot was in the room.
             * This means there is a 1/n*2 chance for there to be n loot.
             *
             * For each piece of loot, here are the chances for the type:
             * 10/30 - Treasure (1-100)
             *  5/30 - Weapon  
             *  5/30 - Armor
             *  5/30 - Health Potion
             *  5/30 - Magic Dampening Scroll
             */
    
            var chanceAtLoot = StaticRandom.Instance.Next(2);
            // failed loot roll
            if (chanceAtLoot == 0) return;
            
            
            var rollForLoot = StaticRandom.Instance.Next(31);
            // Treasure 10/30
            if (rollForLoot >= 0 && rollForLoot < 10)
            {
                // treasure amount 1-100
                var rollForTreasureAmt = StaticRandom.Instance.Next(100);
                Loot.Add(new Treasure(rollForTreasureAmt + 1));
            }
            
            // Weapon 5/30
            else if (rollForLoot >= 10 && rollForLoot < 15)
            {
                Loot.Add(new Weapon());
            }
            
            // Armor 5/30
            else if (rollForLoot >= 15 && rollForLoot < 20)
            {
                //todo
            }
            
            // Health Potion 5/30
            else if (rollForLoot >= 20 && rollForLoot < 25)
            {
                Loot.Add(new HealthPotion());
            }
            
            // Magic Dampening Scroll 5/30
            else
            {
                //todo
            }
            
            GenerateLoot();
        }

        public bool isEmpty()
        {
            if (Type == ' ')
            {
                return true;
            }

            return false;
        }
    }
}