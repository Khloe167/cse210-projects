using System;

// EXCEEDING REQUIREMENTS:
// I added a leveling system that rewards users with
// different titles based on their total points.
// The program also announces when a user levels up.
// I included input validation to handle incorrect entries.

class Program
{
    static void Main(string[] args)
    {
        GoalManager manager = new GoalManager();
        manager.Start();
    }
}