namespace SystemModelling.SMO.Elements;

public interface IProcess : IElement
{
    int Queue { get; }
    IEnumerable<Subprocess> Subprocesses { get; }
    int BusySubprocessesCount { get; }
    int Failure { get; }
    event Action OnOutAct;
    event Action OnQueueChange;
}