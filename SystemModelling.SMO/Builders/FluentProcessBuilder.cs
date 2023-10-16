using SystemModelling.SMO.Elements;

namespace SystemModelling.SMO.Builders;

public class FluentProcessBuilder : FluentElementBuilder<FluentProcessBuilder>
{
    protected int MaxQueue = 0;
    protected int Quantity = 1;
    public static FluentProcessBuilder New() => new();

    public FluentProcessBuilder WithMaxQueue(int maxQueue)
    {
        MaxQueue = maxQueue;
        return this;
    }
    
    public FluentProcessBuilder WithProcessesCount(int count)
    {
        Quantity = count;
        return this;
    }

    public override Process Build()
    {
        Process process = new Process(Quantity)
        {
            Name = Name,
            DelayMean = DelayMean,
            DelayDeviation = DelayDeviation,
            Distribution = Distribution,
            TNext = Double.MaxValue,
            MaxQueue = MaxQueue,
        };
        return process;
    }
}