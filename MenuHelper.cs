using System;

namespace EdiusPlayground
{
    internal class MenuHelper
    {
        public static string GetMenuChoice()
        {
            Console.Write("Choose an option from the menu: ");

            string? input = Console.ReadLine();

            if (input == null)
            {
                return "";
            }

            Console.WriteLine();
            return input.Trim().ToLowerInvariant();
            //return Console.ReadLine()?.Trim().ToLowerInvariant() ?? ""; // this is shorthand version of the above, use this later
        }
    }
}
