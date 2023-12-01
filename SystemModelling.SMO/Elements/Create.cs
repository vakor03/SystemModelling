using SystemModelling.SMO.Loggers;

namespace SystemModelling.SMO.Elements;

public class Create : Element
{
    public override void OutAct()
    {
        OutQuantity++;
        TNext = TCurrent + DelayGenerator.GetDelay();
        
        PerformTransitionToNext();
    }

    public override void PrintResult(ILogger logger)
    {
        logger.WriteLine($"\n{Name}\n\tItems created: {OutQuantity}");
    }

    public override void Reset()
    {
        base.Reset();
        TNext = 0;
    }
}