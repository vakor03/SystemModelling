namespace SystemModelling.ModelWIthItems.DelayGenerators;

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