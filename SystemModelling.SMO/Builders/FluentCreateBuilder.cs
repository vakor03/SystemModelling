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
            DelayGenerator = DelayGenerator,
            Id = NextId++,
            TNext = StartedDelay
        };
        
        return create;
    }
}