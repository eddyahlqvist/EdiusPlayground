using System;

namespace EdiusPlayground
{
    internal class MenuHelper
    {
        public static string GetMenuChoice()
        {
            Console.Write("Choose an option from the menu: ");
            return Console.ReadLine()?.Trim().ToLowerInvariant() ?? "";
        }

        public static SystemCommand GetSystemCommand(string choice)
        {
            choice = choice.Trim().ToLowerInvariant();

            switch (choice)
            {
                case "q":
                case "quit":
                case "exit":
                    return SystemCommand.Quit;

                case "b":
                case "back":
                    return SystemCommand.Back;

                default:
                    return SystemCommand.None;
            }
        }
    }
}
