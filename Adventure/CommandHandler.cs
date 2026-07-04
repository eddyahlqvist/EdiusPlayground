using System;

namespace EdiusPlayground.Adventure
{
    internal class CommandHandler
    {        
        public CommandResult Handle(string input, Player player)
        {
            switch (input)
            {
                case "exit":
                    return CommandResult.Exit;

                case "north":
                case "n":
                    if (player.CurrentRoom?.North != null)
                    {
                        player.CurrentRoom = player.CurrentRoom.North;
                    }
                    else
                    {
                        Console.WriteLine("You can't go that way.");
                    }

                    return CommandResult.Continue;

                case "south":
                case "s":
                    if (player.CurrentRoom?.South != null)
                    {
                        player.CurrentRoom = player.CurrentRoom.South;
                    }
                    else
                    {
                        Console.WriteLine("You can't go that way.");
                    }

                    return CommandResult.Continue;

                case "east":
                case "e":
                    if (player.CurrentRoom?.East != null)
                    {
                        player.CurrentRoom = player.CurrentRoom.East;
                    }
                    else
                    {
                        Console.WriteLine("You can't go that way.");
                    }

                    return CommandResult.Continue;

                case "west":
                case "w":
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
