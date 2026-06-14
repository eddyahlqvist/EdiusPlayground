
using System;

namespace EdiusPlayground
{
    internal class ToolHub
    {
        private readonly RandomNumberTool _randomNumberTool = new();
        public SystemCommand Run()
        {
            SystemCommand returnCommand = RunToolsMenu();
            if (returnCommand == SystemCommand.Quit)
            {
                return SystemCommand.Quit;
            }
            return SystemCommand.None;
        }

        private SystemCommand RunToolsMenu()
        {
            while (true)
            {
                ShowToolsMenu();

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
                        SystemCommand returnCommand = _randomNumberTool.Run();
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

        private void ShowToolsMenu()
        {
            Console.WriteLine("Tools\n");

            Console.WriteLine("1. Random Number Generator");

            Console.WriteLine("B. Back");
            Console.WriteLine("Q. Quit\n");
        }
    }
}
