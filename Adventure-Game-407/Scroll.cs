namespace Adventure_Game_407
{
    // Scroll class
    public class Scroll : Item
    {
        // turns the Room the scroll is in to type 'M' disabling all magic abilities
        public override void Use()
        {
            RoomOccupied.Type = 'M';
        }
    }
}