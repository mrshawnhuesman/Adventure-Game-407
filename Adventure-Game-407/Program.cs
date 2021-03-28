using System;

namespace Adventure_Game_407
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Dungeon dungeon = new Dungeon();
            var rooms = dungeon.Rooms;
            for (int i = 0; i < dungeon.Rows; i++)
            {
                for (int j = 0; j < dungeon.Cols; j++) {
                    Console.Write(rooms[i, j]);
                }
                Console.WriteLine();
            }
        }

    }
}