using System;

namespace EdiusPlayground
{
    internal class AdventureGame
    {
        private Player? _currentPlayer;
        private Room _currentRoom = new Room(
            "The Threshold",
            "You stand in a quiet stone chamber. The air is still.");

        public SystemCommand Run()
        {
            SystemCommand returnCommand = RunAdventureMenu();

            switch (returnCommand)
            {
                case SystemCommand.Back:
                    return SystemCommand.None;

                case SystemCommand.Quit:
                    return SystemCommand.Quit;
            }

            return SystemCommand.None;
        }

        private void CreateNewCharacter()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("== Character Creation ==\n");
                Console.WriteLine("Enter character name:");

                string name = Console.ReadLine()?.Trim() ?? "";

                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("Please enter a character name.");
                    continue;
                }

                _currentPlayer = new Player(name);

                ConsoleHelper.WriteLineColored(
                    $"Character created: {_currentPlayer.Name}",
                    ConsoleColor.Cyan);

                break;
            }
        }

        private void DisplayCurrentCharacter()
        {
            string name = _currentPlayer?.Name ?? "None";
            Console.WriteLine($"Current character: {name}");
        }

        private void EnterWorld()
        {
            if (_currentPlayer == null)
            {
                Console.WriteLine("No character loaded. Create or load a character first.");
                return;
            }

            Console.WriteLine();
            Console.WriteLine(_currentRoom.Name);
            Console.WriteLine(_currentRoom.Description);
        }

        private SystemCommand RunAdventureMenu()
        {
            while (true)
            {
                ShowAdventureMenu();

                string choice = MenuHelper.GetMenuChoice();
                SystemCommand command = MenuHelper.GetSystemCommand(choice);

                switch (command)
                {
                    case SystemCommand.Back:
                        return SystemCommand.Back;

                    case SystemCommand.Quit:
                        return SystemCommand.Quit;
                }

                switch (choice)
                {
                    case "1":
                        CreateNewCharacter();
                        break;

                    case "2":
                        Console.WriteLine();
                        Console.WriteLine("'Load' not yet implemented");
                        break;

                    case "3":
                        EnterWorld();
                        DisplayCurrentCharacter();
                        break;

                    default:
                        Console.WriteLine("Please pick an option from the menu.");
                        break;
                }
            }
        }

        private void ShowAdventureMenu()
        {
            Console.WriteLine();
            Console.WriteLine("== Adventure ==\n"); // come up with something better here

            Console.WriteLine("1. New character: ");
            Console.WriteLine("2. Load character: ");
            Console.WriteLine("3. Enter World: ");

            Console.WriteLine();
            Console.WriteLine("B. Back");
            Console.WriteLine("Q. Quit\n");
        }
    }
}

