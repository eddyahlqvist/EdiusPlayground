using EdiusPlayground.Core;
using EdiusPlayground.Menus;
using System;

namespace EdiusPlayground.Tools
{
    internal class ToolHub
    {
        private readonly RandomNumberTool _randomNumberTool = new();
        private readonly CharacterExplorer _characterExplorer = new();
        private readonly Archive _archive = new();
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
                        {
                            SystemCommand returnCommand = _randomNumberTool.Run();

                            if (returnCommand == SystemCommand.Back)
                            {
                                continue;
                            }

                            if (returnCommand == SystemCommand.Quit)
                            {
                                return SystemCommand.Quit;
                            }

                            break;
                        }

                    case "2":
                        {
                            SystemCommand returnCommand = _characterExplorer.Run();

                            if (returnCommand == SystemCommand.Back)
                            {
                                continue;
                            }

                            if (returnCommand == SystemCommand.Quit)
                            {
                                return SystemCommand.Quit;
                            }

                            break;
                        }

                    case "3":
                        {
                            SystemCommand returnCommand = _archive.Run();

                            if (returnCommand == SystemCommand.Back)
                            {
                                continue;
                            }

                            if (returnCommand == SystemCommand.Quit)
                            {
                                return SystemCommand.Quit;
                            }

                            break;
                        }

                    default:
                        Console.WriteLine("Please pick an option from the menu.");
                        break;
                }
            }
        }

        private void ShowMenu()
        {
            Console.WriteLine();
            Console.WriteLine("== Tools ==\n");

            Console.WriteLine("1. Random Number Generator");
            Console.WriteLine("2. Character Explorer");
            Console.WriteLine("3. Archive");

            Console.WriteLine("B. Back");
            Console.WriteLine("Q. Quit\n");
        }
    }
}
