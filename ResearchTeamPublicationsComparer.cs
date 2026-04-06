using System.Collections.Generic;

public class ResearchTeamPublicationsComparer : IComparer<ResearchTeam>
{
    public int Compare(ResearchTeam? x, ResearchTeam? y)
    {
        if (x is null && y is null) return 0;
        if (x is null) return -1;
        if (y is null) return 1;
        
        return x.Publications.Count.CompareTo(y.Publications.Count);
    }
}