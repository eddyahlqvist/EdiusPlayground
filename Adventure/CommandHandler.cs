using EdiusPlayground.Core;
using System;

namespace EdiusPlayground.Adventure
{
    internal class CommandHandler
    {    
        public Direction GetDirection(string input)
        {
            input = input.Trim().ToLowerInvariant();

            switch (input)
            {
                case "north":
                case "n":
                    return Direction.North;

                case "south":
                case "s":                
                    return Direction.South;

                case "west":
                case "w":
                    return Direction.West;

                case "east":
                case "e":
                    return Direction.East;

                default:
                    return Direction.NoDirection;
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
