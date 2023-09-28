using SystemModelling.SMO.Elements;
using SystemModelling.SMO.Enums;

namespace SystemModelling.SMO.Builders;

public class ElementBuilder
{
    protected static ElementBuilder New() => new();

    protected string Name;

    protected double DelayMean;

    protected double DelayDeviation;

    protected DistributionType Distribution;

    protected int Id;

    public ElementBuilder WithName(string name) 
    {
        Name = name;
        return this;
    }

    public ElementBuilder WithDelayMean(double delayMean)
    {
        DelayMean = delayMean;
        return this;
    }

    public ElementBuilder WithDelayDeviation(double delayDeviation)
    {
        DelayDeviation = delayDeviation;
        return this;
    }

    public ElementBuilder WithDistribution(DistributionType distribution)
    {
        Distribution = distribution;
        return this;
    }

    public ElementBuilder WithId(int id)
    {
        Id = id;
        return this;
    }

    public Element Build()
    {
        Element element = new Element();
        
        return AddProperties(element);
    }

    protected T AddProperties<T>(T element) where T : Element
    {
        element.Name = Name;
        element.DelayMean = DelayMean;
        element.DelayDeviation = DelayDeviation;
        element.Distribution = Distribution;
        element.Id = Id;

        return element;
    }
}