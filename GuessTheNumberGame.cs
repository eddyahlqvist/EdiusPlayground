
using System;
using System.IO;
using System.Collections.Generic;

namespace EdiusPlayground
{
    internal class GuessTheNumberGame
    {
        private static readonly Random _rnd = new Random();

        private const bool IsDebugMode = true;

        private readonly int _lowNumber = 1;
        private readonly int _highNumber = 100;
        private readonly int _secretNumber;

        private int _amountGuessed;
        private int _score = 100;
        private int _bestScore = 0;

        private const string HighScoreFile = "GTN_highscore.txt";
        private const int GuessCost = 10;
        private const byte MaxTries = 10;

        public GuessTheNumberGame()
        {
            _secretNumber = GenerateSecretNumber();
        }

        public void Run()
        {
            LoadHighScore();

            DebugMessage($"Secret Number is {_secretNumber}.");
            DebugMessage($"Best score is {_bestScore}.");
            DebugMessage("Reset HighScore with 'c'.");

            RunGame();
        }
        private void RunGame()
        {
            List<int> guessedNumbers = [];

            while (true)
            {
                int? playerGuess = GetPlayerGuess();

                if (playerGuess == null)
                {
                    Console.WriteLine("Thanks for playing!");
                    return;
                }

                int guess = playerGuess.Value;
                guessedNumbers.Add(guess);
                _amountGuessed = guessedNumbers.Count;

                if (_amountGuessed > 1)
                {
                    _score -= GuessCost;
                }

                if (_amountGuessed == MaxTries && guess != _secretNumber)
                {
                    Console.WriteLine("Game Over, you have used all your available tries");
                    Console.WriteLine($"Your guesses: {string.Join(", ", guessedNumbers)}");
                    return;
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

                    if (_amountGuessed == 1)
                    {
                        Console.WriteLine("You beat the game on your first try! Unbelievable.");
                        return;
                    }

                    else
                    {
                        Console.WriteLine($"You beat the game in {_amountGuessed} tries.");
                        Console.WriteLine($"Your guesses: {string.Join(", ", guessedNumbers)}");
                        return;
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
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"DEBUG Info: {message}");
                Console.ResetColor();
            }
        }

        private int? GetPlayerGuess()
        {
            while (true)
            {
                Console.Write($"Enter a number between {_lowNumber} and {_highNumber} or 'q' to quit. ");

                string? input = Console.ReadLine();

                if (input == null)
                {
                    return null;
                }

                if (IsDebugMode)
                {
                    if (input.Trim().Equals("c", StringComparison.OrdinalIgnoreCase))
                    {
                        _bestScore = 0;
                        SaveHighScore();
                        DebugMessage($"The HighScore have been reset. New score is {_bestScore}.");
                        continue;
                    }
                }

                if (input.Trim().Equals("q", StringComparison.OrdinalIgnoreCase))
                {
                    return null;
                }

                if (!int.TryParse(input, out int playerGuess))
                {
                    Console.WriteLine("Please enter a whole number or 'q' to quit.");
                    continue;
                }

                if (playerGuess < _lowNumber || playerGuess > _highNumber)
                {
                    Console.WriteLine($"Please enter a number between {_lowNumber} and {_highNumber}.");
                    continue;
                }

                return playerGuess;
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

