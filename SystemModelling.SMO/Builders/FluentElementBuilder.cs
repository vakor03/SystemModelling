using SystemModelling.SMO.Elements;
using SystemModelling.SMO.Enums;
using SystemModelling.SMO.TransitionOptions;

namespace SystemModelling.SMO.Builders;

public class FluentElementBuilder : FluentElementBuilder<FluentElementBuilder>
{
    public static FluentElementBuilder New() => new();
}

public class FluentElementBuilder<T> where T : FluentElementBuilder<T>
{
    protected string Name;

    protected double DelayMean;

    protected double DelayDeviation;

    protected DistributionType Distribution;

    public T WithName(string name)
    {
        Name = name;
        return (T)this;
    }

    public T WithDelayMean(double delayMean)
    {
        DelayMean = delayMean;
        return (T)this;
    }

    public T WithDelayDeviation(double delayDeviation)
    {
        DelayDeviation = delayDeviation;
        return (T)this;
    }

    public T WithDistribution(DistributionType distribution)
    {
        Distribution = distribution;
        return (T)this;
    }

    public virtual Element Build()
    {
        Element element = new Element
        {
            Name = Name,
            DelayMean = DelayMean,
            DelayDeviation = DelayDeviation,
            Distribution = Distribution
        };
        return element;
    }
}