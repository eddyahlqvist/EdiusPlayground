using System;

namespace EdiusPlayground
{
    internal class RandomNumberTool
    {
        private static readonly Random _rnd = new Random();

        private const int LowestAllowedNumber = -1000000000;
        private const int HighestAllowedNumber = 1000000000;

        private int _lowNumber;
        private int _highNumber;
        private int _randomNumber;

        private enum GeneratorMode
        {
            LowRange,
            HighRange,
            Custom
        }

        private readonly struct GeneratorMenuResult
        {
            public GeneratorMode? Mode { get; }
            public SystemCommand Command { get; }

            public GeneratorMenuResult(GeneratorMode? mode, SystemCommand command)
            {
                Mode = mode;
                Command = command;
            }
        }

        public SystemCommand Run()
        {
            GeneratorMenuResult result = RunRandomNumberMenu();

            if (result.Command == SystemCommand.Back)
            {
                return SystemCommand.Back;
            }

            if (result.Command == SystemCommand.Quit)
            {
                return SystemCommand.Quit;
            }

            if (result.Mode == null)
            {
                return SystemCommand.None;
            }

            GeneratorMode chosenMode = result.Mode.Value;

            RunGenerator(chosenMode);
            Console.WriteLine($"Your random number is: {_randomNumber}\n");

            return SystemCommand.Back;
        }

        private void RunGenerator(GeneratorMode chosenMode)
        {
            switch (chosenMode)
            {
                case GeneratorMode.LowRange:
                    _lowNumber = 1;
                    _highNumber = 10;
                    _randomNumber = GenerateRandomNumber();
                    return;

                case GeneratorMode.HighRange:
                    _lowNumber = 1;
                    _highNumber = 100;
                    _randomNumber = GenerateRandomNumber();
                    return;

                case GeneratorMode.Custom:
                    Console.WriteLine("Enter the lower number: ");
                    _lowNumber = GetUserNumber();

                    while (true)
                    {
                        Console.WriteLine("Enter the higher number: ");
                        _highNumber = GetUserNumber();

                        if (_highNumber <= _lowNumber)
                        {
                            Console.WriteLine($"Please enter a number that's higher than {_lowNumber}.");
                            continue;
                        }

                        break;
                    }

                    _randomNumber = GenerateRandomNumber();
                    return;
            }
        }

        private int GetUserNumber()
        {
            while (true)
            {
                string input = Console.ReadLine() ?? "";

                if (!int.TryParse(input, out int number))
                {
                    Console.WriteLine("Please enter a number.");
                    continue;
                }

                if (!ReviewNumber(number))
                {
                    continue;
                }

                return number;
            }
        }

        private bool ReviewNumber(int userNumber)
        {
            if (userNumber < LowestAllowedNumber || userNumber > HighestAllowedNumber)
            {
                Console.WriteLine($"Pick a number between {LowestAllowedNumber} and {HighestAllowedNumber}.");
                return false;
            }

            return true;
        }

        private int GenerateRandomNumber()
        {
            return _rnd.Next(_lowNumber, _highNumber + 1);
        }

        private GeneratorMenuResult RunRandomNumberMenu()
        {
            while (true)
            {
                ShowRandomNumberMenu();

                string choice = MenuHelper.GetMenuChoice();
                SystemCommand command = MenuHelper.GetSystemCommand(choice);

                if (command != SystemCommand.None)
                {
                    return new GeneratorMenuResult(null, command);
                }

                switch (choice)
                {
                    case "1":
                        return new GeneratorMenuResult(GeneratorMode.LowRange, SystemCommand.None);

                    case "2":
                        return new GeneratorMenuResult(GeneratorMode.HighRange, SystemCommand.None);

                    case "3":
                        return new GeneratorMenuResult(GeneratorMode.Custom, SystemCommand.None);

                    default:
                        Console.WriteLine("Please pick an option from the menu.");
                        continue;
                }
            }
        }

        private void ShowRandomNumberMenu()
        {
            Console.WriteLine("Random Number Generators: \n");

            Console.WriteLine("1. Random 1 to 10.");
            Console.WriteLine("2. Random 1 to 100.");
            Console.WriteLine("3. Custom.");

            Console.WriteLine("B. Back.");
            Console.WriteLine("Q. Quit.\n");
        }
    }
}
