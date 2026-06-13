
using System;

namespace EdiusPlayground
{
    internal class ToolHub
    {
        private readonly RandomNumberTool _randomNumberGenerator = new();
        public void Run()
        {
            RunToolsMenu();
        }

        private void RunToolsMenu()
        {
            while (true)
            {
                ShowToolsMenu();

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
                        _randomNumberGenerator.Run();
                        break;

                    default:
                        Console.WriteLine("Please pick an option from the menu.");
                        break;
                }
            }
        }

        private void ShowToolsMenu()
        {
            Console.WriteLine("Tools");

            Console.WriteLine("1. Random Number Generator");

            Console.WriteLine("B. Back");
            Console.WriteLine("Q. Quit");
        }
    }
}
