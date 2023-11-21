using SystemModelling.SMO.Builders;

namespace SystemModelling.SMO.Elements;

public class Create : Element
{
    public override void OutAct()
    {
        base.OutAct();
        TNext = TCurrent + GetDelay();
        
        PerformTransitionToNext();
    }

    public override void Reset()
    {
        base.Reset();
        TNext = 0;
    }

    public new static FluentCreateBuilder New() => new();
}