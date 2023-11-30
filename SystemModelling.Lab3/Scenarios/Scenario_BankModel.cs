using SystemModelling.SMO;
using SystemModelling.SMO.Elements;
using SystemModelling.SMO.Loggers;
using SystemModelling.SMO.RandomValuesProviders;

namespace SystemModelling.Lab3.Scenarios;

public class Scenario_BankModel : Scenario
{
    public int QueueChangeCount { get; protected set; }

    protected void TryChangeQueue(ILogger logger, Process process1, Process process2)
    {
        if (Math.Abs(process1.Queue - process2.Queue) < 2)
        {
            return;
        }

        int process1Queue = process1.Queue;
        int process2Queue = process2.Queue;

        if (process1.Queue < process2.Queue)
        {
            process1.Queue++;
            process2.Queue--;
        }
        else
        {
            process1.Queue--;
            process2.Queue++;
        }

        QueueChangeCount++;

        logger.WriteLine(
            $"Queue changed: {process1.Name} {process1Queue} -> {process1.Queue}, {process2.Name} {process2Queue} -> {process2.Queue}");
    }

    public override void Run(double time)
    {
        Create create = new Create()
        {
            Name = "Creator",
            DelayGenerator = new Exponential(0.5).ToGenerator(),
        };

        Process process1 = new Process(1)
        {
            Name = "Processor",
            DelayGenerator = new Normal(1.0, 0.3).ToGenerator(),
            MaxQueue = 3,
        };

        Process process2 = new Process(1)
        {
            Name = "Processor2",
            DelayGenerator = new Normal(1.0, 0.3).ToGenerator(),
            MaxQueue = 3,
        };
        process1.AddStatistics(new StandardProcessStatistics());
        process2.AddStatistics(new StandardProcessStatistics());

        create.Transition = new CustomTransition(process1, process2);

        // прибуття першого клієнта заплановано на момент часу 0,1 од. часу
        create.TNext = 0.1;

        // у кожній черзі очікують по два автомобіля
        AddItemsToProcess(process1, 3);
        AddItemsToProcess(process2, 3);

        Model model = new Model(create, process1, process2)
        {
            Logger = new FileLogger("lab3_2.txt")
        };

        process1.OnQueueChange += () => TryChangeQueue(model.Logger, process1, process2);
        process2.OnQueueChange += () => TryChangeQueue(model.Logger, process1, process2);

        model.Simulate(time);
    }

    private void AddItemsToProcess(Process process, int count)
    {
        for (int i = 0; i < count; i++)
        {
            process.InAct();
        }
    }
}

internal class CustomModelStatistics : IModelStatistics
{
    private double _meanClientsInBank;
    private double _meanTimeClientInBank;
    private double _clientsFailProbability;

    private Create _create;
    private Dispose _dispose;
    private readonly StandardProcessStatistics _process1;
    private readonly StandardProcessStatistics _process2;

    public CustomModelStatistics(Create create, Dispose dispose, StandardProcessStatistics process1, StandardProcessStatistics process2)
    {
        _create = create;
        _dispose = dispose;
        _process1 = process1;
        _process2 = process2;
    }

    public void Init(IModel model)
    {
    }

    public void DoStatistics(double delta)
    {
        _meanClientsInBank += delta * (_create.OutQuantity - _dispose.InQuantity - _process1.Failure - _process2.Failure);
    }

    public void PrintResult(ILogger logger)
    {
        double totalTime = _dispose.TCurrent;
        _meanClientsInBank /= totalTime;
        _clientsFailProbability = (double)(_process1.Failure + _process2.Failure) / _create.OutQuantity;
        _meanTimeClientInBank = (_process1.MeanLoad + _process1.MeanQueue + _process2.MeanLoad + _process2.MeanQueue) * totalTime / _create.OutQuantity;
    }
}