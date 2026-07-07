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

        public CommandResult HandleCommand(string verb, string argument, Player player)
        {
            switch (verb)
            {
                case "look":
                    LookCommand(argument, player);
                    return CommandResult.Continue;

                case "glance":
                    GlanceCommand(player);
                    return CommandResult.Continue;

                case "yell":
                    YellCommand(argument);
                    return CommandResult.Continue;

                case "shout":
                    ShoutCommand(argument);
                    return CommandResult.Continue;

                case "save":
                    SaveCommand();
                    return CommandResult.Continue;

                case "score":
                    ScoreCommand(player);
                    return CommandResult.Continue;

                case "exit":
                    return CommandResult.Exit;

                default:
                    Console.WriteLine("Unknown command.");
                    return CommandResult.Continue;
            }
        }

        public CommandResult HandleDirection(Direction direction, Player player)
        {
            Room? nextRoom = direction switch
            {
                Direction.North => player.CurrentRoom.North,
                Direction.South => player.CurrentRoom.South,
                Direction.East => player.CurrentRoom.East,
                Direction.West => player.CurrentRoom.West,
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

        private void LookCommand(string argument, Player player)
        {
            if (argument == "")
            {
                Console.WriteLine(player.CurrentRoom.Description);
            }
            else if (argument == "me")
            {
                Console.WriteLine($"You see {player.Name}."); // this will be improved later when player class grows
            }
            else
            {
                Console.WriteLine($"You can't seem to find {argument}.");
            }
        }

        private void GlanceCommand(Player player)
        {
            Console.WriteLine(player.CurrentRoom.Name);
        }

        private void YellCommand(string argument)
        {
            if (argument == "")
            {
                Console.WriteLine("Yell what?");
            }
            else
            {
                string output = argument.ToUpperInvariant();
                Console.WriteLine($"You yell loudly: {output}!!");
            }
        }

        private void ShoutCommand(string argument)
        {
            if (argument == "")
            {
                Console.WriteLine("Shout what?");
            }
            else
            {
                string output = argument.ToUpperInvariant();
                Console.WriteLine($"You shout from the bottom of your lungs: {output}");
            }
        }

        private void ScoreCommand(Player player)
        {
            Console.WriteLine($"HP: {player.HP}");
        }

        private void SaveCommand()
        {
            // temporary fake save, will work on this later
            Console.WriteLine("Saving...");
        }
    }
}
