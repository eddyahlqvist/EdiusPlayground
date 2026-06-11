
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

                string choice = MenuHelper.GetMenuChoice();

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

        private void ShowGamesMenu()
        {
            Console.WriteLine("Games");

            Console.WriteLine("1. Guess the Number");

            Console.WriteLine("B. Back");
            Console.WriteLine("Q. Quit");
        }
    }
}
