namespace SystemModelling.ModelWIthItems.RandomValuesProviders;

public class Erlang : IRandomValueProvider
{
    private readonly double _timeMean;
    private readonly int _k;
    public Erlang(double timeMean, int k)
    {
        _timeMean = timeMean;
        _k = k;
    }

    public double GetRandomValue()
    {
        MathNet.Numerics.Distributions.Erlang erlangDist = new (_k, _timeMean / _k);
        return erlangDist.Sample();
    }   
}