namespace SystemModelling.ModelWIthItems.RandomValuesProviders;

public class Normal : IRandomValueProvider
{
    private readonly double _timeMean;
    private readonly double _timeDeviation;
    public Normal(double timeMean, double timeDeviation)
    {
        _timeMean = timeMean;
        _timeDeviation = timeDeviation;
    }

    public double GetRandomValue()
    {
        MathNet.Numerics.Distributions.Normal normalDist = new ();
        return _timeMean + _timeDeviation * normalDist.Sample();
    }   
}