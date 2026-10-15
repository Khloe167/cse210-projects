
using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        List<Activity> activities = new List<Activity>();

        Running running = new Running(
            new DateTime(2026, 10, 15), 30, 5.0);

        Cycling cycling = new Cycling(
            new DateTime(2026, 10, 15), 45, 20.0);

        Swimming swimming = new Swimming(
            new DateTime(2026, 10, 15), 30, 40);

        activities.Add(running);
        activities.Add(cycling);
        activities.Add(swimming);

        Console.WriteLine("EXERCISE TRACKING PROGRAM");
        Console.WriteLine("-------------------------");
        Console.WriteLine();

        foreach (Activity activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
            Console.WriteLine();
        }
    }
}
