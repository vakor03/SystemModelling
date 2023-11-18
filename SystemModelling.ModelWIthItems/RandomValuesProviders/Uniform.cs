namespace SystemModelling.ModelWIthItems.DelayGenerators;

public class Uniform : IRandomValueProvider
{
    private readonly double _min;
    private readonly double _max;

    public Uniform(double min, double max)
    {
        _min = min;
        _max = max;
    }

    public double GetRandomValue()
    {
        double a = 0;
        while (a == 0)
        {
            a = Random.Shared.NextDouble();
        }

        a = _min + a * (_max - _min);
        return a;
    }
}