
using System;

namespace EdiusPlayground
{
    internal class GameHub
    {
        private readonly GuessTheNumberGame _guessTheNumberGame = new();
        
        public void Run()
        {
            RunGamesMenu();
        }

        private void RunGamesMenu()
        {
            while (true)
            {
                ShowGamesMenu();

                string choice = GetMenuChoice();

                if (choice == "b")
                {
                    return; // back to main menu
                }

                if (choice == "q")
                {
                    // later: signal quit to the whole app
                    return;
                }

                if (choice == "1")
                {
                    _guessTheNumberGame.Run();
                }
                else
                {
                    Console.WriteLine("Please pick an option from the menu.");
                }
            }
        }

        private string GetMenuChoice()
        {
            Console.Write("Choose an option from the menu: ");

            string? input = Console.ReadLine();

            if (input == null)
            {
                return "";
            }

            Console.WriteLine();
            return input.Trim().ToLowerInvariant();
        }

        private void ShowGamesMenu()
        {
            Console.WriteLine("Games");

            Console.WriteLine("1. Guess the Number");

            Console.WriteLine("B. Back");
            Console.WriteLine("Q. Quit");
        }
    }
}
