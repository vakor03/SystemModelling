using SystemModelling.SMO.Loggers;

namespace SystemModelling.SMO.Elements;

public interface IProcessStatistics
{
    void Init(IProcess process);
    void DoStatistics(double delta);
    void PrintResult(ILogger logger);
}

public interface IModelStatistics
{
    void Init(IModel model);
    void DoStatistics(double delta);
    void PrintResult(ILogger logger);
}

public interface IModel
{
}