using System;

public class Team : INameAndCopy, IComparable
{
    protected string _organization = null!;
    protected int _registrationNumber;

    public Team(string organization, int registrationNumber)
    {
        Organization = organization;
        RegistrationNumber = registrationNumber;
    }

    public Team() : this("Невідома організація", 1)
    {
    }

    public string Organization
    {
        get { return _organization; }
        set { _organization = value; }
    }

    public int RegistrationNumber
    {
        get { return _registrationNumber; }
        set
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException("RegistrationNumber", "Реєстраційний номер повинен бути більшим за нуль.");
            }
            _registrationNumber = value;
        }
    }

    public string Name
    {
        get { return Organization; }
        set { Organization = value; }
    }

    public virtual object DeepCopy()
    {
        return new Team(Organization, RegistrationNumber);
    }

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }
        Team t = (Team)obj;
        return Organization == t.Organization && RegistrationNumber == t.RegistrationNumber;
    }

    public static bool operator ==(Team t1, Team t2)
    {
        if (ReferenceEquals(t1, t2)) return true;
        if (t1 is null || t2 is null) return false;
        return t1.Equals(t2);
    }

    public static bool operator !=(Team t1, Team t2)
    {
        return !(t1 == t2);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Organization, RegistrationNumber);
    }

    public override string ToString() => $"Team: {Organization}, Реєстр. №: {RegistrationNumber}";

    public int CompareTo(object? obj)
    {
        if (obj == null) return 1;
        if (obj is Team otherTeam)
        {
            return this.RegistrationNumber.CompareTo(otherTeam.RegistrationNumber);
        }
        throw new ArgumentException("Об'єкт не є типом Team");
    }
}