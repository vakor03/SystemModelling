namespace SystemModelling.SMO;

public interface IElement
{
    double TNext { get; }
    double TCurrent { get; set; }
    void OutAct();
    void DoStatistics(double deltaTime);
    void PrintInfo();
    void Reset();
}