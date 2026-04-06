using System;

public class Person
{
    private string _firstName = null!;
    private string _lastName = null!;
    private DateTime _birthDate;

    public Person(string firstName, string lastName, DateTime birthDate)
    {
        FirstName = firstName;
        LastName = lastName;
        BirthDate = birthDate;
    }

    public Person() : this("Невідомо", "Невідомо", new DateTime(1, 1, 1))
    {
    }

    public string FirstName
    {
        get { return _firstName; }
        set { _firstName = value; } 
    }

    public string LastName
    {
        get { return _lastName; }
        set { _lastName = value; }
    }

    public DateTime BirthDate
    {
        get { return _birthDate; }
        set { _birthDate = value; }
    }

    public int BirthYear
    {
        get { return _birthDate.Year; }
        set {
            _birthDate = new DateTime(value, _birthDate.Month, _birthDate.Day); 
        }
    }

    public override string ToString() => $"Person: {FirstName} {LastName}, Дата народження: {BirthDate.ToShortDateString()}";

    public virtual string ToShortString() => $"{FirstName} {LastName}";

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        Person p = (Person)obj;
        return FirstName == p.FirstName &&
               LastName == p.LastName &&
               BirthDate == p.BirthDate;
    }

    public static bool operator ==(Person p1, Person p2)
    {
        if (ReferenceEquals(p1, p2)) return true;
        if (p1 is null || p2 is null) return false;
        return p1.Equals(p2);
    }

    public static bool operator !=(Person p1, Person p2)
    {
        return !(p1 == p2);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(FirstName, LastName, BirthDate);
    }

    public virtual object DeepCopy()
    {
        return new Person(FirstName, LastName, BirthDate);
    }
}