using System;

namespace EdiusPlayground
{
    internal class AdventureGame
    {
        public SystemCommand Run()
        {
            SystemCommand returnCommand = RunAdventureMenu();

            switch (returnCommand)
            {
                case SystemCommand.Back:
                    return SystemCommand.None;

                case SystemCommand.Quit:
                    return SystemCommand.Quit;
            }

            return SystemCommand.None;
        }

        private void CreateNewCharacter()
        {
            Console.WriteLine();
            Console.WriteLine("'New' not yet implemented");
        }

        private SystemCommand RunAdventureMenu()
        {
            while (true)
            {
                ShowAdventureMenu();

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
                        CreateNewCharacter();
                        break;

                    case "2":
                        Console.WriteLine();
                        Console.WriteLine("'Load' not yet implemented");
                        break;

                    case "3":
                        Console.WriteLine();
                        Console.WriteLine("'Continue' not yet implemented");
                        break;

                    default:
                        Console.WriteLine("Please pick an option from the menu.");
                        break;
                }
            }
        }

        private void ShowAdventureMenu()
        {
            Console.WriteLine();
            Console.WriteLine("== Adventure ==\n"); // come up with something better here

            Console.WriteLine("1. New character: ");
            Console.WriteLine("2. Load character: ");
            Console.WriteLine("3. Continue: ");

            Console.WriteLine();
            Console.WriteLine("B. Back");
            Console.WriteLine("Q. Quit\n");
        }
    }
}

