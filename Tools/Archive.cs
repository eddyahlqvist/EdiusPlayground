using EdiusPlayground.Core;
using EdiusPlayground.Menus;
using System;
using System.Collections.Generic;
using System.IO;

namespace EdiusPlayground.Tools
{
    internal class Archive
    {
        private const string ArchiveFile = "archive.txt";
        private List<string> _notes = new();

        private const bool IsDebugMode = true;
        
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
            LoadNotes();

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
                        ReadNote();
                        break;

                    case ArchiveMode.AddNote:
                        AddNote();
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

        private void AddNote()
        {
            Console.WriteLine("Write your note: ");
            string? input = Console.ReadLine();

            if (input != null)
            {
                _notes.Add(input);
                SaveNotes();
            }
        }

        //private void SelectNote()
        //{

        //}

        private void ListNotes()
        {
            if (_notes.Count == 0)
            {
                Console.WriteLine("The Archive is empty.");
                return;
            }

            for (int i = 0; i < _notes.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_notes[i]}");
            }
        }

        private void ReadNote()
        {            
            ListNotes();
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

        private void LoadNotes()
        {
            _notes.Clear();

            if (!File.Exists(ArchiveFile))
            {
                return;
            }

            string[] lines = File.ReadAllLines(ArchiveFile);

            foreach (string line in lines)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    _notes.Add(line);
                }
            }

            DebugMessage($"Loaded {_notes.Count} notes from {ArchiveFile}.");
        }

        private void SaveNotes()
        {
            File.WriteAllLines(ArchiveFile, _notes);
            DebugMessage($"Saved {_notes.Count} notes to {ArchiveFile}.");
        }

        private void DebugMessage(string message)
        {
            if (IsDebugMode)
            {
                ConsoleColor oldColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"DEBUG Info: {message}");
                Console.ForegroundColor = oldColor;
            }
        }
    }
}
