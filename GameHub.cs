
using System;

namespace EdiusPlayground
{
    internal class GameHub
    {
        private readonly GuessTheNumberGame _guessTheNumberGame = new();

        public SystemCommand Run()
        {
            SystemCommand returnCommand = RunMenu();

            switch (returnCommand)
            {
                case SystemCommand.Back:
                    return SystemCommand.None;

                case SystemCommand.Quit:
                    return SystemCommand.Quit;
            }

            return SystemCommand.None;
        }

        private SystemCommand RunMenu()
        {
            while (true)
            {
                ShowMenu();

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
                        SystemCommand returnCommand = _guessTheNumberGame.Run();

                        if (returnCommand == SystemCommand.Back)
                        {
                            continue;
                        }

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

        private void ShowMenu()
        {
            Console.WriteLine();
            Console.WriteLine("== Games ==\n");

            Console.WriteLine("1. Guess the Number");

            Console.WriteLine("B. Back");
            Console.WriteLine("Q. Quit\n");
        }
    }
}
