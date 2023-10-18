using SystemModelling.Shared;
using SystemModelling.SMO.Builders;
using SystemModelling.SMO.Enums;
using SystemModelling.SMO.Transitions;

namespace SystemModelling.SMO.Elements;

public class Element
{
    public string Name { get; set; }
    public int Quantity { get; set; }
    protected State CurrentState { get; set; }
    public ITransition? Transition { get; set; }

    #region DelayGenerator

    public double DelayMean { get; set; }
    public double DelayDeviation { get; set; }
    public DistributionType Distribution { get; set; }

    #endregion

    #region Id

    public int Id { get; init; }
    public static int NextId { get; set; } = 0;

    #endregion
    
    #region Time

    public double TCurrent { get; set; }
    public double TNext { get; set; }

    #endregion
    
    public ILogger Logger { get; set; }

    public Element()
    {
        Id = NextId++;
        Name = $"element {Id}";
    }

    protected double GetDelay()
    {
        return Distribution switch
        {
            DistributionType.Exp => FunRand.Exp(DelayMean),
            DistributionType.Constant => DelayMean,
            DistributionType.Normal => FunRand.Norm(DelayMean, DelayDeviation),
            DistributionType.Uniform => FunRand.Unif(DelayMean, DelayDeviation),
            _ => throw new ArgumentOutOfRangeException()
        };
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
    
    public virtual void LogTransition(Element? transitionElement)
    {
        Logger.WriteLine($"{Name} -> {transitionElement?.Name ?? "null"}");
    }

    protected void PerformTransitionToNext()
    {
        Element? transitionElement = Transition?.Next;
        
        LogTransition(transitionElement);
        transitionElement?.InAct();
    }

    public virtual void InAct()
    {
    }

    public virtual void DoStatistics(double delta)
    {
    }

    protected enum State
    {
        Free = 0,
        Busy = 1,
    }
    
    public static FluentElementBuilder New() => new();
}