namespace SystemModelling.ModelWIthItems.DelayGenerators;

public class UniformDelayGenerator : IDelayGenerator
{
    private readonly double _min;
    private readonly double _max;
    public UniformDelayGenerator(double delayMean, double delayDeviation)   
    {
        _min = delayMean - delayDeviation;
        _max = delayMean + delayDeviation;
    }

    public double GetDelay()
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