using SystemModelling.Shared;

namespace SystemModelling.SMO.Elements;

public interface IProcessStatistics
{
    void Init(IProcess process);
    void DoStatistics(double delta);
    void PrintResult(ILogger logger);
}

public interface IProcess : IElement
{
    int Queue { get; }
    IEnumerable<Subprocess> Subprocesses { get; }
    int BusySubprocessesCount { get; }
    int Quantity { get; }
    int Failure { get; }
    public event Action OnOutAct;
}