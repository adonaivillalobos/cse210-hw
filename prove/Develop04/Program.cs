using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace ActivityProgram
{
    abstract class Activity
    {
        public abstract string Name { get; }
        protected List<string> Prompts { get; set; }
        protected List<string> UsedPrompts { get; set; }
        protected Random Random { get; } = new Random();

        public virtual void Start()
        {
            Console.Clear();
            Console.WriteLine($"{Name} Activity");
            Console.WriteLine(GetDescription());
            int duration = GetDuration();
            Console.WriteLine("Prepare to start...");
            Pause(3);
            PerformActivity(duration);
            EndActivity();
        }

        protected abstract void PerformActivity(int duration);
        protected abstract string GetDescription();

        protected string GetRandomPrompt()
        {
            if (Prompts.Count == 0)
            {
                Prompts.AddRange(UsedPrompts);
                UsedPrompts.Clear();
            }

            int index = Random.Next(Prompts.Count);
            string prompt = Prompts[index];
            Prompts.RemoveAt(index);
            UsedPrompts.Add(prompt);

            return prompt;
        }

        protected int GetDuration()
        {
            Console.Write("Enter the duration of the activity in seconds: ");
            int duration;
            while (!int.TryParse(Console.ReadLine(), out duration) || duration <= 0)
            {
                Console.WriteLine("Please enter a valid positive number.");
            }
            return duration;
        }

        protected void Pause(int seconds)
        {
            for (int i = 0; i < seconds; i++)
            {
                Console.Write(".");
                Thread.Sleep(1000);
            }
            Console.WriteLine();
        }

        protected void EndActivity()
        {
            Console.WriteLine($"You have completed the {Name} Activity.");
            Console.WriteLine("Well done!");
            Pause(3);
            LogActivity();
            Console.WriteLine("Press any key to return to the main menu...");
            Console.ReadKey();
        }

        protected void LogActivity()
        {
            string logFilePath = "activity_log.txt";
            File.AppendAllText(logFilePath, $"{DateTime.Now}: Completed {Name} Activity\n");
        }
    }

    class BreathingActivity : Activity
    {
        public override string Name => "Breathing";
        protected override string GetDescription() => 
            "This activity will help you relax by guiding you through slow breathing.";

        protected override void PerformActivity(int duration)
        {
            for (int i = 0; i < duration / 5; i++)
            {
                Console.WriteLine("Breathe in...");
                Pause(2);
                Console.WriteLine("Breathe out...");
                Pause(2);
            }
        }
    }

    class ReflectionActivity : Activity
    {
        public override string Name => "Reflection";

        public ReflectionActivity()
        {
            Prompts = new List<string>
            {
                "Think of a time when you stood up for someone else.",
                "Think of a time when you did something really difficult.",
                "Think of a time when you helped someone in need.",
                "Think of a time when you did something truly selfless."
            };
            UsedPrompts = new List<string>();
        }

        protected override string GetDescription() =>
            "This activity will help you reflect on your life and recognize your strengths.";

        protected override void PerformActivity(int duration)
        {
            List<string> questions = new List<string>
            {
                "Why was this experience meaningful to you?",
                "Have you ever done anything like this before?",
                "How did you get started?",
                "How did you feel when it was complete?",
                "What made this time different than other times when you were not as successful?",
                "What is your favorite thing about this experience?",
                "What could you learn from this experience that applies to other situations?",
                "What did you learn about yourself through this experience?",
                "How can you keep this experience in mind in the future?"
            };

            for (int i = 0; i < duration / 10; i++)
            {
                Console.WriteLine(GetRandomPrompt());
                Pause(5);
                Console.WriteLine(questions[Random.Next(questions.Count)]);
                Pause(5);
            }
        }
    }

    class ListingActivity : Activity
    {
        public override string Name => "Listing";

        public ListingActivity()
        {
            Prompts = new List<string>
            {
                "Who are people that you appreciate?",
                "What are personal strengths of yours?",
                "Who are people that you have helped this week?",
                "When have you felt the Holy Ghost this month?",
                "Who are some of your personal heroes?"
            };
            UsedPrompts = new List<string>();
        }

        protected override string GetDescription() =>
            "This activity will help you reflect on the good things in your life.";

        protected override void PerformActivity(int duration)
        {
            Console.WriteLine(GetRandomPrompt());
            Pause(5);

            DateTime start = DateTime.Now;
            while ((DateTime.Now - start).TotalSeconds < duration)
            {
                Console.Write("Enter an item: ");
                string item = Console.ReadLine();
            }
        }
    }

    class VisualizationActivity : Activity
    {
        public override string Name => "Visualization";

        public VisualizationActivity()
        {
            Prompts = new List<string>
            {
                "Imagine a place where you feel completely at peace.",
                "Visualize yourself achieving a personal goal.",
                "Picture yourself surrounded by your loved ones, feeling their support.",
                "Think of a relaxing scene in nature, such as a beach or a forest."
            };
            UsedPrompts = new List<string>();
        }

        protected override string GetDescription() =>
            "This activity will help you visualize positive and calming scenarios.";

        protected override void PerformActivity(int duration)
        {
            Console.WriteLine(GetRandomPrompt());
            Pause(duration);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Welcome to the Activity Program!");
                Console.WriteLine("Choose an activity:");
                Console.WriteLine("1. Breathing Activity");
                Console.WriteLine("2. Reflection Activity");
                Console.WriteLine("3. Listing Activity");
                Console.WriteLine("4. Visualization Activity");
                Console.WriteLine("5. View Activity Log");
                Console.WriteLine("6. Exit");
                Console.Write("Enter your choice: ");
                
                switch (Console.ReadLine())
                {
                    case "1":
                        new BreathingActivity().Start();
                        break;
                    case "2":
                        new ReflectionActivity().Start();
                        break;
                    case "3":
                        new ListingActivity().Start();
                        break;
                    case "4":
                        new VisualizationActivity().Start();
                        break;
                    case "5":
                        ViewActivityLog();
                        break;
                    case "6":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        static void ViewActivityLog()
        {
            string logFilePath = "activity_log.txt";
            if (File.Exists(logFilePath))
            {
                Console.WriteLine("Activity Log:");
                Console.WriteLine(File.ReadAllText(logFilePath));
            }
            else
            {
                Console.WriteLine("No activities have been logged yet.");
            }
            Console.WriteLine("Press any key to return to the main menu...");
            Console.ReadKey();
        }
    }
}
