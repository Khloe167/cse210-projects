
using System;
using System.Collections.Generic;
using System.IO;

public class GoalManager
{
    private List<Goal> _goals = new List<Goal>();
    private int _score = 0;

    public void Start()
    {
        int choice = 0;

        while (choice != 6)
        {
            DisplayPlayerInfo();

            Console.WriteLine("\nMenu Options:");
            Console.WriteLine("1. Create New Goal");
            Console.WriteLine("2. List Goals");
            Console.WriteLine("3. Save Goals");
            Console.WriteLine("4. Load Goals");
            Console.WriteLine("5. Record Event");
            Console.WriteLine("6. Quit");

            choice = ReadInt("Select a choice from the menu: ");

            switch (choice)
            {
                case 1:
                    CreateGoal();
                    break;
                case 2:
                    ListGoals();
                    break;
                case 3:
                    SaveGoals();
                    break;
                case 4:
                    LoadGoals();
                    break;
                case 5:
                    RecordEvent();
                    break;
                case 6:
                    Console.WriteLine("Keep achieving your goals. Goodbye!");
                    break;
                default:
                    Console.WriteLine("Please select a valid option.");
                    break;
            }
        }
    }

    private int ReadInt(string message)
    {
        while (true)
        {
            Console.Write(message);

            if (int.TryParse(Console.ReadLine(), out int value))
            {
                return value;
            }

            Console.WriteLine("Please enter a valid number.");
        }
    }

    private int ReadPositiveInt(string message)
    {
        int value;

        do
        {
            value = ReadInt(message);

            if (value <= 0)
            {
                Console.WriteLine("Enter a number greater than zero.");
            }
        }
        while (value <= 0);

        return value;
    }

    private void DisplayPlayerInfo()
    {
        Console.WriteLine("\n================================");
        Console.WriteLine("       ETERNAL QUEST");
        Console.WriteLine("================================");

        Console.WriteLine($"You have {_score} points.");
        Console.WriteLine($"Current Level: {GetLevel()}");
    }

    private string GetLevel()
    {
        if (_score >= 2000)
            return "Level 4 - Eternal Quest Master";

        if (_score >= 1000)
            return "Level 3 - Goal Champion";

        if (_score >= 500)
            return "Level 2 - Rising Adventurer";

        return "Level 1 - Beginner Explorer";
    }

    private void CreateGoal()
    {
        Console.WriteLine("\nTypes of Goals:");
        Console.WriteLine("1. Simple Goal");
        Console.WriteLine("2. Eternal Goal");
        Console.WriteLine("3. Checklist Goal");

        int type = ReadInt("Which type of goal would you like to create? ");

        if (type < 1 || type > 3)
        {
            Console.WriteLine("Invalid goal type.");
            return;
        }

        Console.Write("What is the name of your goal? ");
        string name = Console.ReadLine() ?? "";

        Console.Write("What is a short description? ");
        string description = Console.ReadLine() ?? "";

        int points = ReadPositiveInt("How many points is this goal worth? ");

        if (type == 1)
        {
            _goals.Add(new SimpleGoal(name, description, points));
        }
        else if (type == 2)
        {
            _goals.Add(new EternalGoal(name, description, points));
        }
        else
        {
            int target = ReadPositiveInt("How many times must this goal be completed? ");
            int bonus = ReadPositiveInt("What is the bonus for completing it? ");

            _goals.Add(new ChecklistGoal(name, description, points, target, bonus));
        }

        Console.WriteLine("Goal created successfully!");
    }

    private void ListGoals()
    {
        Console.WriteLine("\nYour Goals:");

        if (_goals.Count == 0)
        {
            Console.WriteLine("No goals have been created yet.");
            return;
        }

        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].GetDisplayString()}");
        }
    }

    private void RecordEvent()
    {
        if (_goals.Count == 0)
        {
            Console.WriteLine("Create a goal first.");
            return;
        }

        Console.WriteLine("\nYour Goals:");

        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].GetName()}");
        }

        int number = ReadInt("Which goal did you accomplish? ");

        if (number < 1 || number > _goals.Count)
        {
            Console.WriteLine("Invalid goal selection.");
            return;
        }

        Goal goal = _goals[number - 1];

        int earned = goal.RecordEvent();

        if (earned == 0)
        {
            Console.WriteLine("This goal has already been completed.");
            return;
        }

        string previousLevel = GetLevel();

        _score += earned;

        Console.WriteLine($"Congratulations! You earned {earned} points!");
        Console.WriteLine($"Your total score is now {_score}.");

        if (previousLevel != GetLevel())
        {
            Console.WriteLine($"LEVEL UP! You are now {GetLevel()}!");
        }
    }

    private void SaveGoals()
    {
        Console.Write("Enter filename to save: ");
        string filename = Console.ReadLine() ?? "";

        try
        {
            using (StreamWriter writer = new StreamWriter(filename))
            {
                writer.WriteLine(_score);

                foreach (Goal goal in _goals)
                {
                    writer.WriteLine(goal.GetStringRepresentation());
                }
            }

            Console.WriteLine("Goals saved successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving goals: {ex.Message}");
        }
    }

    private void LoadGoals()
    {
        Console.Write("Enter filename to load: ");
        string filename = Console.ReadLine() ?? "";

        if (!File.Exists(filename))
        {
            Console.WriteLine("File not found.");
            return;
        }

        try
        {
            string[] lines = File.ReadAllLines(filename);

            if (lines.Length == 0)
            {
                Console.WriteLine("The file is empty.");
                return;
            }

            int loadedScore = int.Parse(lines[0]);
            List<Goal> loadedGoals = new List<Goal>();

            for (int i = 1; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(':', 2);

                if (parts.Length != 2)
                    continue;

                string type = parts[0];
                string[] data = parts[1].Split('|');

                string name = data[0];
                string description = data[1];
                int points = int.Parse(data[2]);

                if (type == "SimpleGoal")
                {
                    bool completed = bool.Parse(data[3]);
                    loadedGoals.Add(
                        new SimpleGoal(name, description, points, completed));
                }
                else if (type == "EternalGoal")
                {
                    loadedGoals.Add(
                        new EternalGoal(name, description, points));
                }
                else if (type == "ChecklistGoal")
                {
                    int target = int.Parse(data[3]);
                    int bonus = int.Parse(data[4]);
                    int completed = int.Parse(data[5]);

                    loadedGoals.Add(
                        new ChecklistGoal(name, description, points,
                            target, bonus, completed));
                }
            }

            _score = loadedScore;
            _goals = loadedGoals;

            Console.WriteLine("Goals loaded successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading goals: {ex.Message}");
        }
    }
}
