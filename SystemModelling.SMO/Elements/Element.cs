﻿using SystemModelling.SMO.Builders;
using SystemModelling.SMO.Enums;
using SystemModelling.SMO.Loggers;
using SystemModelling.SMO.Transitions;

namespace SystemModelling.SMO.Elements;

public class Element
{
    public string Name { get; set; }
    public double DelayMean { get; set; }
    public double DelayDeviation { get; set; }
    public DistributionType Distribution { get; set; }
    public int Quantity { get; set; }
    public State CurrentState { get; set; }
    public ITransition? Transition { get; set; }

    public int Id { get; private set; }
    public static int NextId { get; set; } = 0;
    public double TCurrent { get; set; }
    public double TNext { get; set; }

    public Element()
    {
        TNext = Double.MaxValue;
        DelayMean = 1.0;
        Distribution = DistributionType.Exp;
        TCurrent = TNext;
        CurrentState = State.Free;
        Transition = null;
        Id = NextId;
        NextId++;
        Name = "element" + Id;
    }

    public Element(double delay)
    {
        Name = "anonymus";
        TNext = 0.0;
        DelayMean = delay;
        Distribution = DistributionType.Constant;
        TCurrent = TNext;
        CurrentState = State.Free;
        Transition = null;
        Id = NextId;
        NextId++;
        Name = "element" + Id;
    }

    public Element(String nameOfElement, double delay)
    {
        Name = nameOfElement;
        TNext = 0.0;
        DelayMean = delay;
        Distribution = DistributionType.Exp;
        TCurrent = TNext;
        CurrentState = State.Free;
        Transition = null;
        Id = NextId;
        NextId++;
        Name = "element" + Id;
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
        logger.WriteLine($"{Name} state={CurrentState} quantity={Quantity} tNext={TNext}");
    }

    public virtual void PrintResult(ILogger logger)
    {
        logger.WriteLine($"{Name} quantity={Quantity}");
    }

    public virtual void OutAct()
    {
        Quantity++;
    }

    public virtual void InAct()
    {
    }

    public virtual void DoStatistics(double delta)
    {
    }

    public enum State
    {
        Free = 0,
        Busy = 1
    }
    
    public static FluentElementBuilder New() => new();
}