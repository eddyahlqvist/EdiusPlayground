
using System.Collections.Generic;

namespace EdiusPlayground.Adventure
{
    internal class WorldBuilder
    {
        public World BuildWorld()
        {
            // create rooms
            Room threshold = new(
                "The Threshold",
                "You stand in a quiet stone chamber.");

            Room hallway = new(
                "The Hallway",
                "A narrow hallway stretches into the gloom.");

            // connect rooms
            threshold.North = hallway;
            hallway.South = threshold;

            // package rooms
            List<Room> rooms = new()
            {
                threshold,
                hallway
            };

            // create world
            World world = new(threshold, rooms);

            return world;
            //return new World(threshold, rooms); // might change to this later
        }
    }
}
