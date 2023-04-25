
using System.Collections.Generic;

public class NameAndPositionStatistics
{
    public string Name { get; set; }
    public List<PositionStatistics> PositionStatistics { get; set; }

    public NameAndPositionStatistics(string name, List<PositionStatistics> positions)
    {
        Name = name;
        PositionStatistics = positions;
    }
}

