
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
                SystemCommand command = MenuHelper.GetSystemCommand(choice);

                if (command == SystemCommand.Back)
                {
                    return;
                }

                if (command == SystemCommand.Quit)
                {
                    // later: bubble quit
                    return;
                }

                switch (choice)
                {
                    case "1":
                        _guessTheNumberGame.Run();
                        break;

                    default:
                        Console.WriteLine("Please pick an option from the menu.");
                        break;
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
