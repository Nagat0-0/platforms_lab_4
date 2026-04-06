using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

public class ResearchTeamCollection
{
    private ImmutableList<ResearchTeam> _researchTeams = ImmutableList<ResearchTeam>.Empty;

    public void AddDefaults()
    {
        ResearchTeam team1 = new ResearchTeam("Легенди футболу", "FIFA", 777, TimeFrame.Long);
        ResearchTeam team2 = new ResearchTeam("Нове покоління", "UEFA", 10, TimeFrame.TwoYears);
        ResearchTeam team3 = new ResearchTeam("Українці в АПЛ", "FA", 202, TimeFrame.Year);

        Person p1 = new Person("Андрій", "Шевченко", new DateTime(1976, 9, 29));
        Person p2 = new Person("Кріштіану", "Роналду", new DateTime(1985, 2, 5));
        Person p3 = new Person("Кіліан", "Мбаппе", new DateTime(1998, 12, 20));
        Person p4 = new Person("Олександр", "Зінченко", new DateTime(1996, 12, 15));
        Person p5 = new Person("Михайло", "Мудрик", new DateTime(2001, 1, 5));

        team1.AddParticipants(p1, p2);
        team2.AddParticipants(p3);
        team3.AddParticipants(p4, p5, new Person("Ілля", "Забарний", new DateTime(2002, 9, 1)));

        team1.AddPapers(
            new Paper("Бомбардирські рекорди Ліги Чемпіонів", p2, new DateTime(2023, 5, 10)),
            new Paper("Еволюція тактики 2000-х", p1, new DateTime(2021, 8, 24))
        );
        
        team2.AddPapers(
            new Paper("Швидкість як головна зброя сучасного футболу", p3, new DateTime(2024, 1, 15))
        );

        AddResearchTeams(team1, team2, team3);
    }

    public void AddResearchTeams(params ResearchTeam[] teams)
    {
        if (teams != null)
        {
            _researchTeams = _researchTeams.AddRange(teams);
        }
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        foreach (ResearchTeam team in _researchTeams)
        {
            sb.AppendLine(team.ToString());
        }
        return sb.ToString();
    }

    public virtual string ToShortList()
    {
        StringBuilder sb = new StringBuilder();
        foreach (ResearchTeam team in _researchTeams)
        {
            sb.AppendLine($"Дослідження: '{team.ResearchTopic}' ({team.Organization}), Реєстр. №: {team.RegistrationNumber}, Тривалість: {team.Duration}, Учасників: {team.Participants.Count}, Публікацій: {team.Publications.Count}");
        }
        return sb.ToString();
    }

    public void SortByRegistrationNumber()
    {
        _researchTeams = _researchTeams.Sort();
    }

    public void SortByResearchTopic()
    {
        _researchTeams = _researchTeams.Sort(new ResearchTeam());
    }

    public void SortByPublicationsCount()
    {
        _researchTeams = _researchTeams.Sort(new ResearchTeamPublicationsComparer());
    }

    public int MinRegistrationNumber
    {
        get
        {
            if (_researchTeams.Count == 0) return 0;
            return _researchTeams.Min(t => t.RegistrationNumber);
        }
    }

    public IEnumerable<ResearchTeam> TwoYearsDuration
    {
        get
        {
            return _researchTeams.Where(t => t.Duration == TimeFrame.TwoYears);
        }
    }

    public List<ResearchTeam> NGroup(int value)
    {
        var grouped = _researchTeams.GroupBy(t => t.Participants.Count);
        foreach (var group in grouped)
        {
            if (group.Key == value)
            {
                return group.ToList();
            }
        }
        return new List<ResearchTeam>();
    }
}