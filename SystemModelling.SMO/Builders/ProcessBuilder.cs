using SystemModelling.SMO.Elements;

namespace SystemModelling.SMO.Builders;

public class ProcessBuilder : ElementBuilder
{
    protected int MaxQueue = Int32.MaxValue;
    protected double Delay;
    public static ProcessBuilder New() => new();

    public ProcessBuilder WithDelay(double delay)
    {
        Delay = delay;
        return this;
    }

    public ProcessBuilder WithMaxQueue(int maxQueue)
    {
        MaxQueue = maxQueue;
        return this;
    }

    public Process Build()
    {
        Process process = new Process(Delay, MaxQueue);
        return AddProperties(process);
    }
}