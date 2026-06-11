using System;

namespace EdiusPlayground
{
    internal class MainMenu
    {
        private readonly GameHub _gameHub;
        private string _user = "Unknown"; // move ownership to App later
        public MainMenu(GameHub gameHub)
        {
            _gameHub = gameHub;
        }
        public void Run()
        {
            while (true)
            {
                DisplayUser();
                ShowMainMenu();

                string choice = MenuHelper.GetMenuChoice();

                if (HandleMenuChoice(choice))
                {
                    return;
                }
            }
        }

        private void ShowMainMenu()
        {
            Console.WriteLine("Main Menu\n");

            Console.WriteLine("1. Set active user");       // create, save or load user
            Console.WriteLine("2. Enter playable world");  // game mode
            Console.WriteLine("3. Games");
            Console.WriteLine("4. Tools");
            Console.WriteLine("5. Settings");   // customization for colors, fonts etc

            Console.WriteLine("Q. Quit");
        }        

        private bool HandleMenuChoice(string choice)
        {
            switch (choice)
            {
                case "1":
                    _user = SetUser();
                    return false;

                case "2":
                    Console.WriteLine("Playable world is not implemented yet.");
                    return false;

                case "3":
                    _gameHub.Run();
                    return false;

                case "4":
                    Console.WriteLine("Tools are not implemented yet.");
                    return false;

                case "5":
                    Console.WriteLine("Settings are not implemented yet.");
                    return false;

                case "q":
                    return true;

                default:
                    Console.WriteLine("Please pick an option from the menu.");
                    return false;
            }
        }

        private void DisplayUser()
        {
            Console.WriteLine($"Current active user: {_user}\n");
        }

        private string SetUser()
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