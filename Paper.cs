using System;

public class Paper
{
    public string Title { get; set; }
    public Person Author { get; set; }
    public DateTime PublicationDate { get; set; }

    public Paper(string title, Person author, DateTime publicationDate)
    {
        Title = title;
        Author = author;
        PublicationDate = publicationDate;
    }

    public Paper() : this("Без назви", new Person(), DateTime.Now) 
    { 
    }

    public override string ToString() => $"Публікація: '{Title}', Автор: {Author.ToShortString()}, Дата: {PublicationDate.ToShortDateString()}";

    public virtual object DeepCopy()
    {
        Person copiedAuthor = (Person)Author.DeepCopy();
        return new Paper(Title, copiedAuthor, PublicationDate);
    }

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        Paper p = (Paper)obj;
        return Title == p.Title &&
               Author.Equals(p.Author) &&
               PublicationDate == p.PublicationDate;
    }

    public static bool operator ==(Paper p1, Paper p2)
    {
        if (ReferenceEquals(p1, p2)) return true;
        if (p1 is null || p2 is null) return false;
        return p1.Equals(p2);
    }

    public static bool operator !=(Paper p1, Paper p2)
    {
        return !(p1 == p2);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Title, Author, PublicationDate);
    }
}