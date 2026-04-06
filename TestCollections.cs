using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;

public class TestCollections
{
    private List<Team> _teams = new List<Team>();
    private List<string> _strings = new List<string>();
    private Dictionary<Team, ResearchTeam> _teamDict = new Dictionary<Team, ResearchTeam>();
    private Dictionary<string, ResearchTeam> _stringDict = new Dictionary<string, ResearchTeam>();

    private ImmutableList<Team> _immTeams;
    private ImmutableList<string> _immStrings;
    private ImmutableDictionary<Team, ResearchTeam> _immTeamDict;
    private ImmutableDictionary<string, ResearchTeam> _immStringDict;

    private SortedList<Team, ResearchTeam> _sortedListTeam = new SortedList<Team, ResearchTeam>();
    private SortedList<string, ResearchTeam> _sortedListString = new SortedList<string, ResearchTeam>();
    private SortedDictionary<Team, ResearchTeam> _sortedDictTeam = new SortedDictionary<Team, ResearchTeam>();
    private SortedDictionary<string, ResearchTeam> _sortedDictString = new SortedDictionary<string, ResearchTeam>();

    public static ResearchTeam GenerateResearchTeam(int value)
    {
        int regNumber = value > 0 ? value : 1;
        return new ResearchTeam($"Тема_{value}", $"Організація_{value}", regNumber, TimeFrame.Year);
    }

    public TestCollections(int count)
    {
        for (int i = 1; i <= count; i++)
        {
            ResearchTeam rt = GenerateResearchTeam(i);
            Team t = rt.TeamBase;
            
            _teams.Add(t);
            _strings.Add(t.ToString());
            _teamDict.Add(t, rt);
            _stringDict.Add(t.ToString(), rt);

            _sortedListTeam.Add(t, rt);
            _sortedListString.Add(t.ToString(), rt);
            _sortedDictTeam.Add(t, rt);
            _sortedDictString.Add(t.ToString(), rt);
        }

        _immTeams = _teams.ToImmutableList();
        _immStrings = _strings.ToImmutableList();
        _immTeamDict = _teamDict.ToImmutableDictionary();
        _immStringDict = _stringDict.ToImmutableDictionary();
    }

    public void MeasureSearchTimes()
    {
        if (_teams.Count == 0) return;

        ResearchTeam firstRt = GenerateResearchTeam(1);
        ResearchTeam middleRt = GenerateResearchTeam(_teams.Count / 2 + 1);
        ResearchTeam lastRt = GenerateResearchTeam(_teams.Count);
        ResearchTeam nonExistentRt = GenerateResearchTeam(_teams.Count + 1);

        _teams.Contains(firstRt.TeamBase);
        _strings.Contains(firstRt.TeamBase.ToString());
        _teamDict.ContainsKey(firstRt.TeamBase);
        _stringDict.ContainsKey(firstRt.TeamBase.ToString());
        _teamDict.ContainsValue(firstRt);

        _immTeams.Contains(firstRt.TeamBase);
        _immTeamDict.ContainsKey(firstRt.TeamBase);
        _sortedListTeam.ContainsKey(firstRt.TeamBase);
        _sortedDictTeam.ContainsKey(firstRt.TeamBase);

        MeasureElement("Перший елемент", firstRt.TeamBase, firstRt);
        MeasureElement("Центральний елемент", middleRt.TeamBase, middleRt);
        MeasureElement("Останній елемент", lastRt.TeamBase, lastRt);
        MeasureElement("Неіснуючий елемент", nonExistentRt.TeamBase, nonExistentRt);
    }

