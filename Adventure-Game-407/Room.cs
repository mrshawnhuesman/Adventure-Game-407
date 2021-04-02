using System;
using System.Collections.Generic;

namespace Adventure_Game_407
{
    // Room class 
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
        
        public Creature Monster { get; set; }

        public Room(char type, int row, int col)
        {
            /*
             * Constructor for room with specified type.
             * Randomly generates 0 or more loot
             * Randomly generates 0-1 monster(s)
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
            CheckForMonster();
        }
        
        // Returns true is type is empty
        public bool IsEmpty()
        {
            if (Type == ' ')
            {
                return true;
            }

            return false;
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
             *  5/30 - Weapon  + Treasure (1-100)
             *  5/30 - Armor   + Treasure (1-100)
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
            
            // Weapon 5/30 + Treasure
            else if (rollForLoot >= 10 && rollForLoot < 15)
            {
                var rollForTreasureAmt = StaticRandom.Instance.Next(100);
                Loot.Add(new Treasure(rollForTreasureAmt + 1));
                Loot.Add(new Weapon());
            }
            
            // Armor 5/30 + Treasure
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
            CheckForMonster();
        }

        // Rolls a 0 or 1, and if 1 calls CreateMonster(), otherwise does nothing 
        private void CheckForMonster()
        {
            var rollForMonster = StaticRandom.Instance.Next(2);
            // 'found' monster
            if (rollForMonster == 1)
            {
                CreateMonster();
            }
        }

        private void CreateMonster()
        {
            // check for monster name
            var monsterNames = new []
            {
                "Seth Adjei", "Shahid Noor", "Aziz Bahha", "Chris Brewer",
                "Alina Campan", "Nicholas Caporusso", "Ankur Chattopadhyay",
                "Samuel Cho", "Scot Cunningham", "Maureen Doyle", 
                "Charles Frank", "Wei Hao", "Yi Hu", "Rasib Khan",
                "John Musgrave", "Gary Newell", "Ken Roth", "Emily Taylor",
                "Bradford Thomas", "Cynthia Thomas", "Marius Truta",
                "Anthony Tsetse", "James Walden", "Hongmei Wang", 
                "Jeff Ward", "Junxiu Zhou"
            };
            var rollNameIndex = StaticRandom.Instance.Next(monsterNames.Length);
            var monsterName = monsterNames[rollNameIndex];
            
            // check for monster hitpoints (10-75)
            var monsterHitpoints = StaticRandom.Instance.Next(10, 75 + 1);
            
            // check for monster aggression (1-3)
            var monsterAggression = StaticRandom.Instance.Next(1, 3 + 1);
            
            // check type of monster: physical or magical
            var rollForMonsterType = StaticRandom.Instance.Next(1 + 1);
            // physical monster
            if (rollForMonsterType == 0)
            {
                Monster = new PhysicalMonster(monsterName, new Weapon(), new Armor(), 
                    monsterHitpoints, monsterAggression );
            }
            // magical monster
            else
            {
                // check for magical offense and defense abilities
                var magicalOffenseAbilities = new []
                {
                    new MagicalSkill( "Musical Strike", "offensive", 5),
                    new MagicalSkill( "Snow Balls", "offensive", 10),
                    new MagicalSkill( "Winter Storm", "offensive", 15)
                };
                var magicalDefenseAbilities = new []
                {
                    new MagicalSkill( "Elemental Protection", "defensive", 10),
                    new MagicalSkill( "Greater Elemental Protection", "defensive", 20),
                };
                
                var rollOffenseIndex = StaticRandom.Instance.Next(magicalOffenseAbilities.Length);
                var monsterOffenseAbility = magicalOffenseAbilities[rollOffenseIndex];
                var rollDefenseIndex = StaticRandom.Instance.Next(magicalDefenseAbilities.Length);
                var monsterDefenseAbility = magicalDefenseAbilities[rollDefenseIndex];
                
                Monster = new MagicalMonster(monsterName, new Weapon(), new Armor(),
                    monsterHitpoints, monsterAggression, monsterOffenseAbility, 
                    monsterDefenseAbility  );
            }
        }

        // Returns true if Room contains a Monster
        public bool HasMonster()
        {
            if (Monster != null)
            {
                return true;
            }

            return false;
        }

        // Checks and returns the Monster Aggression
        public int CheckMonsterAggro()
        {
            if (HasMonster())
            {
                if (Monster is MagicalMonster)
                {
                    return ((MagicalMonster) Monster).Aggression;
                }
                else
                {
                    return ((PhysicalMonster) Monster).Aggression;
                }
            }
            else
            {
                return -1;
            }
        }
    }
}