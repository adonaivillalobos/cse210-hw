using System;
using System.Collections.Generic;

// Define the Comment class
public class Comment
{
    public string CommenterName { get; set; }
    public string Text { get; set; }

    public Comment(string commenterName, string text)
    {
        CommenterName = commenterName;
        Text = text;
    }
}

// Define the Video class
public class Video
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int LengthInSeconds { get; set; }
    private List<Comment> Comments { get; set; }

    public Video(string title, string author, int lengthInSeconds)
    {
        Title = title;
        Author = author;
        LengthInSeconds = lengthInSeconds;
        Comments = new List<Comment>();
    }

    public void AddComment(Comment comment)
    {
        Comments.Add(comment);
    }

    public int GetCommentCount()
    {
        return Comments.Count;
    }

    public void DisplayVideoInfo()
    {
        Console.WriteLine($"Title: {Title}, Author: {Author}, Length: {LengthInSeconds} seconds, Comments: {GetCommentCount()}");
        foreach (var comment in Comments)
        {
            Console.WriteLine($"  Comment by {comment.CommenterName}: {comment.Text}");
        }
    }
}

public class Program
{
    public static void Main()
    {
        // Create a list of videos
        List<Video> videos = new List<Video>();

        // Create 3-4 videos
        Video video1 = new Video("Video 1", "Author A", 300);
        Video video2 = new Video("Video 2", "Author B", 600);
        Video video3 = new Video("Video 3", "Author C", 900);

        // Add comments to each video
        video1.AddComment(new Comment("User1", "Great video!"));
        video1.AddComment(new Comment("User2", "Very informative."));
        video1.AddComment(new Comment("User3", "Thanks for sharing."));

        video2.AddComment(new Comment("User4", "I learned a lot."));
        video2.AddComment(new Comment("User5", "Awesome content."));
        video2.AddComment(new Comment("User6", "Well explained."));

        video3.AddComment(new Comment("User7", "Loved the visuals."));
        video3.AddComment(new Comment("User8", "Fantastic work."));
        video3.AddComment(new Comment("User9", "Keep it up."));

        // Add videos to the list
        videos.Add(video1);
        videos.Add(video2);
        videos.Add(video3);

        // Iterate through the list of videos and display their information
        foreach (var video in videos)
        {
            video.DisplayVideoInfo();
            Console.WriteLine();
        }
    }
}