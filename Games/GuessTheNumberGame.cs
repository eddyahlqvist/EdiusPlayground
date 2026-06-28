
using System;
using System.IO;
using System.Collections.Generic;
using EdiusPlayground.Helpers;
using EdiusPlayground.Menus;
using EdiusPlayground.Core;

namespace EdiusPlayground.Games
{
    internal class GuessTheNumberGame
    {
        private static readonly Random _rnd = new();

        private enum GameMode
        {
            Classic,
            LimitedTries,
            Hardcore
        }

        private readonly struct ModeMenuResult
        {
            public GameMode? Mode { get; }
            public SystemCommand Command { get; }

            public ModeMenuResult(GameMode? mode, SystemCommand command)
            {
                Mode = mode;
                Command = command;
            }
        }

        private readonly struct GuessResult
        {
            public int? Guess { get; }
            public SystemCommand Command { get; }

            public GuessResult(int? guess, SystemCommand command)
            {
                Guess = guess;
                Command = command;
            }
        }

        // Game settings
        private const byte _lowNumber = 1;
        private int _highNumber;
        private const byte GuessCost = 10;
        private const byte MaxTries = 10;

        // Runtime state
        private int _secretNumber;
        private int _amountGuessed;
        private int _score;
        private int _bestScore = 0;

        // Debug
        private const bool IsDebugMode = true;

        // Persistence
        private const string HighScoreFile = "GTN_highscore.txt";

        public SystemCommand Run()
        {
            while (true)
            {
                ModeMenuResult result = RunModesMenu();

                switch (result.Command)
                {
                    case SystemCommand.Back:
                        return SystemCommand.Back;

                    case SystemCommand.Quit:
                        return SystemCommand.Quit;
                }

                if (result.Mode == null)
                {
                    return SystemCommand.None;
                }

                GameMode chosenMode = result.Mode.Value;

                switch (chosenMode)
                {
                    case GameMode.Classic:
                        {
                            _highNumber = 100;
                            PrepareNewRound();

                            SystemCommand classicCommand = RunGame(chosenMode);

                            switch (classicCommand)
                            {
                                case SystemCommand.Back:
                                case SystemCommand.None:
                                    continue;

                                case SystemCommand.Quit:
                                    return SystemCommand.Quit;
                            }
                        }

                        break;

                    case GameMode.LimitedTries:
                        {
                            _highNumber = 100;
                            LoadHighScore();
                            _score = 100;

                            DebugMessage($"Best score is {_bestScore}.");
                            DebugMessage("Reset HighScore with 'c'.");

                            PrepareNewRound();

                            SystemCommand limitedCommand = RunGame(chosenMode);

                            switch(limitedCommand)
                            {
                                case SystemCommand.Back:
                                case SystemCommand.None:
                                    continue;

                                case SystemCommand.Quit:
                                    return SystemCommand.Quit;
                            }

                            break;
                        }

                    case GameMode.Hardcore:
                        {
                            _highNumber = 1000;
                            PrepareNewRound();                            

                            SystemCommand hardcoreCommand = RunGame(chosenMode);

                            switch (hardcoreCommand)
                            {
                                case SystemCommand.Back:
                                case SystemCommand.None:
                                    continue;

                                case SystemCommand.Quit:
                                    return SystemCommand.Quit;
                            }
                        }

                        break;

                    default:
                        throw new InvalidOperationException($"Unknown game mode: {chosenMode}");
                }
            }
        }


        private SystemCommand RunGame(GameMode chosenMode)
        {
            List<int> guessedNumbers = [];

            while (true)
            {
                GuessResult result = GetPlayerGuess(chosenMode);

                switch (result.Command)
                {
                    case SystemCommand.Back:
                        Console.WriteLine("Returning to menu.");
                        return SystemCommand.Back;

                    case SystemCommand.Quit:
                        Console.WriteLine("Thanks for playing!");
                        return SystemCommand.Quit;
                }

                if (result.Guess == null)
                {
                    return SystemCommand.None;
                }

                int guess = result.Guess.Value;

                guessedNumbers.Add(guess);
                _amountGuessed = guessedNumbers.Count;

                if (chosenMode == GameMode.LimitedTries || chosenMode == GameMode.Hardcore)
                {
                    if (_amountGuessed > 1)
                    {
                        _score -= GuessCost;
                    }

                    if (_amountGuessed == MaxTries && guess != _secretNumber)
                    {
                        ConsoleHelper.WriteLineColored("Game Over, you have used all your available tries", ConsoleColor.Cyan);
                        ConsoleHelper.WriteLineColored($"Your guesses: {string.Join(", ", guessedNumbers)}", ConsoleColor.Cyan);
                        return SystemCommand.None;
                    }
                }

                if (guess < _secretNumber)
                {
                    Console.WriteLine("Too low, try again.");
                }
                else if (guess > _secretNumber)
                {
                    Console.WriteLine("Too high, try again.");
                }
                else
                {
                    ConsoleHelper.WriteLineColored($"Correct! The secret number was {_secretNumber}. Congratulations!", ConsoleColor.Cyan);

                    if (chosenMode == GameMode.LimitedTries)
                    {
                        if (_score > _bestScore)
                        {
                            _bestScore = _score;
                            SaveHighScore();

                            ConsoleHelper.WriteLineColored($"New high score: {_bestScore} points!", ConsoleColor.Cyan);
                        }

                        else
                        {
                            ConsoleHelper.WriteLineColored($"Best score: {_bestScore}", ConsoleColor.Cyan);
                        }
                    }

                    if (_amountGuessed == 1)
                    {
                        ConsoleHelper.WriteLineColored("You beat the game on your first try! Unbelievable.", ConsoleColor.Cyan);
                        return SystemCommand.None;
                    }

                    else
                    {
                        ConsoleHelper.WriteLineColored($"You beat the game in {_amountGuessed} tries.", ConsoleColor.Cyan);
                        ConsoleHelper.WriteLineColored($"Your guesses: {string.Join(", ", guessedNumbers)}", ConsoleColor.Cyan);
                        return SystemCommand.None;
                    }
                }
            }
        }

