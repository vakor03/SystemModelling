using SystemModelling.ModelWIthItems.DelayGenerators;
using SystemModelling.Shared;
using SystemModelling.SMO.DelayGenerators;
using SystemModelling.SMO.Enums;
using SystemModelling.SMO.Transitions;

namespace SystemModelling.SMO.Elements;

public abstract class Element : IElement
{
    public string Name { get; set; }
    public int Quantity { get; set; }
    protected State CurrentState { get; set; }
    public ITransition? Transition { get; set; }

    // public double DelayMean { get; set; }
    // public double DelayDeviation { get; set; }
    // public DistributionType Distribution { get; set; }
    public IDelayGenerator DelayGenerator { get; set; }

    #region Id

    public int Id { get; init; }
    public static int NextId { get; set; } = 0;

    #endregion
    
    #region Time

    public double TCurrent { get; set; }
    public double TNext { get; set; }

    #endregion
    
    public ILogger Logger { get; set; }
    public double ClientTimeProcessing { get; set; }

    public Element()
    {
        Id = NextId++;
        Name = $"element {Id}";
    }

    public virtual void PrintInfo(ILogger logger)
    {
        logger.WriteLine($"{Name} quantity={Quantity} tNext={TNext}");
    }

    public virtual void PrintResult(ILogger logger)
    {
        logger.WriteLine($"{Name} quantity={Quantity}");
    }

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

    protected enum State
    {
        Free = 0,
        Busy = 1,
    }

    public virtual void Reset()
    {
        TNext = double.MaxValue;
    }
}