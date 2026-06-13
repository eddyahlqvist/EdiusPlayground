
using System;

namespace EdiusPlayground
{
    internal class GameHub
    {
        private readonly GuessTheNumberGame _guessTheNumberGame = new();

        public SystemCommand Run()
        {
            SystemCommand returnCommand = RunGamesMenu();
            if (returnCommand == SystemCommand.Quit)
            {
                return SystemCommand.Quit;
            }
            return SystemCommand.None;
        }

        private SystemCommand RunGamesMenu()
        {
            while (true)
            {
                ShowGamesMenu();

                string choice = MenuHelper.GetMenuChoice();
                SystemCommand command = MenuHelper.GetSystemCommand(choice);

                if (command == SystemCommand.Back)
                {
                    return SystemCommand.None;
                }

                if (command == SystemCommand.Quit)
                {
                    return SystemCommand.Quit;
                }

                switch (choice)
                {
                    case "1":
                        SystemCommand returnCommand = _guessTheNumberGame.Run();
                        if (returnCommand == SystemCommand.Quit)
                        {
                            return SystemCommand.Quit;
                        }
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
