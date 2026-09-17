using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

// EXCEEDING REQUIREMENTS / STRETCH CHALLENGES ADDED:
// 1. Only selects words that are not already hidden when hiding random words.
// 2. Contains a Scripture Library feature: Loads scriptures from an external text file ("scriptures.txt") 
//    if available, or falls back to an internal library of multiple scriptures and picks one at random.

class Program
{
    static void Main(string[] args)
    {
        // Exceeding Requirements: Load scriptures from library / file and pick one at random
        List<Scripture> library = LoadScriptureLibrary();
        Random random = new Random();
        Scripture scripture = library[random.Next(library.Count)];

        while (true)
        {
            Console.Clear();
            Console.WriteLine(scripture.GetDisplayText());
            Console.WriteLine("\nPress Enter to hide words, or type 'quit' to exit.");

            string input = Console.ReadLine();

            if (input?.Trim().ToLower() == "quit")
            {
                break;
            }

            if (scripture.IsCompletelyHidden())
            {
                break;
            }

            // Hide 3 random words each turn
            scripture.HideRandomWords(3);
        }

        // Display final state before exiting
        Console.Clear();
        Console.WriteLine(scripture.GetDisplayText());
        Console.WriteLine("\nAll words are hidden. Program finished!");
    }

    private static List<Scripture> LoadScriptureLibrary()
    {
        List<Scripture> library = new List<Scripture>();
        string filePath = "scriptures.txt";

        // Try to load from external file if it exists
        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                // Expected file format: Book|Chapter|StartVerse|EndVerse|Text
                // For single verse: Book|Chapter|StartVerse|0|Text
                string[] parts = line.Split('|');
                if (parts.Length == 5)
                {
                    string book = parts[0];
                    int chapter = int.Parse(parts[1]);
                    int startVerse = int.Parse(parts[2]);
                    int endVerse = int.Parse(parts[3]);
                    string text = parts[4];

                    Reference reference = endVerse > 0
                        ? new Reference(book, chapter, startVerse, endVerse)
                        : new Reference(book, chapter, startVerse);

                    library.Add(new Scripture(reference, text));
                }
            }
        }

        // Fallback default library if file is missing or empty
        if (library.Count == 0)
        {
            library.Add(new Scripture(
                new Reference("Proverbs", 3, 5, 6),
                "Trust in the LORD with all thine heart; and lean not unto thine own understanding. In all thy ways acknowledge him, and he shall direct thy paths."
            ));
            library.Add(new Scripture(
                new Reference("John", 3, 16),
                "For God so loved the world, that he gave his only begotten Son, that whosoever believeth in him should not perish, but have everlasting life."
            ));
            library.Add(new Scripture(
                new Reference("Philippians", 4, 13),
                "I can do all things through Christ which strengtheneth me."
            ));
        }

        return library;
    }
}

public class Reference
{
    private string _book;
    private int _chapter;
    private int _verse;
    private int _endVerse;

    public Reference(string book, int chapter, int verse)
    {
        _book = book;
        _chapter = chapter;
        _verse = verse;
        _endVerse = 0; // Represents a single-verse reference
    }

    public Reference(string book, int chapter, int startVerse, int endVerse)
    {
        _book = book;
        _chapter = chapter;
        _verse = startVerse;
        _endVerse = endVerse;
    }

    public string GetDisplayText()
    {
        if (_endVerse > 0 && _endVerse != _verse)
        {
            return $"{_book} {_chapter}:{_verse}-{_endVerse}";
        }
        return $"{_book} {_chapter}:{_verse}";
    }
}

public class Scripture
{
    private Reference _reference;
    private List<Word> _words;

    public Scripture(Reference reference, string text)
    {
        _reference = reference;
        _words = new List<Word>();

        string[] splitWords = text.Split(' ');
        foreach (string word in splitWords)
        {
            _words.Add(new Word(word));
        }
    }

    public void HideRandomWords(int numberToHide)
    {
        Random random = new Random();

        // Filter only words that are NOT hidden (Exceeding requirement)
        List<Word> unhiddenWords = _words.Where(w => !w.IsHidden()).ToList();

        int countToHide = Math.Min(numberToHide, unhiddenWords.Count);

        for (int i = 0; i < countToHide; i++)
        {
            int index = random.Next(unhiddenWords.Count);
            unhiddenWords[index].Hide();
            unhiddenWords.RemoveAt(index); // Ensure no duplicate pick in same iteration
        }
    }

    public string GetDisplayText()
    {
        List<string> wordDisplays = new List<string>();
        foreach (Word word in _words)
        {
            wordDisplays.Add(word.GetDisplayText());
        }

        return $"{_reference.GetDisplayText()} {string.Join(" ", wordDisplays)}";
    }

    public bool IsCompletelyHidden()
    {
        return _words.All(w => w.IsHidden());
    }
}

public class Word
{
    private string _text;
    private bool _isHidden;

    public Word(string text)
    {
        _text = text;
        _isHidden = false;
    }

    public void Hide()
    {
        _isHidden = true;
    }

    public void Show()
    {
        _isHidden = false;
    }

    public bool IsHidden()
    {
        return _isHidden;
    }

    public string GetDisplayText()
    {
        if (_isHidden)
        {
            // Preserve punctuation attached to words while replacing letters with underscores
            string hiddenWord = "";
            foreach (char c in _text)
            {
                if (char.IsLetterOrDigit(c))
                {
                    hiddenWord += "_";
                }
                else
                {
                    hiddenWord += c; // Retain punctuation like commas and periods
                }
            }
            return hiddenWord;
        }

        return _text;
    }
}