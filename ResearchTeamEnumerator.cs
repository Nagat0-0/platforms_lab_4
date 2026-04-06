using System;
using System.Collections;
using System.Collections.Generic;

public class ResearchTeamEnumerator : IEnumerator
{
    private List<Person> _participants;
    private List<Paper> _publications;
    private int _position = -1;
    private List<Person> _participantsWithPublications;

    public ResearchTeamEnumerator(List<Person> participants, List<Paper> publications)
    {
        _participants = participants;
        _publications = publications;
        _participantsWithPublications = new List<Person>();

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
            if (hasPublication)
            {
                _participantsWithPublications.Add(p);
            }
        }
    }

    public object Current
    {
        get
        {
            if (_position < 0 || _position >= _participantsWithPublications.Count)
            {
                throw new InvalidOperationException();
            }
            return _participantsWithPublications[_position];
        }
    }

    public bool MoveNext()
    {
        _position++;
        return (_position < _participantsWithPublications.Count);
    }

    public void Reset()
    {
        _position = -1;
    }
}