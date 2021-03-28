using System;

namespace Adventure_Game_407
{
    public class Dungeon
    {
        /*
         * Contains a 2d array of procedurally generated rooms with
         * some magic dampening rooms, regular rooms, 1 entry, and 1 exit
         */
        public int Rows { get; private set; }
        public int Cols { get; private set; }
        public Room[,] Rooms { get; }
        public int StartRow { get; }
        public int StartCol { get; }
        
        private readonly Random _random = new Random();
        private int[] _lastVisited;

        public Dungeon()
        {
            /*
             * Default Dungeon constructor that generates 8 by 8 dungeon rooms
             */
            Rows = 8;
            Cols = 8;
            Rooms = new Room[Rows, Cols];
            var visited = new bool[Rows, Cols];
            
            // initialize all rooms to empty
            // initialize all visited to false
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    Rooms[i, j] = new Room(' ');
                    visited[i, j] = false;
                }
            }
            
            // Randomly select a starting location (entry)
            StartRow = StaticRandom.Instance.Next(Rows - 1);
            StartCol = StaticRandom.Instance.Next(Cols - 1);
            _lastVisited = new[] {StartRow, StartCol};
            
            // procedurally create rooms
            CreateRooms(Rooms, StartRow, StartCol, visited, 0);
            
            // Mark exit
            Rooms[_lastVisited[0], _lastVisited[1]] = new Room('X');
            
            // Mark entry
            Rooms[StartRow, StartCol] = new Room('E');
        }

        private void CreateRooms(Room[,] rooms, int row, int col, bool[,] visited, int roomCount)
        {
            /*
             * Procedurally creates dungeon rooms recursively using Depth-First Search (DFS)
             * and puts them into an array.
             * The first 8 connected rooms are guaranteed to be in the dungeon. Each consecutive
             * room has a 25% chance to be in the dungeon. Each room selected to be in the dungeon
             * has a 14.3% chance to be magic dampening.
             * Keeps track of the last visited spot in the array to later make an exit
             */
            
            Rows = rooms.GetLength(0);
            Cols = rooms.GetLength(1);
            const int minRoomCount = 8;

            // index out of bounds or room has been visited
            if (row < 0 || col < 0 || row >= Rows || col >= Cols || visited[row, col]) return;
            
            // chance to pick a room = 1/4
            var chanceToPickRoom = StaticRandom.Instance.Next(4);
            visited[row, col] = true;

            if (chanceToPickRoom == 1 || roomCount < minRoomCount)
            {
                // chance to pick magic dampening room = 1/7
                var chanceToPickMagicRoom = StaticRandom.Instance.Next(7);
                if (chanceToPickMagicRoom == 1)
                {
                    // Magic-dampening room
                    Rooms[row, col] = new Room('M');
                }
                else
                {
                    // Regular room
                    Rooms[row, col] = new Room('R');
                }

                roomCount += 1;

                // keep track of last visited location to make an exit
                _lastVisited[0] = row;
                _lastVisited[1] = col;

                // go down
                CreateRooms(Rooms, row, col - 1, visited, roomCount);
                // go right
                CreateRooms(Rooms, row + 1, col, visited, roomCount);
                // go left
                CreateRooms(Rooms, row - 1, col, visited, roomCount);
                // go up
                CreateRooms(Rooms, row, col + 1, visited, roomCount);
            }
        }
    }
}