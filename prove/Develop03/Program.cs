using System;
using System.Collections.Generic;
using System.Linq;

public class Word
{
    public string Text { get; set; }
    public bool IsHidden { get; set; }

    public Word(string text)
    {
        Text = text;
        IsHidden = false;
    }
}

public class ScriptureReference
{
    public string Book { get; set; }
    public int StartChapter { get; set; }
    public int StartVerse { get; set; }
    public int? EndChapter { get; set; }
    public int? EndVerse { get; set; }

    public ScriptureReference(string book, int startChapter, int startVerse)
    {
        Book = book;
        StartChapter = startChapter;
        StartVerse = startVerse;
    }

    public ScriptureReference(string book, int startChapter, int startVerse, int endChapter, int endVerse)
    {
        Book = book;
        StartChapter = startChapter;
        StartVerse = startVerse;
        EndChapter = endChapter;
        EndVerse = endVerse;
    }

    public override string ToString()
    {
        if (EndChapter.HasValue && EndVerse.HasValue)
        {
            return $"{Book} {StartChapter}:{StartVerse}-{EndChapter}:{EndVerse}";
        }
        else
        {
            return $"{Book} {StartChapter}:{StartVerse}";
        }
    }
}

public class Scripture
{
    public ScriptureReference Reference { get; set; }
    public List<Word> Words { get; set; }

    public Scripture(ScriptureReference reference, string text)
    {
        Reference = reference;
        Words = text.Split(' ').Select(w => new Word(w)).ToList();
    }

    public void HideRandomWord()
    {
        Random rand = new Random();
        int index = rand.Next(Words.Count);
        if (!Words[index].IsHidden)
        {
            Words[index].IsHidden = true;
        }
        else
        {
            HideRandomWord();
        }
    }

    public override string ToString()
    {
        return $"{Reference} - {string.Join(" ", Words.Select(w => w.IsHidden ? "[hidden]" : w.Text))}";
    }
}

class Program
{
    static void Main(string[] args)
    {
        Scripture scripture = new Scripture(new ScriptureReference("John", 3, 16), "For God so loved the world, that he gave his only begotten Son, that whosoever believeth in him should not perish, but have everlasting life.");

        while (true)
        {
            Console.Clear();
            Console.WriteLine(scripture.ToString());

            Console.Write("Press enter to continue or type quit to exit: ");
            string input = Console.ReadLine();
            if (input.ToLower() == "quit")
            {
                break;
            }

            scripture.HideRandomWord();
        }

        Console.WriteLine("Thanks for playing!");
        Console.ReadKey();
    }
}