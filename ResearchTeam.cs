using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

public class ResearchTeam : Team, IEnumerable, IComparer<ResearchTeam>
{
    private string _researchTopic  = null!;
    private TimeFrame _duration;
    private ImmutableList<Person> _participants = ImmutableList<Person>.Empty;
    private ImmutableList<Paper> _publications = ImmutableList<Paper>.Empty;

    public ResearchTeam(string researchTopic, string organization, int registrationNumber, TimeFrame duration)
        : base(organization, registrationNumber)
    {
        ResearchTopic = researchTopic;
        Duration = duration;
        Participants = ImmutableList<Person>.Empty;
        Publications = ImmutableList<Paper>.Empty;
    }

    public ResearchTeam() : this("Невідома тема", "Невідома організація", 1, TimeFrame.Year)
    {
    }

    public string ResearchTopic
    {
        get { return _researchTopic; }
        set { _researchTopic = value; }
    }

    public TimeFrame Duration
    {
        get { return _duration; }
        set { _duration = value; }
    }

    public ImmutableList<Paper> Publications
    {
        get { return _publications; }
        set { _publications = value; }
    }

    public ImmutableList<Person> Participants
    {
        get { return _participants; }
        set { _participants = value; }
    }

    public Paper? LatestPaper
    {
        get
        {
            if (_publications == null || _publications.Count == 0)
            {
                return null;
            }

            Paper latest = _publications[0];
            
            for (int i = 1; i < _publications.Count; i++)
            {
                Paper current = _publications[i];
                if (current.PublicationDate > latest.PublicationDate)
                {
                    latest = current;
                }
            }
            return latest;
        }
    }

    public bool this[TimeFrame index] => Duration == index;

    public void AddPapers(params Paper[] newPapers)
    {
        if (newPapers != null)
        {
            _publications = _publications.AddRange(newPapers);
        }
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"Дослідження: '{ResearchTopic}' ({Organization}), Реєстр. №: {RegistrationNumber}, Тривалість: {Duration}");
        sb.AppendLine("Список публікацій:");

        if (_publications.Count == 0)
        {
            sb.AppendLine("  Публікацій ще немає.");
        }
        else
        {
            foreach (Paper p in _publications) 
            {
                sb.AppendLine($"  - {p.ToString()}");
            }
        }

        sb.AppendLine("Список учасників:");
        if (_participants.Count == 0)
        {
            sb.AppendLine("  Учасників ще немає.");
        }
        else
        {
            foreach (Person p in _participants) 
            {
                sb.AppendLine($"  - {p.ToString()}");
            }
        }

        return sb.ToString();
    }

    public virtual string ToShortString() =>
    $"Дослідження: '{ResearchTopic}' ({Organization}), Реєстр. №: {RegistrationNumber}, Тривалість: {Duration}";

    public Team TeamBase
    {
        get { return this; }
        set
        {
            Organization = value.Organization;
            RegistrationNumber = value.RegistrationNumber;
        }
    }

    public void AddParticipants(params Person[] newParticipants)
    {
        if (newParticipants != null)
        {
            _participants = _participants.AddRange(newParticipants);
        }
    }

    public override object DeepCopy()
    {
        ResearchTeam copy = new ResearchTeam(ResearchTopic, Organization, RegistrationNumber, Duration);
        
        foreach (Person p in _participants)
        {
            copy.AddParticipants((Person)p.DeepCopy());
        }

        foreach (Paper p in _publications)
        {
            copy.AddPapers((Paper)p.DeepCopy());
        }

        return copy;
    }

    public override bool Equals(object? obj)
    {
        if (!base.Equals(obj)) 
        {
            return false;
        }

        ResearchTeam rt = (ResearchTeam)obj;

        if (ResearchTopic != rt.ResearchTopic || Duration != rt.Duration)
        {
            return false;
        }

        if (Participants.Count != rt.Participants.Count || Publications.Count != rt.Publications.Count)
        {
            return false;
        }

        for (int i = 0; i < Participants.Count; i++)
        {
            if (!Participants[i].Equals(rt.Participants[i]))
            {
                return false;
            }
        }

        for (int i = 0; i < Publications.Count; i++)
        {
            if (!Publications[i].Equals(rt.Publications[i]))
            {
                return false;
            }
        }

        return true;
    }

    public static bool operator ==(ResearchTeam rt1, ResearchTeam rt2)
    {
        if (ReferenceEquals(rt1, rt2)) return true;
        if (rt1 is null || rt2 is null) return false;
        return rt1.Equals(rt2);
    }

    public static bool operator !=(ResearchTeam rt1, ResearchTeam rt2)
    {
        return !(rt1 == rt2);
    }

    public override int GetHashCode()
    {
        int hash = base.GetHashCode();
        hash = HashCode.Combine(hash, ResearchTopic, Duration, Participants.Count, Publications.Count);
        return hash;
    }

    public IEnumerable GetPersonsWithoutPublications()
    {
        foreach (Person p in _participants)
        {
            bool hasPublication = false;
            foreach (Paper paper in _publications)
            {
                if (paper.Author.Equals(p))
                {
                    hasPublication = true;
                    break;
                }
            }
            if (!hasPublication)
            {
                yield return p;
            }
        }
    }

    public IEnumerable GetRecentPublications(int years)
    {
        DateTime limit = DateTime.Now.AddYears(-years);
        foreach (Paper p in _publications)
        {
            if (p.PublicationDate >= limit)
            {
                yield return p;
            }
        }
    }

    public IEnumerator GetEnumerator()
    {
        return new ResearchTeamEnumerator(new List<Person>(_participants), new List<Paper>(_publications));
    }

    public IEnumerable GetPersonsWithMultiplePublications()
    {
        foreach (Person p in _participants)
        {
            int count = 0;
            foreach (Paper paper in _publications)
            {
                if (paper.Author.Equals(p))
                {
                    count++;
                }
            }
            if (count > 1)
            {
                yield return p;
            }
        }
    }

    public IEnumerable GetPublicationsFromLastYear()
    {
        DateTime limit = DateTime.Now.AddYears(-1);
        foreach (Paper p in _publications)
        {
            if (p.PublicationDate >= limit)
            {
                yield return p;
            }
        }
    }

    public int Compare(ResearchTeam? x, ResearchTeam? y)
    {
        if (x is null && y is null) return 0;
        if (x is null) return -1;
        if (y is null) return 1;
        
        return string.Compare(x.ResearchTopic, y.ResearchTopic, StringComparison.OrdinalIgnoreCase);
    }
}