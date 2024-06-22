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

            // Save entries to a JSON file
            public void SaveToFile(string filename)
            {
                var json = JsonSerializer.Serialize(Entries, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filename, json);
                Console.WriteLine($"Journal saved to {filename}");
            }

            // Load entries from a JSON file
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
                        DisplayExitMessage();
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

            DisplayConsistencyMessage();
            DisplayAvoidanceTips();
        }

        static void SaveJournal(Journal journal)
        {
            const string filename = "journal.txt";
            journal.SaveToFile(filename);
        }

        static void LoadJournal(Journal journal)
        {
            const string filename = "journal.txt";
            journal.LoadFromFile(filename);
        }

        static void DisplayConsistencyMessage()
        {
            Console.WriteLine("\nRemember, to gain the most from your journal, consistency is key. Try to write regularly to build a habit.");
            Console.WriteLine("Journaling can help you process your thoughts, track your progress, and reflect on your experiences.");
        }

        static void DisplayAvoidanceTips()
        {
            Console.WriteLine("\nTips on Why People Avoid Writing a Journal and How to Overcome It:");
            Console.WriteLine("1. **Lack of Time**: Schedule a few minutes daily. Consistency matters more than the length of entries.");
            Console.WriteLine("2. **Fear of Judgment**: Your journal is for you. Write freely without worrying about others' opinions.");
            Console.WriteLine("3. **Perfectionism**: Your entries don’t have to be perfect. Focus on expressing your thoughts honestly.");
            Console.WriteLine("4. **Not Knowing What to Write**: Use prompts to get started. Write about your day, feelings, or random thoughts.");
            Console.WriteLine("5. **Difficulty in Forming a Habit**: Link journaling to an existing habit, like before bed or with your morning coffee.");
        }

        static void DisplayExitMessage()
        {
            Console.WriteLine("\nThank you for using the journal! Remember:");
            Console.WriteLine("1. Consistency is key: Make journaling a regular habit.");
            Console.WriteLine("2. Your journal is your safe space: Write freely and honestly.");
            Console.WriteLine("3. Overcome barriers: Everyone faces challenges in journaling, but with small steps, you can make it a rewarding part of your life.");
            Console.WriteLine("\nHappy journaling and see you next time!");
        }
    }
}