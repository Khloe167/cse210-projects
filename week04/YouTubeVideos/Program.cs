using System;
using System.Collections.Generic;

namespace YouTubeVideos
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Video> videos = new List<Video>();

            // ----------------------------------------------------
            // Video 1
            // ----------------------------------------------------
            Video video1 = new Video("C# Abstraction Explained", "Code with Tim", 600);
            video1.AddComment(new Comment("Alice", "Great explanation of OOP concepts!"));
            video1.AddComment(new Comment("Bob", "This really helped me understand classes."));
            video1.AddComment(new Comment("Charlie", "Can you cover Encapsulation in the next video?"));
            videos.Add(video1);

            // ----------------------------------------------------
            // Video 2
            // ----------------------------------------------------
            Video video2 = new Video("Top 10 Web Development Frameworks", "Tech Byte", 900);
            video2.AddComment(new Comment("David", "React is definitely my favorite."));
            video2.AddComment(new Comment("Eve", "Vue.js deserves more recognition."));
            video2.AddComment(new Comment("Frank", "Awesome comparison video!"));
            videos.Add(video2);

            // ----------------------------------------------------
            // Video 3
            // ----------------------------------------------------
            Video video3 = new Video("How to Build a REST API in .NET", "Dev Solutions", 1200);
            video3.AddComment(new Comment("Grace", "Very clear step-by-step tutorial."));
            video3.AddComment(new Comment("Hank", "Ran into an issue on step 3, but solved it!"));
            video3.AddComment(new Comment("Ivy", "Subscribed! Keep making content like this."));
            videos.Add(video3);

            // ----------------------------------------------------
            // Video 4
            // ----------------------------------------------------
            Video video4 = new Video("CSS Grid vs Flexbox Layouts", "Design Code", 450);
            video4.AddComment(new Comment("Jack", "Flexbox for 1D, Grid for 2D layout. Perfect!"));
            video4.AddComment(new Comment("Karen", "Loved the visual layout examples."));
            video4.AddComment(new Comment("Leo", "Short, concise, and right to the point."));
            videos.Add(video4);

            // ----------------------------------------------------
            // Display Output
            // ----------------------------------------------------
            foreach (Video video in videos)
            {
                Console.WriteLine("==================================================");
                Console.WriteLine($"Title: {video.GetTitle()}");
                Console.WriteLine($"Author: {video.GetAuthor()}");
                Console.WriteLine($"Length: {video.GetLengthInSeconds()} seconds");
                Console.WriteLine($"Total Comments: {video.GetCommentCount()}");
                Console.WriteLine("--------------------------------------------------");
                Console.WriteLine("Comments:");

                foreach (Comment comment in video.GetComments())
                {
                    Console.WriteLine($" - {comment.GetName()}: \"{comment.GetText()}\"");
                }

                Console.WriteLine();
            }
        }
    }
}