using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace JournalApp
{
    class Program
    {
        // Entry class to store individual journal entries
        class Entry
        {
            public string Prompt { get; set; }
            public string Response { get; set; }
            public DateTime Date { get; set; }

            public Entry(string prompt, string response)
            {
                Prompt = prompt;
                Response = response;
                Date = DateTime.Now;
            }

            public override string ToString()
            {
                return $"{Date.ToShortDateString()} - Prompt: {Prompt}\nResponse: {Response}\n";
            }
        }

        // Journal class to manage journal entries
        class Journal
        {
            public List<Entry> Entries { get; private set; } = new List<Entry>();

            // Add a new entry
            public void AddEntry(Entry entry)
            {
                Entries.Add(entry);
            }

            // Display all entries
            public void DisplayEntries()
            {
                if (Entries.Count == 0)
                {
                    Console.WriteLine("The journal is empty.");
                }
                else
                {
                    foreach (var entry in Entries)
                    {
                        Console.WriteLine(entry);
                    }
                }
            }

            // Save entries to a file
            public void SaveToFile(string filename)
            {
                var json = JsonSerializer.Serialize(Entries, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filename, json);
                Console.WriteLine($"Journal saved to {filename}");
            }

            // Load entries from a file
            public void LoadFromFile(string filename)
            {
                if (File.Exists(filename))
                {
                    var json = File.ReadAllText(filename);
                    Entries = JsonSerializer.Deserialize<List<Entry>>(json);
                    Console.WriteLine($"Journal loaded from {filename}");
                }
                else
                {
                    Console.WriteLine($"File {filename} does not exist.");
                }
            }
        }

        static void Main(string[] args)
        {
            Journal journal = new Journal();
            List<string> prompts = new List<string>
            {
                "What was the highlight of your day?",
                "Describe a challenge you faced today.",
                "What are you grateful for today?",
                "What did you learn today?",
                "Write about something that made you smile today."
            };

            while (true)
            {
                Console.WriteLine("\nJournal Menu:");
                Console.WriteLine("1. Write a new entry");
                Console.WriteLine("2. Display the journal");
                Console.WriteLine("3. Save the journal to a file");
                Console.WriteLine("4. Load the journal from a file");
                Console.WriteLine("5. Exit");
                Console.Write("Choose an option (1-5): ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        WriteNewEntry(journal, prompts);
                        break;
                    case "2":
                        journal.DisplayEntries();
                        break;
                    case "3":
                        SaveJournal(journal);
                        break;
                    case "4":
                        LoadJournal(journal);
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please choose again.");
                        break;
                }
            }
        }

        static void WriteNewEntry(Journal journal, List<string> prompts)
        {
            Random random = new Random();
            int promptIndex = random.Next(prompts.Count);
            string prompt = prompts[promptIndex];

            Console.WriteLine($"\nPrompt: {prompt}");
            Console.Write("Your response: ");
            string response = Console.ReadLine();

            Entry entry = new Entry(prompt, response);
            journal.AddEntry(entry);

            Console.WriteLine("Entry saved.");
        }

        static void SaveJournal(Journal journal)
        {
            Console.Write("Enter the filename to save the journal: ");
            string filename = Console.ReadLine();
            journal.SaveToFile(filename);
        }

        static void LoadJournal(Journal journal)
        {
            Console.Write("Enter the filename to load the journal from: ");
            string filename = Console.ReadLine();
            journal.LoadFromFile(filename);
        }
    }
}
