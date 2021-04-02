namespace Adventure_Game_407
{  
    // Treasure Class
    public class Treasure : Item
    {
        private int GoldAmount { get; }

        // Treasure constructor that sets the GoldAmount
        public Treasure(int goldAmount)
        {
            GoldAmount = goldAmount;
        }
    }
}