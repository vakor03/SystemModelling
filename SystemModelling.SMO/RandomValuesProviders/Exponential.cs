using SystemModelling.ModelWIthItems.DelayGenerators;

namespace SystemModelling.SMO.RandomValuesProviders;

public class Exponential : IRandomValueProvider
{
    private readonly double _timeMean;
    public Exponential(double timeMean)
    {
        _timeMean = timeMean;
    }

    public double GetRandomValue()
    {
        double a = 0;
        while (a == 0) {
            a = Random.Shared.NextDouble();
        }
        a = -_timeMean * Math.Log(a);
        return a;

    }
}