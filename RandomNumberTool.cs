using System;

namespace EdiusPlayground
{
    internal class RandomNumberTool
    {
        private static readonly Random _rnd = new Random();

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
                return SystemCommand.None;
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
            Console.WriteLine($"Your random number is: {_randomNumber}");

            return SystemCommand.None;
        }

        private void RunGenerator(GeneratorMode chosenMode)
        {
            if (chosenMode == GeneratorMode.LowRange)
            {
                _lowNumber = 1;
                _highNumber = 10;
                _randomNumber = GenerateRandomNumber();
                return;
            }

            if (chosenMode == GeneratorMode.HighRange)
            {
                _lowNumber = 1;
                _highNumber = 100;
                _randomNumber = GenerateRandomNumber();
                return;
            }

            if (chosenMode == GeneratorMode.Custom)
            {
                // will soon start working on this
                Console.WriteLine("Currently not working");
                return;
            }
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
            Console.WriteLine("Q. Quit.");
        }
    }
}
