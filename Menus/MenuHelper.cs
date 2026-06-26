using System;

namespace EdiusPlayground
{
    internal static class MenuHelper
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
                case "b":
                case "back":
                    return SystemCommand.Back;

                case "q":
                case "quit":
                case "exit":
                    return SystemCommand.Quit;
                
                default:
                    return SystemCommand.None;
            }
        }
    }
}