    private void MeasureElement(string elementName, Team key, ResearchTeam value)
    {
        Stopwatch sw = new Stopwatch();
        string keyString = key.ToString();

        sw.Restart();
        _teams.Contains(key);
        sw.Stop();
        long timeListTeam = sw.ElapsedTicks;

        sw.Restart();
        _strings.Contains(keyString);
        sw.Stop();
        long timeListString = sw.ElapsedTicks;

        sw.Restart();
        _teamDict.ContainsKey(key);
        sw.Stop();
        long timeDictKey = sw.ElapsedTicks;

        sw.Restart();
        _stringDict.ContainsKey(keyString);
        sw.Stop();
        long timeDictString = sw.ElapsedTicks;

        sw.Restart();
        _teamDict.ContainsValue(value);
        sw.Stop();
        long timeDictValue = sw.ElapsedTicks;



        sw.Restart();
        _immTeams.Contains(key);
        sw.Stop();
        long timeImmListTeam = sw.ElapsedTicks;

        sw.Restart();
        _immStrings.Contains(keyString);
        sw.Stop();
        long timeImmListString = sw.ElapsedTicks;

        sw.Restart();
        _immTeamDict.ContainsKey(key);
        sw.Stop();
        long timeImmDictKey = sw.ElapsedTicks;

        sw.Restart();
        _immStringDict.ContainsKey(keyString);
        sw.Stop();
        long timeImmDictString = sw.ElapsedTicks;

        sw.Restart();
        _immTeamDict.ContainsValue(value);
        sw.Stop();
        long timeImmDictValue = sw.ElapsedTicks;


        sw.Restart();
        _sortedListTeam.ContainsKey(key);
        sw.Stop();
        long timeSortedListKey = sw.ElapsedTicks;

        sw.Restart();
        _sortedListString.ContainsKey(keyString);
        sw.Stop();
        long timeSortedListStr = sw.ElapsedTicks;

        sw.Restart();
        _sortedListTeam.ContainsValue(value);
        sw.Stop();
        long timeSortedListVal = sw.ElapsedTicks;

        sw.Restart();
        _sortedDictTeam.ContainsKey(key);
        sw.Stop();
        long timeSortedDictKey = sw.ElapsedTicks;

        sw.Restart();
        _sortedDictString.ContainsKey(keyString);
        sw.Stop();
        long timeSortedDictStr = sw.ElapsedTicks;

        sw.Restart();
        _sortedDictTeam.ContainsValue(value);
        sw.Stop();
        long timeSortedDictVal = sw.ElapsedTicks;

        Console.WriteLine($"*** {elementName} ***");
        Console.WriteLine($"[Standard] List<Team> Contains:                     {timeListTeam} ticks");
        Console.WriteLine($"[Standard] List<string> Contains:                   {timeListString} ticks");
        Console.WriteLine($"[Standard] Dictionary<Team> ContainsKey:            {timeDictKey} ticks");
        Console.WriteLine($"[Standard] Dictionary<string> ContainsKey:          {timeDictString} ticks");
        Console.WriteLine($"[Standard] Dictionary<Team> ContainsValue:          {timeDictValue} ticks");
        
        Console.WriteLine($"[Immutable] ImmutableList<Team> Contains:           {timeImmListTeam} ticks");
        Console.WriteLine($"[Immutable] ImmutableList<string> Contains:         {timeImmListString} ticks");
        Console.WriteLine($"[Immutable] ImmutableDictionary<Team> ContainsKey:  {timeImmDictKey} ticks");
        Console.WriteLine($"[Immutable] ImmutableDictionary<string> ContainsKey:{timeImmDictString} ticks");
        Console.WriteLine($"[Immutable] ImmutableDictionary<Team> ContainsVal:  {timeImmDictValue} ticks");

        Console.WriteLine($"[Sorted] SortedList<Team> ContainsKey:              {timeSortedListKey} ticks");
        Console.WriteLine($"[Sorted] SortedList<string> ContainsKey:            {timeSortedListStr} ticks");
        Console.WriteLine($"[Sorted] SortedList<Team> ContainsValue:            {timeSortedListVal} ticks");
        Console.WriteLine($"[Sorted] SortedDictionary<Team> ContainsKey:        {timeSortedDictKey} ticks");
        Console.WriteLine($"[Sorted] SortedDictionary<string> ContainsKey:      {timeSortedDictStr} ticks");
        Console.WriteLine($"[Sorted] SortedDictionary<Team> ContainsValue:      {timeSortedDictVal} ticks");

        Console.WriteLine();
    }
}