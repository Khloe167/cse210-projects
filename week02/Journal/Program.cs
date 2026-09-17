using System;

class Program
{
    static void Main(string[] args)
    {
        Journal journal = new Journal();

        // Creativity and exceeding requirements:
        // I added extra journal prompts and a feature that allows users
        // to see how many entries are currently in their journal.
        // I also added simple error handling when loading a file.

        Console.WriteLine("Welcome to the Journal Program!");

        bool running = true;

        while (running)
        {
            Console.WriteLine();
            Console.WriteLine("Please select one of the following choices:");
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display the journal");
            Console.WriteLine("3. Save the journal");
            Console.WriteLine("4. Load the journal");
            Console.WriteLine("5. Show number of entries");
            Console.WriteLine("6. Quit");

            Console.Write("What would you like to do? ");
            string choice = Console.ReadLine();

            Console.WriteLine();

            if (choice == "1")
            {
                string prompt = journal.GetRandomPrompt();

                Console.WriteLine(prompt);
                Console.Write("> ");
                string response = Console.ReadLine();

                string date = DateTime.Now.ToShortDateString();

                Entry newEntry = new Entry(date, prompt, response);

                journal.AddEntry(newEntry);

                Console.WriteLine("Your entry has been saved.");
            }
            else if (choice == "2")
            {
                journal.DisplayEntries();
            }
            else if (choice == "3")
            {
                Console.Write("Enter the filename: ");
                string filename = Console.ReadLine();

                journal.SaveToFile(filename);
            }
            else if (choice == "4")
            {
                Console.Write("Enter the filename: ");
                string filename = Console.ReadLine();

                try
                {
                    journal.LoadFromFile(filename);
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine("Sorry, that file could not be found.");
                }
                catch (Exception)
                {
                    Console.WriteLine("There was a problem loading the journal.");
                }
            }
            else if (choice == "5")
            {
                Console.WriteLine($"You currently have {journal._entries.Count} journal entries.");
            }
            else if (choice == "6")
            {
                running = false;
                Console.WriteLine("Thank you for using the Journal Program. Goodbye!");
            }
            else
            {
                Console.WriteLine("Invalid choice. Please select a number from 1 to 6.");
            }
        }
    }
}