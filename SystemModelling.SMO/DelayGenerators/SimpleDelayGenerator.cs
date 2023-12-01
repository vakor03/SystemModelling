using SystemModelling.SMO.RandomValuesProviders;

namespace SystemModelling.SMO.DelayGenerators;

public class SimpleDelayGenerator<T> : IDelayGenerator<T> where T : IItem
{
    private readonly IRandomValueProvider _randomValueProvider;

    public SimpleDelayGenerator(IRandomValueProvider randomValueProvider)
    {
        _randomValueProvider = randomValueProvider;
    }

    public double GetDelay(T item)
    {
        return _randomValueProvider.GetRandomValue();
    }
}

public class SimpleDelayGenerator : IDelayGenerator
{
    private readonly IRandomValueProvider _randomValueProvider;

    public SimpleDelayGenerator(IRandomValueProvider randomValueProvider)
    {
        _randomValueProvider = randomValueProvider;
    }

    public double GetDelay()
    {
        return _randomValueProvider.GetRandomValue();
    }
}