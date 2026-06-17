using System;

namespace EdiusPlayground
{
    internal class MainMenu
    {
        private readonly GameHub _gameHub;
        private readonly ToolHub _toolHub;

        private readonly Func<string> _getUser;
        private readonly Action<string> _setUser;

        public MainMenu(GameHub gameHub, ToolHub toolHub, Func<string> getUser, Action<string> setUser)
        {
            _gameHub = gameHub;
            _toolHub = toolHub;
            _getUser = getUser;
            _setUser = setUser;
        }
        public SystemCommand Run()
        {
            while (true)
            {
                DisplayUser();
                ShowMainMenu();

                string choice = MenuHelper.GetMenuChoice();
                SystemCommand command = MenuHelper.GetSystemCommand(choice);

                switch (command)
                {
                    case SystemCommand.Back:
                        Console.WriteLine("You can't go back from the main menu. Use 'q' to exit.");
                        continue;

                    case SystemCommand.Quit:
                        Console.WriteLine($"{_getUser()} is signing out. Farewell!");
                        return SystemCommand.Quit;
                }                

                switch (choice)
                {
                    case "1":
                        _setUser(PromptForUserName());
                        break;

                    case "2":
                        Console.WriteLine("Playable world is not implemented yet.");
                        break;

                    case "3":
                        {
                            SystemCommand returnCommand = _gameHub.Run();

                            switch (returnCommand)
                            {
                                case SystemCommand.Back:
                                    continue;

                                case SystemCommand.Quit:
                                    return SystemCommand.Quit;
                            }
                            break;
                        }

                    case "4":
                        {
                            SystemCommand returnCommand = _toolHub.Run();

                            switch (returnCommand)
                            {
                                case SystemCommand.Back:
                                    continue;

                                case SystemCommand.Quit:
                                    return SystemCommand.Quit;
                            }
                            break;
                        }

                    case "5":
                        Console.WriteLine("Settings are not implemented yet.");
                        break;
                   
                    default:
                        Console.WriteLine("Please pick an option from the menu.");
                        break;
                }                
            }
        }

        private void ShowMainMenu()
        {
            Console.WriteLine("== Main Menu ==\n");

            Console.WriteLine("1. Set active user");       // create, save or load user
            Console.WriteLine("2. Enter playable world");  // game mode
            Console.WriteLine("3. Games");
            Console.WriteLine("4. Tools");
            Console.WriteLine("5. Settings");   // customization for colors, fonts etc

            Console.WriteLine("Q. Quit\n");
        }        

        private void DisplayUser()
        {
            Console.WriteLine($"Current active user: {_getUser()}\n");
        }

        private string PromptForUserName()
        {
            Console.WriteLine("Please enter your name: ");

            string? input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                return "Unknown";
            }

            return input.Trim();
        }
    }
}