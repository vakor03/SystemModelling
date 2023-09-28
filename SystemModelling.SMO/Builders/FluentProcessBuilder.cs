using SystemModelling.SMO.Elements;

namespace SystemModelling.SMO.Builders;

public class FluentProcessBuilder : FluentElementBuilder<FluentProcessBuilder>
{
    protected int MaxQueue = Int32.MaxValue;
    public static FluentProcessBuilder New() => new();

    public FluentProcessBuilder WithMaxQueue(int maxQueue)
    {
        MaxQueue = maxQueue;
        return this;
    }

    public override Process Build()
    {
        Process process = new Process(DelayMean, MaxQueue)
        {
            Name = Name,
            DelayMean = DelayMean,
            DelayDeviation = DelayDeviation,
            Distribution = Distribution
        };
        return process;
    }
}