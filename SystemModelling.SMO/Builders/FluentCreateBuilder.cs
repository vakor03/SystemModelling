using SystemModelling.SMO.Elements;

namespace SystemModelling.SMO.Builders;

public class FluentCreateBuilder : FluentElementBuilder<FluentCreateBuilder>
{
    public static FluentCreateBuilder New() => new();
    
    public FluentCreateBuilder WithStartedDelay(double startedDelay)
    {
        StartedDelay = startedDelay;
        return this;
    }

    public double StartedDelay { get; set; }

    public override Create Build()
    {
        Create create = new Create
        {
            Name = Name,
            DelayMean = DelayMean,
            DelayDeviation = DelayDeviation,
            Distribution = Distribution,
            Id = Element.NextId++,
            TNext = StartedDelay
        };
        
        return create;
    }
}