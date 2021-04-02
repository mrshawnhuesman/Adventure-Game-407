namespace Adventure_Game_407
{  
    // Treasure Class
    public class Treasure : Item
    {
        public int GoldAmount { get; set; }

        // Treasure constructor that sets the GoldAmount
        public Treasure(int goldAmount)
        {
            GoldAmount = goldAmount;
            Name = "Gold coins";
        }
    }
}