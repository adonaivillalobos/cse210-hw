using System;
using System.Collections.Generic;

// Base class for all goals
public abstract class Goal
{
    public string Name { get; set; }
    public int Points { get; set; }
    public bool IsCompleted { get; set; }
    public int Level { get; set; }

    public Goal(string name, int points)
    {
        Name = name;
        Points = points;
        Level = 1;
    }

    public abstract void RecordEvent();

    public void LevelUp()
    {
        Level++;
        Console.WriteLine($"Leveled up to level {Level}!");
    }

    public void LevelDown()
    {
        Level--;
        Console.WriteLine($"Leveled down to level {Level}!");
    }
}

// Simple goal class
public class SimpleGoal : Goal
{
    public SimpleGoal(string name, int points) : base(name, points) { }

    public override void RecordEvent()
    {
        IsCompleted = true;
        Console.WriteLine($"Completed {Name} and earned {Points} points!");
        LevelUp();
    }
}

// Eternal goal class
public class EternalGoal : Goal
{
    public EternalGoal(string name, int points) : base(name, points) { }

    public override void RecordEvent()
    {
        Console.WriteLine($"Recorded {Name} and earned {Points} points!");
        LevelUp();
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
            LevelUp();
        }
    }
}

// Marathon goal class (example of a large goal)
public class MarathonGoal : Goal
{
    public int TotalDistance { get; set; }
    public int CurrentDistance { get; set; }

    public MarathonGoal(string name, int points, int totalDistance) : base(name, points)
    {
        TotalDistance = totalDistance;
        CurrentDistance = 0;
    }

    public override void RecordEvent()
    {
        Console.Write("Enter distance completed: ");
        int distance = int.Parse(Console.ReadLine());
        CurrentDistance += distance;
        Console.WriteLine($"Recorded {distance} miles towards {Name} ({CurrentDistance}/{TotalDistance}) and earned {Points} points!");
        if (CurrentDistance >= TotalDistance)
        {
            IsCompleted = true;
            Console.WriteLine($"Completed {Name} and earned bonus {Points * 2} points!");
            LevelUp();
        }
    }
}

// Negative goal class (example of a "bad habit" goal)
public class NegativeGoal : Goal
{
    public NegativeGoal(string name, int points) : base(name, points) { }

    public override void RecordEvent()
    {
        Console.WriteLine($"Recorded {Name} and lost {Points} points!");
        LevelDown();
    }
}

// Program class
public class EternalQuest
{
    private List<Goal> Goals { get; set; }
    private int Score { get; set; }
    private int Level { get; set; }

    public EternalQuest()
    {
        Goals = new List<Goal>();
        Score = 0;
        Level = 1;
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
            case GoalType.Marathon:
                Console.Write("Enter total distance: ");
                int totalDistance = int.Parse(Console.ReadLine());
                goal = new MarathonGoal(name, points, totalDistance);
                break;
            case GoalType.Negative:
                goal = new NegativeGoal(name, points);
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
            string status = goal.IsCompleted? "" : "";
            Console.WriteLine($"{goal.Name} ({status})");
            if (goal is ChecklistGoal checklistGoal)
            {
                Console.WriteLine($"  {checklistGoal.CurrentCount}/{checklistGoal.TargetCount}");
            }
            else if (goal is MarathonGoal marathonGoal)
            {
                Console.WriteLine($"  {marathonGoal.CurrentDistance}/{marathonGoal.TotalDistance}");
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
    Checklist,
    Marathon,
    Negative
}

class Program
{
    static void Main(string[] args)
    {
        EternalQuest quest = new EternalQuest();

        while (true)
        {
            Console.WriteLine("1. Create goal");
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
                    Console.Write("Enter goal type (1=Simple, 2=Eternal, 3=Checklist, 4=Marathon, 5=Negative): ");
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