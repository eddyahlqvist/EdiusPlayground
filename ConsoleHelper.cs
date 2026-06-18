
using System;

namespace EdiusPlayground
{
    internal static class ConsoleHelper
    {
        public static void WriteColored(string text, ConsoleColor color)
        {
            ConsoleColor oldColor = Console.ForegroundColor;

            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ForegroundColor = oldColor;
        }

        public static void WriteLineColored(string text, ConsoleColor color)
        {
            WriteColored(text, color);
            Console.WriteLine();
        }
    }
}
