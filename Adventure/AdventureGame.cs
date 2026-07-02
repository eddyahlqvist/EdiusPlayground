using EdiusPlayground.Core;
using EdiusPlayground.Helpers;
using EdiusPlayground.Menus;
using System;

namespace EdiusPlayground.Adventure
{
    internal class AdventureGame
    {
        private Player? _currentPlayer;
        private Room? _startingRoom;

        public SystemCommand Run()
        {
            BuildWorld();

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

        private void RunGameLoop()
        {
            while (true)
            {
                Room? currentRoom = _currentPlayer?.CurrentRoom;

                if (currentRoom == null)
                {                    
                    DebugHelper.Write("ERROR: Player CurrentRoom was null while inside the world. Resetting to starting room.");

                    ConsoleHelper.WriteLineColored(
                        "Something went wrong. Reality flickers, and you are returned to The Threshold.",
                        ConsoleColor.Red);

                    _currentPlayer!.CurrentRoom = _startingRoom;

                    currentRoom = _currentPlayer.CurrentRoom;
                }

                Console.WriteLine();
                Console.WriteLine(currentRoom?.Name);
                Console.WriteLine(currentRoom?.Description);

                Console.Write("> ");

                string command = Console.ReadLine()?.Trim().ToLower() ?? "";

                if (command == "exit")
                {
                    break;
                }

                if (command == "north" && _currentPlayer?.CurrentRoom?.North != null)
                {
                    _currentPlayer.CurrentRoom = _currentPlayer.CurrentRoom.North;
                    continue;
                }

                if (command == "south" && _currentPlayer?.CurrentRoom?.South != null)
                {
                    _currentPlayer.CurrentRoom = _currentPlayer.CurrentRoom.South;
                    continue;
                }

                Console.WriteLine("Unknown command.");
            }
        }

        private void EnterWorld()
        {
            if (_currentPlayer == null)
            {
                Console.WriteLine("No character loaded.");
                return;
            }

            if (_currentPlayer.CurrentRoom == null)
            {
                _currentPlayer.CurrentRoom = _startingRoom;
            }

            RunGameLoop();
        }

        private SystemCommand RunAdventureMenu()
        {
            while (true)
            {
                DisplayCurrentCharacter();
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
            Console.WriteLine("== Adventure MUD sim ==\n"); // come up with something better here

            Console.WriteLine("1. New character: ");
            Console.WriteLine("2. Load character: ");
            Console.WriteLine("3. Enter World: ");

            Console.WriteLine();
            Console.WriteLine("B. Back");
            Console.WriteLine("Q. Quit\n");
        }

        private void BuildWorld()
        {
            WorldBuilder builder = new();
            _startingRoom = builder.BuildWorld();
        }
    }
}

