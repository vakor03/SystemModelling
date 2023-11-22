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

    public FluentProcessBuilder WithStartedCount(int startedCount)
    {
        StartedCount = startedCount;
        return this;
    }

    public int StartedCount { get; set; }


    public override Process Build()
    {
        Process process = new Process(Quantity)
        {
            Name = Name,
            DelayGenerator = DelayGenerator,
            TNext = Double.MaxValue,
            MaxQueue = MaxQueue,
            Id = NextId++
        };
        
        process.AddStatistics(new StandardProcessStatistics());

        for (int i = 0; i < StartedCount; i++)
        {
            process.InAct();
        }
        return process;
    }
}