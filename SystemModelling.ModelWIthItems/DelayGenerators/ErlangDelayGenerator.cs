using MathNet.Numerics.Distributions;

namespace SystemModelling.ModelWIthItems.DelayGenerators;

public class ErlangDelayGenerator : IDelayGenerator
{
    private readonly double _timeMean;
    private readonly int _k;
    public ErlangDelayGenerator(double timeMean, int k)
    {
        _timeMean = timeMean;
        _k = k;
    }

    public double GetDelay()
    {
        Erlang erlangDist = new (_k, _timeMean / _k);
        return erlangDist.Sample();
    }   
}