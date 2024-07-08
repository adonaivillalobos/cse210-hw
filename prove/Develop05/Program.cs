using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

// Base class for all goals
public abstract class Goal
{
    public string Name { get; set; }
    public int Points { get; set; }

    public abstract int RecordEvent();
    public abstract string DisplayStatus();
}

// Simple goal that can be completed once
public class SimpleGoal : Goal
{
    public bool IsCompleted { get; set; } = false;

    public override int RecordEvent()
    {
        if (!IsCompleted)
        {
            IsCompleted = true;
            return Points;
        }
        return 0;
    }

    public override string DisplayStatus()
    {
        return IsCompleted ? $"[x] {Name}" : $"[ ] {Name}";
    }
}

// Eternal goal that can be recorded multiple times
public class EternalGoal : Goal
{
    public override int RecordEvent()
    {
        return Points;
    }

    public override string DisplayStatus()
    {
        return $"[∞] {Name}";
    }
}

// Checklist goal that needs to be completed a certain number of times
public class ChecklistGoal : Goal
{
    public int TargetCount { get; set; }
    public int CurrentCount { get; set; } = 0;
    public int BonusPoints { get; set; }

    public override int RecordEvent()
    {
        CurrentCount++;
        if (CurrentCount == TargetCount)
        {
            return Points + BonusPoints;
        }
        return Points;
    }

    public override string DisplayStatus()
    {
        return $"Completed {CurrentCount}/{TargetCount} times: {Name}";
    }
}

// User class to manage user information, goals, and score
public class User
{
    public string UserName { get; set; }
    public int Score { get; set; }
    public List<Goal> Goals { get; set; } = new List<Goal>();

    public void AddGoal(Goal goal)
    {
        Goals.Add(goal);
    }

    public void RecordEvent(string goalName)
    {
        foreach (var goal in Goals)
        {
            if (goal.Name == goalName)
            {
                Score += goal.RecordEvent();
                break;
            }
        }
    }

    public void DisplayGoals()
    {
        foreach (var goal in Goals)
        {
            Console.WriteLine(goal.DisplayStatus());
        }
    }

    public void SaveProgress(string filePath)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        var jsonString = JsonSerializer.Serialize(this, options);
        File.WriteAllText(filePath, jsonString);
    }

    public void LoadProgress(string filePath)
    {
        if (File.Exists(filePath))
        {
            var jsonString = File.ReadAllText(filePath);
            var loadedUser = JsonSerializer.Deserialize<User>(jsonString);
            UserName = loadedUser.UserName;
            Score = loadedUser.Score;
            Goals = loadedUser.Goals;
        }
    }
}

// Main program
public class Program
{
    public static void Main(string[] args)
    {
        User user = new User { UserName = "Player1" };

        // Add sample goals
        user.AddGoal(new SimpleGoal { Name = "Run a marathon", Points = 1000 });
        user.AddGoal(new EternalGoal { Name = "Read scriptures", Points = 100 });
        user.AddGoal(new ChecklistGoal { Name = "Attend temple", Points = 50, TargetCount = 10, BonusPoints = 500 });

        // Simulate recording events
        user.RecordEvent("Run a marathon");
        user.RecordEvent("Read scriptures");
        user.RecordEvent("Attend temple");

        // Display user goals and score
        user.DisplayGoals();
        Console.WriteLine($"Score: {user.Score}");

        // Save and load progress
        string filePath = "progress.json";
        user.SaveProgress(filePath);
        user.LoadProgress(filePath);

        // Display user goals and score after loading progress
        user.DisplayGoals();
        Console.WriteLine($"Score: {user.Score}");
    }
}