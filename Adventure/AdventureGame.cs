using EdiusPlayground.Core;
using EdiusPlayground.Helpers;
using EdiusPlayground.Menus;
using System;
using System.IO;

namespace EdiusPlayground.Adventure
{
    internal class AdventureGame
    {
        private const string PlayerFile = "player.txt";

        private World? _world;
        private Player? _player;
        private readonly CommandHandler _commandHandler = new();

        public SystemCommand Run()
        {
            LoadPlayerFile();
            InitializeWorld();

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

                SavePlayerFile(name);

                InitializePlayer(name);

                ConsoleHelper.WriteLineColored(
                    $"Character created: {_player?.Name}",
                    ConsoleColor.Cyan);

                break;
            }
        }

        private void DisplayCurrentCharacter()
        {
            string name = _player?.Name ?? "None";
            Console.WriteLine();
            ConsoleHelper.WriteLineColored($"Current character: {name}", ConsoleColor.Cyan);
        }

        private void DisplayExits()
        {
            Console.WriteLine(_player!.CurrentRoom!.GetExitShort());
        }

        private void RunGameLoop()
        {
            while (true)
            {
                Room? currentRoom = _player?.CurrentRoom;

                if (currentRoom == null)
                {
                    DebugHelper.Write("ERROR: Player CurrentRoom was null while inside the world. Resetting to starting room.");

                    ConsoleHelper.WriteLineColored(
                        "Something went wrong. Reality flickers, and you are returned to The Threshold.",
                        ConsoleColor.Red);

                    _player!.CurrentRoom = _world?.StartingRoom;

                    currentRoom = _player.CurrentRoom;
                }

                Console.WriteLine();
                DisplayExits();

                DebugHelper.Write($"X:{currentRoom!.X}, Y:{currentRoom!.Y}, Z:{currentRoom!.Z}");

                Console.WriteLine(currentRoom?.Name);
                Console.WriteLine(currentRoom?.Description);

                Console.Write("> ");

                string input = Console.ReadLine()?.Trim().ToLower() ?? "";
                string verb;
                string argument;
                int firstSpace = input.IndexOf(' ');

                if (firstSpace == -1)
                {
                    verb = input;
                    argument = "";
                }
                else
                {
                    verb = input[..firstSpace];
                    argument = input[(firstSpace + 1)..].Trim();
                }

                CommandResult result;

                if (_commandHandler.TryGetDirection(verb, out Direction direction))
                {
                    result = _commandHandler.HandleDirection(direction, _player!);
                }
                else
                {
                    result = _commandHandler.HandleCommand(verb, argument, _player!);
                }

                if (result == CommandResult.Exit)
                {
                    break;
                }
            }
        }

        private void EnterWorld()
        {
            if (_player == null)
            {
                Console.WriteLine("No character loaded.");
                return;
            }

            if (_player.CurrentRoom == null)
            {
                _player.CurrentRoom = _world?.StartingRoom;
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
            Console.WriteLine("== Adventure MUD sim ==\n");

            Console.WriteLine("1. New character: ");
            Console.WriteLine("2. Load character: ");
            Console.WriteLine("3. Enter World: ");

            Console.WriteLine();
            Console.WriteLine("B. Back");
            Console.WriteLine("Q. Quit\n");
        }

        private void InitializePlayer(string name)
        {
            PlayerBuilder builder = new();
            _player = builder.BuildPlayer(name);
        }

        private void InitializeWorld()
        {
            WorldBuilder builder = new();
            _world = builder.BuildWorld();
        }

        private void LoadPlayerFile()
        {
            if (!File.Exists(PlayerFile))
            {
                return;
            }

            string name = File.ReadAllText(PlayerFile).Trim();
            InitializePlayer(name);

        }

        private void SavePlayerFile(string name)
        {
            File.WriteAllText(PlayerFile, name);
        }
    }
}

