using System.Collections.Generic;
using System.IO;

public class Journal
{
    public List<Entry> _entries = new List<Entry>();
    private readonly Random _random = new Random();

    private List<string> _prompts = new List<string>
    {
        "What was the best part of my day?",
        "Who was the most interesting person I interacted with today?",
        "What is something new I learned today?",
        "What was the strongest emotion I felt today?",
        "What is one thing I am grateful for today?",
        "What challenge did I face today, and how did I handle it?",
        "What is one thing I hope to accomplish tomorrow?"
    };


public string GetRandomPrompt()
{
    Random random = new Random();
    int index = random.Next(_prompts.Count);
    return _prompts[index];
}
    public void AddEntry(Entry entry)
    {
        _entries.Add(entry);
    }

    public void DisplayEntries()
    {
        if (_entries.Count == 0)
        {
            Console.WriteLine("Your journal is empty.");
            return;
        }

        foreach (Entry entry in _entries)
        {
            entry.Display();
        }
    }

    public void SaveToFile(string filename)
    {
        using (StreamWriter outputFile = new StreamWriter(filename))
        {
            foreach (Entry entry in _entries)
            {
                outputFile.WriteLine(entry.GetFileFormat());
            }
        }

        Console.WriteLine("Journal saved successfully!");
    }

    public void LoadFromFile(string filename)
    {
        string[] lines = File.ReadAllLines(filename);

        _entries.Clear();

        foreach (string line in lines)
        {
            string[] parts = line.Split('|');

            if (parts.Length >= 3)
            {
                string date = parts[0];
                string prompt = parts[1];
                string response = string.Join("|", parts, 2, parts.Length - 2);

                Entry entry = new Entry(date, prompt, response);
                _entries.Add(entry);
            }
        }

        Console.WriteLine("Journal loaded successfully!");
    }
}

