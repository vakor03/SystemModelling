namespace SystemModelling.SMO.Elements;

public class Create : Element
{
    public Create(double delay) : base(delay)
    {
        TNext = 0;
    }

    public override void OutAct()
    {
        base.OutAct();
        TNext = TCurrent + GetDelay();
        TransitionOption?.Next?.InAct();
    }
}