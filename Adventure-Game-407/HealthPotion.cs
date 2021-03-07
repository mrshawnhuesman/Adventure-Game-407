using Microsoft.Win32;

namespace Adventure_Game_407
{
    public class HealthPotion : Treasure
    {
        public int RestoreAmount { get; private set; }

        public override void Use()
        {
            Owner.RestoreHealth(RestoreAmount);
        }
    }
}