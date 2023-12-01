using SystemModelling.SMO.Elements;
using SystemModelling.SMO.Loggers;

namespace SystemModelling.Lab3.Scenarios;

internal class BankModelStatistics : IModelStatistics
{
    private double _meanClientsInBank;
    private double _meanTimeClientInBank;
    private double _clientsFailProbability;

    private Create _create;
    private Dispose _dispose;
    private readonly StandardProcessStatistics _process1Statistics;
    private readonly StandardProcessStatistics _process2Statistics;
    private readonly BankChangeQueueBehaviour _bankChangeQueueBehaviour;

    public BankModelStatistics(Create create, Dispose dispose, StandardProcessStatistics process1Statistics,
        StandardProcessStatistics process2Statistics, BankChangeQueueBehaviour bankChangeQueueBehaviour)
    {
        _create = create;
        _dispose = dispose;
        _process1Statistics = process1Statistics;
        _process2Statistics = process2Statistics;
        _bankChangeQueueBehaviour = bankChangeQueueBehaviour;
    }

    public void Init(IModel model)
    {
    }

    public void DoStatistics(double delta)
    {
        var meanClientsInBank =
            delta * (_create.OutQuantity + 6 - _dispose.InQuantity - _process1Statistics.Failure - _process2Statistics.Failure);
        _meanClientsInBank += meanClientsInBank;
    }

    public void PrintResult(ILogger logger)
    {
        double totalTime = _dispose.TCurrent;
        _meanClientsInBank /= totalTime;
        _clientsFailProbability = (double)(_process1Statistics.Failure + _process2Statistics.Failure) / _create.OutQuantity;
        _meanTimeClientInBank = (_process1Statistics.MeanLoad + _process1Statistics.MeanQueue + _process2Statistics.MeanLoad + _process2Statistics.MeanQueue) *
            totalTime / _create.OutQuantity;
        logger.WriteLine($"Mean clients in bank: {_meanClientsInBank}");
        logger.WriteLine($"Mean time client in bank: {_meanTimeClientInBank}");
        logger.WriteLine($"Clients fail probability: {_clientsFailProbability}");
        logger.WriteLine($"Queue change count: {_bankChangeQueueBehaviour.QueueChangeCount}");
    }
}