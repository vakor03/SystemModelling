using SystemModelling.SMO.Elements;

namespace SystemModelling.SMO.Builders;

public class FluentCreateBuilder : FluentElementBuilder<FluentCreateBuilder>
{
    public static FluentCreateBuilder New() => new();

    public override Create Build()
    {
        Create create = new Create
        {
            Name = Name,
            DelayMean = DelayMean,
            DelayDeviation = DelayDeviation,
            Distribution = Distribution
        };
        return create;
    }
}