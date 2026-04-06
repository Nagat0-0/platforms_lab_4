using System;

public interface INameAndCopy
{
    string Name { get; set; }
    object DeepCopy();
}