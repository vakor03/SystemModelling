using SystemModelling.SMO.DelayGenerators;
using SystemModelling.SMO.Elements;
using SystemModelling.SMO.Enums;

namespace SystemModelling.SMO.Builders;

public abstract class FluentElementBuilder : FluentElementBuilder<FluentElementBuilder>
{
    // public static FluentElementBuilder New() => new();
}

public abstract class FluentElementBuilder<T> where T : FluentElementBuilder<T>
{
    protected string Name;

    protected IDelayGenerator DelayGenerator { get; set; }

    public T WithName(string name)
    {
        Name = name;
        return (T)this;
    }

    public T WithDelayGenerator(IDelayGenerator delayGenerator)
    {
        DelayGenerator = delayGenerator;
        return (T)this;
    }

    public abstract Element Build();
}