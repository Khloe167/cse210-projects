
using System;

class Program
{
    // EXCEEDING REQUIREMENTS:
    // 1. The program tracks the number of activities
    //    completed during the current session.
    // 2. The reflection activity avoids repeating
    //    questions until all questions have been used.

    static void Main(string[] args)
    {
        int activitiesCompleted = 0;
        string choice = "";

        while (choice != "4")
        {
            Console.Clear();
            Console.WriteLine("Mindfulness Program");
            Console.WriteLine();
            Console.WriteLine("Menu Options:");
            Console.WriteLine("  1. Start breathing activity");
            Console.WriteLine("  2. Start reflecting activity");
            Console.WriteLine("  3. Start listing activity");
            Console.WriteLine("  4. Quit");
            Console.WriteLine();
            Console.WriteLine($"Activities completed: {activitiesCompleted}");
            Console.Write("Select a choice from the menu: ");

            choice = Console.ReadLine();

            if (choice == "1")
            {
                BreathingActivity activity = new BreathingActivity();
                activity.Run();
                activitiesCompleted++;
            }
            else if (choice == "2")
            {
                ReflectingActivity activity = new ReflectingActivity();
                activity.Run();
                activitiesCompleted++;
            }
            else if (choice == "3")
            {
                ListingActivity activity = new ListingActivity();
                activity.Run();
                activitiesCompleted++;
            }
            else if (choice == "4")
            {
                Console.WriteLine("Thank you for using the Mindfulness Program!");
            }
            else
            {
                Console.WriteLine("Invalid choice. Please try again.");
                Thread.Sleep(2000);
            }
        }
    }
}
