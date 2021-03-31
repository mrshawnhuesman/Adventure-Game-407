using System;

namespace Adventure_Game_407.View
{
    public class CLIView
    {
        public void ShowDungeonMinimap(Dungeon dungeon, Hero hero)
        {
            var rooms = dungeon.Rooms;
            var currentRoom = hero.Room;
            for (int i = 0; i < dungeon.Rows; i++)
            {
                for (int j = 0; j < dungeon.Cols; j++) {
                    if (i == currentRoom.Row && j == currentRoom.Col)
                    {
                        Console.Write("+");
                    }
                    else
                    {
                        Console.Write(rooms[i, j].Type);
                    }
                }
                Console.WriteLine();
            }

            ShowRoomInformation(currentRoom);
        }

        public void ShowRoomInformation(Room room)
        {
            Console.WriteLine("Current Room Type: " + room.Type);
        }

        public void AskHeroForName(Hero hero) {
            Console.WriteLine("Enter Hero Name: ");
            var name = Console.ReadLine();
            hero.Name = name;
        }
        
        
    }
}