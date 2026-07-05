using EdiusPlayground.Core;
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

        public CommandResult HandleDirection(Direction dir, Player player)
        {
            switch (dir)
            {
                case Direction.North:
                    if (player.CurrentRoom?.North != null)
                    {
                        player.CurrentRoom = player.CurrentRoom.North;
                    }
                    else
                    {
                        Console.WriteLine("You can't go that way.");
                    }

                    return CommandResult.Continue;

                case Direction.South:
                    if (player.CurrentRoom?.South != null)
                    {
                        player.CurrentRoom = player.CurrentRoom.South;
                    }
                    else
                    {
                        Console.WriteLine("You can't go that way.");
                    }

                    return CommandResult.Continue;

                case Direction.East:
                    if (player.CurrentRoom?.East != null)
                    {
                        player.CurrentRoom = player.CurrentRoom.East;
                    }
                    else
                    {
                        Console.WriteLine("You can't go that way.");
                    }

                    return CommandResult.Continue;

                case Direction.West:
                    if (player.CurrentRoom?.West != null)
                    {
                        player.CurrentRoom = player.CurrentRoom.West;
                    }
                    else
                    {
                        Console.WriteLine("You can't go that way.");
                    }

                    return CommandResult.Continue;

                default:
                    Console.WriteLine("Unknown command.");
                    return CommandResult.Continue;
            }
        }
    }
}
