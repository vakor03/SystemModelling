using SystemModelling.SMO.DelayGenerators;
using SystemModelling.SMO.Loggers;
using SystemModelling.SMO.Transitions;

namespace SystemModelling.SMO.Elements;

public abstract class Element : IElement
{
    public string Name { get; set; }
    public int Id { get; init; }
    
    public int Quantity { get; set; }
    public ITransition? Transition { get; set; }
    public IDelayGenerator DelayGenerator { get; set; }
    public double TCurrent { get; set; }
    public double TNext { get; set; }

    public virtual void PrintInfo(ILogger logger)
    {
        logger.WriteLine($"{Name} quantity={Quantity} tNext={TNext}");
    }

    public abstract void PrintResult(ILogger logger);

    public virtual void OutAct()
    {
        Quantity++;
    }

    protected void PerformTransitionToNext()
    {
        Element? transitionElement = Transition?.Next;
        
        transitionElement?.InAct();
    }

    public virtual void InAct()
    {
    }

    public virtual void DoStatistics(double delta)
    {
    }

    public virtual void PrintInfo()
    {
        // no-op
    }

    // protected enum State
    // {
    //     Free = 0,
    //     Busy = 1,
    // }

    public virtual void Reset()
    {
        TNext = double.MaxValue;
    }
}