using System;
using System.Collections.Generic;

namespace Adventure_Game_407
{
    public class Room
    {
        /*
         * Room that contains a loot container (array) that contains 0 or more
         * piece of loot that could be Treasure, Weapon, Armor, Health Potion,
         * or Magic Dampening Scroll.
         *
         * Each room could be only one of the following:
         * ' ' - empty
         * 'R' - regular
         * 'M' - magic dampening
         * 'E' - entry
         * 'X' - exit
         * 'B' - Boss
         */
        public List<Item> Loot { get; }

        private char _type;
        public char Type
        {
            get { return _type; }
            set
            {
                if (value == 'R' || value == 'M' || value == 'E' || value == 'X' ||
                    value == ' ' || value == 'B')
                {
                    _type = value;
                }
            }
        }

        public int Row { get; }
        public int Col { get;  }
        
        // todo: add monster

        public Room(char type, int row, int col)
        {
            /*
             * Constructor for room with specified type.
             * Randomly generates loot
             */
            Row = row;
            Col = col;
            Loot = new List<Item>();
            type = Char.ToUpper(type);
            if (type == 'R' || type == 'M' || type == 'E' || type == 'X' ||
                type == ' ' || type == 'B')
            {
                Type = type;
            }
            else
            {
                throw new ArgumentException("Invalid type");
            }

            GenerateLoot();
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
                var rollForTreasureAmt = StaticRandom.Instance.Next(100);
                Loot.Add(new Treasure(rollForTreasureAmt + 1));
                Loot.Add(new Weapon());
            }
            
            // Armor 5/30
            else if (rollForLoot >= 15 && rollForLoot < 20)
            {
                var rollForTreasureAmt = StaticRandom.Instance.Next(100);
                Loot.Add(new Treasure(rollForTreasureAmt + 1));
                Loot.Add(new Armor());
            }
            
            // Health Potion 5/30
            else if (rollForLoot >= 20 && rollForLoot < 25)
            {
                Loot.Add(new HealthPotion());
            }
            
            // Magic Dampening Scroll 5/30
            else
            {
                // Magic Dampening Scroll turns type into M, all magic abilites and weapons do not posses extra damage or swings
                Loot.Add(new Scroll());
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