namespace SystemModelling.SMO.Elements;

public class Subprocess
{
    public bool IsBusy { get; set; }
    public double TNext { get; set; }
    public string Name { get; set; }

    public double MeanLoad { get; private set; }

    public void DoStatistics(double delta)
    {
        MeanLoad += (IsBusy ? 1 : 0) * delta;
    }
}

public interface IHaveInputOutput
{
    void InAct();
    void OutAct();
}