using System;

namespace EdiusPlayground
{
    internal class CharacterExplorer
    {
        private enum CharacterExplorerMode
        {
            CharacterToDecimal,
            DecimalToCharacter
        }

        private readonly struct MenuResult
        {
            public CharacterExplorerMode? Mode { get; }
            public SystemCommand Command { get; }

            public MenuResult(CharacterExplorerMode? mode, SystemCommand command)
            {
                Mode = mode;
                Command = command;
            }
        }

        public SystemCommand Run()
        {
            while (true)
            {
                MenuResult result = RunMenu();

                switch (result.Command)
                {
                    case SystemCommand.Back:
                        return SystemCommand.Back;

                    case SystemCommand.Quit:
                        return SystemCommand.Quit;
                }

                if (result.Mode == null)
                {
                    return SystemCommand.None;
                }

                switch (result.Mode)
                {
                    case CharacterExplorerMode.CharacterToDecimal:
                        Console.WriteLine("TEST1");
                        break;

                    case CharacterExplorerMode.DecimalToCharacter:
                        Console.WriteLine("TEST2");
                        break;
                }
            }
        }

        private MenuResult RunMenu()
        {
            while (true)
            {
                ShowMenu();

                string choice = MenuHelper.GetMenuChoice();
                SystemCommand command = MenuHelper.GetSystemCommand(choice);

                if (command != SystemCommand.None)
                {
                    return new MenuResult(null, command);
                }

                switch (choice)
                {
                    case "1":
                        return new MenuResult(CharacterExplorerMode.CharacterToDecimal, SystemCommand.None);

                    case "2":
                        return new MenuResult(CharacterExplorerMode.DecimalToCharacter, SystemCommand.None);                    

                    default:
                        Console.WriteLine();
                        Console.WriteLine("Please pick an option from the menu.");
                        continue;
                }
            }
        }
        private void ShowMenu()
        {
            Console.WriteLine();
            Console.WriteLine("== Character Explorer ==\n");

            Console.WriteLine("1. ASCII Character to ASCII Decimal.");
            Console.WriteLine("2. ASCII Decimal to ASCII Character.");

            Console.WriteLine("B. Back.");
            Console.WriteLine("Q. Quit.\n");
        }
    }
}
