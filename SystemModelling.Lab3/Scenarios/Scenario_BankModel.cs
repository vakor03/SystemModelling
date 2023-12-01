using SystemModelling.SMO;
using SystemModelling.SMO.Elements;
using SystemModelling.SMO.Loggers;
using SystemModelling.SMO.RandomValuesProviders;
using SystemModelling.SMO.Transitions;

namespace SystemModelling.Lab3.Scenarios;

public class Scenario_BankModel : Scenario
{
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
        Dispose dispose = new Dispose();

        var process1Statistics = new StandardProcessStatistics();
        process1.AddStatistics(process1Statistics);
        process1.AddStatistics(new TotalTimeLeavedStatistics());

        var process2Statistics = new StandardProcessStatistics();
        process2.AddStatistics(process2Statistics);
        process2.AddStatistics(new TotalTimeLeavedStatistics());

        create.Transition = new CustomTransition(process1, process2);
        process1.Transition = new SingleTransition(dispose);
        process2.Transition = new SingleTransition(dispose);

        // прибуття першого клієнта заплановано на момент часу 0,1 од. часу
        create.TNext = 0.1;

        // у кожній черзі очікують по два автомобіля
        AddItemsToProcess(process1, 3);
        AddItemsToProcess(process2, 3);

        Model model = new Model(create, process1, process2, dispose)
        {
            Logger = new FileLogger("lab3_2.txt")
        };

        // логіка зміни черги
        BankChangeQueueBehaviour bankChangeQueueBehaviour = new BankChangeQueueBehaviour();
        
        process1.OnQueueChange += () => bankChangeQueueBehaviour.TryChangeQueue(model.Logger, process1, process2);
        process2.OnQueueChange += () => bankChangeQueueBehaviour.TryChangeQueue(model.Logger, process1, process2);

        model.Statistics = new BankModelStatistics(create, dispose, process1Statistics, process2Statistics,
            bankChangeQueueBehaviour);

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