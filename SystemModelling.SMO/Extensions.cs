using SystemModelling.SMO.DelayGenerators;
using SystemModelling.SMO.RandomValuesProviders;

namespace SystemModelling.SMO;

public static class Extensions
{
    public static IDelayGenerator ToGenerator(this IRandomValueProvider randomValueProvider)
        => new SimpleDelayGenerator(randomValueProvider);
}