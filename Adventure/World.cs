
using System.Collections.Generic;

namespace EdiusPlayground.Adventure
{
    internal class World
    {
        public Room StartingRoom { get; }
        public List<Room> Rooms { get; }

        public World(Room startingRoom, List<Room> rooms)
        {
            StartingRoom = startingRoom;
            Rooms = rooms;
        }        
    }
}
