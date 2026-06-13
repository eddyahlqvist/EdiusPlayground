
using System;
using System.IO;
using System.Collections.Generic;

namespace EdiusPlayground
{
    internal class GuessTheNumberGame
    {
        private static readonly Random _rnd = new Random();

        private enum GameMode
        {
            Classic,
            LimitedTries
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
        private readonly int _lowNumber = 1;
        private readonly int _highNumber = 100;
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
            ModeMenuResult result = RunModesMenu();

            if (result.Command == SystemCommand.Back)
            {
                return SystemCommand.None;
            }

            if (result.Command == SystemCommand.Quit)
            {
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
                        _amountGuessed = 0;
                        _secretNumber = GenerateSecretNumber();
                        DebugMessage($"Secret Number is {_secretNumber}.");

                        SystemCommand command = RunGame(chosenMode);

                        if (command != SystemCommand.None)
                        {
                            return command;
                        }

                        return SystemCommand.None;
                    }
                case GameMode.LimitedTries:
                    {
                        LoadHighScore();
                        _score = 100;
                        _amountGuessed = 0;

                        _secretNumber = GenerateSecretNumber();
                        DebugMessage($"Secret Number is {_secretNumber}.");
                        DebugMessage($"Best score is {_bestScore}.");
                        DebugMessage("Reset HighScore with 'c'.");

                        SystemCommand command = RunGame(chosenMode);

                        if (command != SystemCommand.None)
                        {
                            return command;
                        }

                        return SystemCommand.None;
                    }
                default:
                    throw new InvalidOperationException($"Unknown game mode: {chosenMode}");
            }
        }
        private SystemCommand RunGame(GameMode chosenMode)
        {
            List<int> guessedNumbers = [];

            while (true)
            {
                GuessResult result = GetPlayerGuess(chosenMode);                

                if (result.Command == SystemCommand.Back)
                {
                    Console.WriteLine("Returning to menu.");
                    return SystemCommand.Back;
                }

                if (result.Command == SystemCommand.Quit)
                {
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

                if (chosenMode == GameMode.LimitedTries)
                {
                    if (_amountGuessed > 1)
                    {
                        _score -= GuessCost;
                    }

                    if (_amountGuessed == MaxTries && guess != _secretNumber)
                    {
                        Console.WriteLine("Game Over, you have used all your available tries");
                        Console.WriteLine($"Your guesses: {string.Join(", ", guessedNumbers)}");
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
                    Console.WriteLine($"Correct! The secret number was {_secretNumber}. Congratulations!");

                    if (chosenMode == GameMode.LimitedTries)
                    {
                        if (_score > _bestScore)
                        {
                            _bestScore = _score;
                            SaveHighScore();

                            Console.WriteLine($"New high score: {_bestScore} points!");
                        }

                        else
                        {
                            Console.WriteLine($"Best score: {_bestScore}");
                        }
                    }

                    if (_amountGuessed == 1)
                    {
                        Console.WriteLine("You beat the game on your first try! Unbelievable.");
                        return SystemCommand.None;
                    }

                    else
                    {
                        Console.WriteLine($"You beat the game in {_amountGuessed} tries.");
                        Console.WriteLine($"Your guesses: {string.Join(", ", guessedNumbers)}");
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
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"DEBUG Info: {message}");
                Console.ResetColor();
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
                if (command == SystemCommand.Quit)
                {
                    return new GuessResult(null, SystemCommand.Quit);
                }

                if (command == SystemCommand.Back)
                {
                    return new GuessResult(null, SystemCommand.Back);
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

                return new GuessResult (playerGuess, SystemCommand.None);
            }
        }

        private void ShowModesMenu()
        {
            Console.WriteLine("Guess the Number Game Modes: \n");
            Console.WriteLine("1. Classic.");
            Console.WriteLine("2. Guess limit set to 10 tries. (HighScore)");

            Console.WriteLine("B. Back.");
            Console.WriteLine("Q. Quit.");
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

                    default:
                        Console.WriteLine("Please pick an option from the menu.");
                        continue;
                }
            }
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