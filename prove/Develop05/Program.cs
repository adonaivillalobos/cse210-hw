using System;
using System.Collections.Generic;

// Base class for all goals
public abstract class Goal
{
    public string Name { get; set; }
    public int Points { get; set; }
    public bool IsCompleted { get; set; }

    public Goal(string name, int points)
    {
        Name = name;
        Points = points;
    }

    public abstract void RecordEvent();
}

// Simple goal class
public class SimpleGoal : Goal
{
    public SimpleGoal(string name, int points) : base(name, points) { }

    public override void RecordEvent()
    {
        IsCompleted = true;
        Console.WriteLine($"Completed {Name} and earned {Points} points!");
    }
}

// Eternal goal class
public class EternalGoal : Goal
{
    public EternalGoal(string name, int points) : base(name, points) { }

    public override void RecordEvent()
    {
        Console.WriteLine($"Recorded {Name} and earned {Points} points!");
    }
}

// Checklist goal class
public class ChecklistGoal : Goal
{
    public int TargetCount { get; set; }
    public int CurrentCount { get; set; }

    public ChecklistGoal(string name, int points, int targetCount) : base(name, points)
    {
        TargetCount = targetCount;
        CurrentCount = 0;
    }

    public override void RecordEvent()
    {
        CurrentCount++;
        Console.WriteLine($"Recorded {Name} ({CurrentCount}/{TargetCount}) and earned {Points} points!");
        if (CurrentCount >= TargetCount)
        {
            IsCompleted = true;
            Console.WriteLine($"Completed {Name} and earned bonus {Points * 2} points!");
        }
    }
}

// Program class
public class EternalQuest
{
    private List<Goal> Goals { get; set; }
    private int Score { get; set; }

    public EternalQuest()
    {
        Goals = new List<Goal>();
        Score = 0;
    }

    public void CreateGoal(string name, int points, GoalType type)
    {
        Goal goal;
        switch (type)
        {
            case GoalType.Simple:
                goal = new SimpleGoal(name, points);
                break;
            case GoalType.Eternal:
                goal = new EternalGoal(name, points);
                break;
            case GoalType.Checklist:
                Console.Write("Enter target count: ");
                int targetCount = int.Parse(Console.ReadLine());
                goal = new ChecklistGoal(name, points, targetCount);
                break;
            default:
                throw new ArgumentException("Invalid goal type");
        }
        Goals.Add(goal);
    }

    public void RecordEvent(string goalName)
    {
        Goal goal = Goals.Find(g => g.Name == goalName);
        if (goal != null)
        {
            goal.RecordEvent();
            Score += goal.Points;
            Console.WriteLine($"Current score: {Score}");
        }
        else
        {
            Console.WriteLine("Goal not found");
        }
    }

    public void DisplayGoals()
    {
        foreach (Goal goal in Goals)
        {
            string status = goal.IsCompleted ? "" : "";
            Console.WriteLine($"{goal.Name} ({status})");
            if (goal is ChecklistGoal checklistGoal)
            {
                Console.WriteLine($"  {checklistGoal.CurrentCount}/{checklistGoal.TargetCount}");
            }
        }
    }

    public void Save()
    {
        // TO DO: implement saving to file or database
    }

    public void Load()
    {
        // TO DO: implement loading from file or database
    }
}

public enum GoalType
{
    Simple,
    Eternal,
    Checklist
}

class Program
{
    static void Main(string[] args)
    {
        EternalQuest quest = new EternalQuest();

        while (true)
        {
            Console.WriteLine("\n1. Create goal");
            Console.WriteLine("2. Record event");
            Console.WriteLine("3. Display goals");
            Console.WriteLine("4. Save and exit");
            Console.Write("Choose an option: ");
            int option = int.Parse(Console.ReadLine());

            switch (option)
            {
                case 1:
                    Console.Write("Enter goal name: ");
                    string name = Console.ReadLine();
                    Console.Write("Enter points: ");
                    int points = int.Parse(Console.ReadLine());
                    Console.Write("Enter goal type (1=Simple, 2=Eternal, 3=Checklist): ");
                    int type = int.Parse(Console.ReadLine());
                    GoalType goalType = (GoalType)type;
                    quest.CreateGoal(name, points, goalType);
                    break;
                case 2:
                    Console.Write("Enter goal name: ");
                    string goalName = Console.ReadLine();
                    quest.RecordEvent(goalName);
                    break;
                case 3:
                    quest.DisplayGoals();
                    break;
                case 4:
                    quest.Save();
                    return;
            }
        }
    }
}