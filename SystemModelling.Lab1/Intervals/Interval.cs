namespace SystemModelling.Lab1.Intervals;

public struct Interval
{
    public double Start { get; set; }
    public double End { get; set; }

    public double Length => End - Start;
    public int ElementsCount => Values.Count;

    public List<double> Values { get; set; }

    public Interval(double start, double end)
    {
        Start = start;
        End = end;
        Values = new List<double>();
    }
}