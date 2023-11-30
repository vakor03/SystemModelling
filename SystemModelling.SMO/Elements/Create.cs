using SystemModelling.SMO.Loggers;

namespace SystemModelling.SMO.Elements;

public class Create : Element
{
    public override void OutAct()
    {
        base.OutAct();
        TNext = TCurrent + DelayGenerator.GetDelay();
        
        PerformTransitionToNext();
    }

    public override void PrintResult(ILogger logger)
    {
        logger.WriteLine($"Create {Name}\n\tItems created: {Quantity}");
    }

    public override void Reset()
    {
        base.Reset();
        TNext = 0;
    }
}