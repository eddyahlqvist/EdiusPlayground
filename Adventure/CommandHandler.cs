using System;

namespace EdiusPlayground.Adventure
{
    internal class CommandHandler
    {
        public bool TryGetDirection(string input, out Direction direction)
        {
            switch (input)
            {
                case "north":
                case "n":
                    direction = Direction.North;
                    return true;

                case "south":
                case "s":
                    direction = Direction.South;
                    return true;

                case "east":
                case "e":
                    direction = Direction.East;
                    return true;

                case "west":
                case "w":
                    direction = Direction.West;
                    return true;

                default:
                    direction = default;
                    return false;
            }
        }

        public CommandResult HandleCommand(string command)
        {
            if (command == "exit")
            {
                return CommandResult.Exit;
            }

            else
            {
                return CommandResult.Continue;
            }
        }

        public CommandResult HandleDirection(Direction direction, Player player)
        {
            Room? nextRoom = direction switch
            {
                Direction.North => player.CurrentRoom?.North,
                Direction.South => player.CurrentRoom?.South,
                Direction.East => player.CurrentRoom?.East,
                Direction.West => player.CurrentRoom?.West,
                _ => null
            };

            if (nextRoom == null)
            {
                Console.WriteLine("You can't go that way.");
                return CommandResult.Continue;
            }

            player.CurrentRoom = nextRoom;
            return CommandResult.Continue;
        }
    }
}
