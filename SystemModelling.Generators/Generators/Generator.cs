namespace SystemModelling.Generators.Generators;

public abstract class Generator
{
    public abstract double Next();

    public double[] GenerateMany(int count)
    {
        return Enumerable.Range(0, count).Select(_ => Next()).ToArray();
    }

    public abstract Func<double, double, double> PiFunc { get; }
}