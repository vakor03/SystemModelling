using SystemModelling.SMO.Loggers;

namespace SystemModelling.SMO;

public interface IElement
{
    double TNext { get; }
    double TCurrent { get; set; }
    void OutAct();
    void DoStatistics(double deltaTime);
    void PrintInfo(ILogger logger);
    void PrintResult(ILogger logger);
    void Reset();
    int InQuantity { get; set; }
    int OutQuantity { get; set; }
}