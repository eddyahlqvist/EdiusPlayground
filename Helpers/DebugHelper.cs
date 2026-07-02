
using EdiusPlayground.Core;
using System;

namespace EdiusPlayground.Helpers
{
    internal static class DebugHelper
    {
        public static void Write(string message)
        {
            if (App.IsDebugMode)
            {
                ConsoleColor oldColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"DEBUG Info: {message}");
                Console.ForegroundColor = oldColor;
            }
        }
    }
}
