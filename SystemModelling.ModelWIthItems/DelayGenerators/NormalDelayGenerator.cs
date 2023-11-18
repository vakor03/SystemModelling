using MathNet.Numerics.Distributions;

namespace SystemModelling.ModelWIthItems.DelayGenerators;

public class NormalDelayGenerator : IDelayGenerator
{
    private readonly double _timeMean;
    private readonly double _timeDeviation;
    public NormalDelayGenerator(double timeMean, double timeDeviation)
    {
        _timeMean = timeMean;
        _timeDeviation = timeDeviation;
    }

    public double GetDelay()
    {
        Normal normalDist = new ();
        return _timeMean + _timeDeviation * normalDist.Sample();
    }   
}