        private int GenerateSecretNumber()
        {
            return _rnd.Next(_lowNumber, _highNumber + 1);
        }

        private void DebugMessage(string message)
        {
            if (IsDebugMode)
            {
                ConsoleColor oldColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"DEBUG Info: {message}");
                Console.ForegroundColor = oldColor;
            }
        }

        private GuessResult GetPlayerGuess(GameMode chosenMode)
        {
            while (true)
            {
                Console.Write($"Enter a number between {_lowNumber} and {_highNumber}. 'B' to go back or 'Q' to quit. ");

                string? input = Console.ReadLine();

                if (input == null)
                {
                    return new GuessResult(null, SystemCommand.Back);
                }

                if (IsDebugMode && chosenMode == GameMode.LimitedTries)
                {
                    if (input.Trim().Equals("c", StringComparison.OrdinalIgnoreCase))
                    {
                        _bestScore = 0;
                        SaveHighScore();
                        DebugMessage($"The HighScore have been reset. New score is {_bestScore}.");
                        continue;
                    }
                }

                SystemCommand command = MenuHelper.GetSystemCommand(input);

                switch (command)
                {
                    case SystemCommand.Back:
                        return new GuessResult(null, SystemCommand.Back);

                    case SystemCommand.Quit:
                        return new GuessResult(null, SystemCommand.Quit);
                }

                if (!int.TryParse(input, out int playerGuess))
                {
                    Console.WriteLine("Please enter a whole number. 'B' to go back or 'Q' to quit.");
                    continue;
                }

                if (playerGuess < _lowNumber || playerGuess > _highNumber)
                {
                    Console.WriteLine($"Please enter a number between {_lowNumber} and {_highNumber}. 'B' to go back or 'Q' to quit.");
                    continue;
                }

                return new GuessResult(playerGuess, SystemCommand.None);
            }
        }

        private void ShowModesMenu()
        {
            Console.WriteLine();
            Console.WriteLine("== Guess the Number Game Modes ==\n");
            Console.WriteLine("1. Classic.");
            Console.WriteLine("2. Guess limit set to 10 tries. (HighScore)");
            Console.WriteLine("3. Hardcore with 10 tries");

            Console.WriteLine("B. Back.");
            Console.WriteLine("Q. Quit.\n");
        }

        private ModeMenuResult RunModesMenu()
        {
            while (true)
            {
                ShowModesMenu();

                string choice = MenuHelper.GetMenuChoice();
                SystemCommand command = MenuHelper.GetSystemCommand(choice);

                if (command != SystemCommand.None)
                {
                    return new ModeMenuResult(null, command);
                }

                switch (choice)
                {
                    case "1":
                        return new ModeMenuResult(GameMode.Classic, SystemCommand.None);

                    case "2":
                        return new ModeMenuResult(GameMode.LimitedTries, SystemCommand.None);

                    case "3":
                        return new ModeMenuResult(GameMode.Hardcore, SystemCommand.None);

                    default:
                        Console.WriteLine("Please pick an option from the menu.");
                        continue;
                }
            }
        }

        private void PrepareNewRound()
        {
            _amountGuessed = 0;
            _secretNumber = GenerateSecretNumber();
            DebugMessage($"Secret Number is {_secretNumber}.");
        }

        private void LoadHighScore()
        {
            if (!File.Exists(HighScoreFile))
            {
                return;
            }

            string text = File.ReadAllText(HighScoreFile);

            if (int.TryParse(text, out int loadedScore))
            {
                _bestScore = loadedScore;
            }
        }

        private void SaveHighScore()
        {
            File.WriteAllText(HighScoreFile, _bestScore.ToString());
        }
    }
}