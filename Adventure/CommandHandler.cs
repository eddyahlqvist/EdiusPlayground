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
                    LookCommand(player);
                    return CommandResult.Continue;

                case "shout":
                case "yell":
                    ShoutCommand(verb, argument);
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

        private void LookCommand(Player player)
        {
            Console.WriteLine(player.CurrentRoom!.Description);
        }

        private void ShoutCommand(string verb, string argument)
        {            
            if (verb == "shout" && argument == "")
            {
                Console.WriteLine("Shout what?");
            }
            else if (verb == "yell" && argument == "")
            {
                Console.WriteLine("Yell what?");
            }
            else
            {
                string output = argument.ToUpperInvariant();

                if (verb == "yell")
                {
                    Console.WriteLine($"You yell loudly: {output}");
                }
                else
                {
                    Console.WriteLine($"You shout from the bottom of your lungs: {output}");
                }
            }            
        }
    }
}
