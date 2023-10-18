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

    public new static FluentCreateBuilder New() => new();
}