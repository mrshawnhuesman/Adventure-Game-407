namespace Adventure_Game_407
{
    // Weapon Class
    public class Weapon : Item
    {
        public int NumAttacks { get; set; }
        public int MaxDamage { get; set; }

        public int DamageBuff => Owner.Room.Type == 'M' ? 0 : 2;

        public int NumAttackBuff { get; set; }
        public bool IsMagical { get; set; }

        // Default constructor that will automatically generate damage, number of attacks, type, name
        public Weapon()
        {
            var rollSwingsPerTurn = StaticRandom.Instance.Next(2);
            NumAttacks = rollSwingsPerTurn + 1;
            var rollDamage = StaticRandom.Instance.Next(5);
            MaxDamage = rollDamage + 1;
            var rollIsMagical = StaticRandom.Instance.Next(2);
            if (rollIsMagical == 1)
            {
                IsMagical = true;
            }
            else
            {
                IsMagical = false;
            }
            GenerateName();
        }

        //Weapon constructor that automatically assign the weapon name
        public Weapon(int numAttacks, int maxDamage, bool isMagical)
        {
            NumAttacks = numAttacks;
            MaxDamage = maxDamage;
            IsMagical = isMagical;
            GenerateName();
            if (IsMagical) 
                Name = "Magic " + Name;
        }

        //Natural Weapon constructor that takes 4 parameters : number of attacks, max weapon damage, weapon type, and weapon name
        public Weapon(int numAttacks, int maxDamage, bool isMagical, string name)
        {
            NumAttacks = numAttacks;
            MaxDamage = maxDamage;
            IsMagical = isMagical;
            Name = name;
            
        }

        private void GenerateName()
        {
            var miscellaneousWeaponNames = new []
            {
                "Handheld Fan",
                "Dr. Fox's Pen",
                "Computer",
                "Computer Mouse",
                "COBOL: The Programming Language",
                "NullPointer ExcepZion"
            };
            
            if (NumAttacks == 1 && MaxDamage < 3 && IsMagical == false)
                Name = "Gummy Bear";
            
            else if (NumAttacks == 1 && MaxDamage < 3 && IsMagical == false)
                Name = "Bread Stick";

            else if (NumAttacks == 2 && MaxDamage == 3 || MaxDamage == 4 && IsMagical == false)
                Name = "Hammer";

            else if (NumAttacks == 1 && MaxDamage == 3 || MaxDamage == 4 && IsMagical == false)
                Name = "Water Gun";

            else if (NumAttacks == 2 && MaxDamage == 3 || MaxDamage == 4 && IsMagical == false)
                Name = "Lawn Mower";

            else if (NumAttacks == 1 && MaxDamage == 3 || MaxDamage == 4 && IsMagical)
                Name = "Iron Fist";

            else if (NumAttacks == 2 && MaxDamage == 3 || MaxDamage == 4 && IsMagical)
                Name = "Sword";
            
            else if (NumAttacks == 1 && MaxDamage == 5 && IsMagical)
                Name = "Staff";
            
            else if (NumAttacks == 2 && MaxDamage == 5 && IsMagical == false)
                Name = "COVID-19";
            
            else if (NumAttacks == 2 && MaxDamage == 5 && IsMagical)
                Name = "Economic Recession";

            else
            {
                var rollName = StaticRandom.Instance.Next(miscellaneousWeaponNames.Length);
                Name = miscellaneousWeaponNames[rollName];
            }
        }
        
        public int CompareTo(Weapon weapon)
        {
            var currentIsMagical = IsMagical;
            var currentNumAttacks = NumAttacks;
            var currentMaxDamage = MaxDamage;
            
            // IsMagical carries most weight
            if (currentIsMagical && weapon.IsMagical == false)
                return 1;
            if (weapon.IsMagical && currentIsMagical == false)
                return -1;
            // if isMagical is true for both, or false for both, compare MaxDamage and NumAttacks
            if (currentNumAttacks > weapon.NumAttacks && currentMaxDamage > weapon.MaxDamage)
                return 1;
            if (weapon.NumAttacks > currentNumAttacks && weapon.MaxDamage > currentMaxDamage)
                return -1;
            // MaxDamage carries second most weight
            if (currentMaxDamage > weapon.MaxDamage)
                return 1;
            if (weapon.MaxDamage > currentMaxDamage)
                return -1;
            // NumAttacks carries the least weight
            if (currentNumAttacks > weapon.NumAttacks)
                return 1;
            if (weapon.NumAttacks > currentNumAttacks)
                return -1;
            
            return 0;
        }

        public override void Use()
        {
            var ownerWeapon = Owner.Weapon;
            Owner.Weapon = this;
            if (Owner is Hero)
            {
                ((Hero) Owner).Inventory.Add(ownerWeapon);
            }
            else
            {
                ownerWeapon.Drop();
            }
        }
    }
}