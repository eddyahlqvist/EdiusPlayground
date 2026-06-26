using System;

namespace EdiusPlayground
{
    internal class Archive
    {
        private enum ArchiveMode
        {
            ReadNote,
            AddNote,
            EditNote,
            DeleteNote,
            ArchiveNote
        }

        private readonly struct MenuResult
        {
            public ArchiveMode? Mode { get; }
            public SystemCommand Command { get; }

            public MenuResult(ArchiveMode? mode, SystemCommand command)
            {
                Mode = mode;
                Command = command;
            }
        }

        public SystemCommand Run()
        {
            while (true)
            {
                MenuResult result = RunMenu();

                switch (result.Command)
                {
                    case SystemCommand.Back:
                        return SystemCommand.Back;

                    case SystemCommand.Quit:
                        return SystemCommand.Quit;
                }

                if (result.Mode == null)
                {
                    return SystemCommand.None;
                }

                switch (result.Mode)
                {
                    case ArchiveMode.ReadNote:
                        Console.WriteLine("ReadNote, not yet implemented");
                        break;

                    case ArchiveMode.AddNote:
                        Console.WriteLine("AddNote, not yet implemented");
                        break;
                    case ArchiveMode.EditNote:
                        Console.WriteLine("EditNote, not yet implemented");
                        break;
                    case ArchiveMode.DeleteNote:
                        Console.WriteLine("DeleteNote, not yet implemented");
                        break;
                    case ArchiveMode.ArchiveNote:
                        Console.WriteLine("ArchiveNote, not yet implemented");
                        break;
                }
            }
        }

        private MenuResult RunMenu()
        {
            while (true)
            {
                ShowMenu();

                string choice = MenuHelper.GetMenuChoice();
                SystemCommand command = MenuHelper.GetSystemCommand(choice);

                if (command != SystemCommand.None)
                {
                    return new MenuResult(null, command);
                }

                switch (choice)
                {
                    case "1":
                        return new MenuResult(ArchiveMode.ReadNote, SystemCommand.None);

                    case "2":
                        return new MenuResult(ArchiveMode.AddNote, SystemCommand.None);

                    case "3":
                        return new MenuResult(ArchiveMode.EditNote, SystemCommand.None);

                    case "4":
                        return new MenuResult(ArchiveMode.DeleteNote, SystemCommand.None);

                    case "5":
                        return new MenuResult(ArchiveMode.ArchiveNote, SystemCommand.None);

                    default:
                        Console.WriteLine();
                        Console.WriteLine("Please pick an option from the menu.");
                        continue;
                }
            }
        }
        private void ShowMenu()
        {
            Console.WriteLine();
            Console.WriteLine("== Archive ==\n");

            Console.WriteLine("1. Read Notes");
            Console.WriteLine("2. Add Note");
            Console.WriteLine("3. Edit Note");
            Console.WriteLine("4. Delete Note");
            Console.WriteLine("5. Archive Completed Note");

            Console.WriteLine("B. Back.");
            Console.WriteLine("Q. Quit.\n");
        }
    }
}
