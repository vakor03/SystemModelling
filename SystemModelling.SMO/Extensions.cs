using SystemModelling.ModelWIthItems.DelayGenerators;
using SystemModelling.SMO.DelayGenerators;

namespace SystemModelling.SMO;

public static class Extensions
{
    public static IDelayGenerator ToGenerator(this IRandomValueProvider randomValueProvider)
        => new SimpleDelayGenerator(randomValueProvider);
}