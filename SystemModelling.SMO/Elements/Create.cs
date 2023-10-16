using SystemModelling.SMO.Builders;

namespace SystemModelling.SMO.Elements;

public class Create : Element
{
    public override void OutAct()
    {
        base.OutAct();
        TNext = TCurrent + GetDelay();
        Transition?.Next?.InAct();
    }

    public new static FluentCreateBuilder New() => new();
}