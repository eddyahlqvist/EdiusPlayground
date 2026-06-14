
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

            if (returnCommand == SystemCommand.Back)
            {
                return SystemCommand.None;
            }

            return SystemCommand.Back;
        }

        private SystemCommand RunToolsMenu()
        {
            while (true)
            {
                ShowToolsMenu();

                string choice = MenuHelper.GetMenuChoice();
                SystemCommand command = MenuHelper.GetSystemCommand(choice);

                switch (command)
                {
                    case SystemCommand.Quit:
                        return SystemCommand.Quit;

                    case SystemCommand.Back:
                        return SystemCommand.Back;
                }                

                switch (choice)
                {
                    case "1":
                        //return _randomNumberTool.Run();
                        SystemCommand returnCommand = _randomNumberTool.Run();
                        if (returnCommand == SystemCommand.Quit)
                        {
                            return SystemCommand.Quit;
                        }

                        if (returnCommand == SystemCommand.Back)
                        {
                            continue;
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
