
using System;

namespace EdiusPlayground
{
    internal class ToolHub
    {
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

                if (choice == "b")
                {
                    return; // back to main menu
                }

                if (choice == "q")
                {
                    // later: signal quit to the whole app
                    return;
                }

                else
                {
                    Console.WriteLine("Please pick an option from the menu.");
                }
            }
        }

        private void ShowToolsMenu()
        {
            Console.WriteLine("Tools Menu to be constructed");
        }
    }
}
