
using System;

public class BreathingActivity : Activity
{
    public BreathingActivity()
        : base(
            "Breathing Activity",
            "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.")
    {
    }

    public void Run()
    {
        DisplayStartingMessage();

        int duration = GetDuration();
        DateTime endTime = DateTime.Now.AddSeconds(duration);

        while (DateTime.Now < endTime)
        {
            int remaining = (int)Math.Ceiling(
                (endTime - DateTime.Now).TotalSeconds);

            if (remaining <= 0)
            {
                break;
            }

            Console.Write("Breathe in...");
            ShowCountDown(Math.Min(4, remaining));
            Console.WriteLine();

            remaining = (int)Math.Ceiling(
                (endTime - DateTime.Now).TotalSeconds);

            if (remaining <= 0)
            {
                break;
            }

            Console.Write("Breathe out...");
            ShowCountDown(Math.Min(6, remaining));
            Console.WriteLine();
            Console.WriteLine();
        }

        DisplayEndingMessage();
    }
}
