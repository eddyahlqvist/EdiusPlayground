using System;

namespace EdiusPlayground.Adventure
{
    internal class WorldBuilder
    {
        public Room BuildWorld()
        {
            Room threshold = new(
                "The Threshold",
                "You stand in a quiet stone chamber. The air is still.");

            Room hallway = new(
                "The Hallway",
                "A narrow hallway stretches into the gloom.");

            threshold.North = hallway;
            hallway.South = threshold;            

            return threshold;
        }
    }
}
