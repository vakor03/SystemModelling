namespace SystemModelling.ModelWIthItems.DelayGenerators;

public class ExponentialDelayGenerator : IDelayGenerator
{
    private readonly double _timeMean;
    public ExponentialDelayGenerator(double timeMean)
    {
        _timeMean = timeMean;
    }

    public double GetDelay()
    {
        double a = 0;
        while (a == 0) {
            a = Random.Shared.NextDouble();
        }
        a = -_timeMean * Math.Log(a);
        return a;

    }
}