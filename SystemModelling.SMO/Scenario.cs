using SystemModelling.Shared;
using SystemModelling.SMO.Elements;
using SystemModelling.SMO.Enums;
using SystemModelling.SMO.Transitions;

namespace SystemModelling.SMO;

public abstract class Scenario
{
    public abstract void Run();
}

public class Scenario_3_1_1 : Scenario
{
    public override void Run()
    {
        Create create = Create.New()
            .WithName("Create")
            .WithDelayMean(0.5)
            .WithDistribution(DistributionType.Exp)
            .Build();

        Process process1 = Process.New()
            .WithName("Process1")
            .WithDelayMean(1)
            .WithDelayDeviation(0.3)
            .WithDistribution(DistributionType.Normal)
            .WithMaxQueue(3)
            .Build();
        
        Process process2 = Process.New()
            .WithName("Process2")
            .WithDelayMean(1)
            .WithDelayDeviation(0.3)
            .WithDistribution(DistributionType.Normal)
            .WithMaxQueue(3)
            .Build();

        create.Transition = new CustomTransition(process1, process2);
        
        Model model = new Model(create, process1, process2);
        
        process1.OnQueueChanged += () => TryChangeQueue(model.Logger, process1, process2);
        process2.OnQueueChanged += () => TryChangeQueue(model.Logger, process1, process2);
        
        model.Simulate(1000.0);
    }
}

public static class Scenarios
{
    public static void RunTask_3_1_1()
    {
        Create create = Create.New()
            .WithName("Create")
            .WithDelayMean(0.5)
            .WithDistribution(DistributionType.Exp)
            .Build();

        Process process1 = Process.New()
            .WithName("Process1")
            .WithDelayMean(1)
            .WithDelayDeviation(0.3)
            .WithDistribution(DistributionType.Normal)
            .WithMaxQueue(3)
            .Build();
        
        Process process2 = Process.New()
            .WithName("Process2")
            .WithDelayMean(1)
            .WithDelayDeviation(0.3)
            .WithDistribution(DistributionType.Normal)
            .WithMaxQueue(3)
            .Build();

        create.Transition = new CustomTransition(process1, process2);
        
        Model model = new Model(create, process1, process2);
        
        process1.OnQueueChanged += () => TryChangeQueue(model.Logger, process1, process2);
        process2.OnQueueChanged += () => TryChangeQueue(model.Logger, process1, process2);
        
        model.Simulate(1000.0);
    }
    public static void RunTask_3_1_2()
    {
        Create create = Create.New()
            .WithName("Create")
            .WithDelayMean(0.5)
            .WithDistribution(DistributionType.Exp)
            .WithStartedDelay(0.1)
            .Build();

        Process process1 = Process.New()
            .WithName("Process1")
            .WithDelayMean(0.3)
            .WithDistribution(DistributionType.Exp)
            .WithMaxQueue(3)
            .Build();
        
        Process process2 = Process.New()
            .WithName("Process2")
            .WithDelayMean(0.3)
            .WithDistribution(DistributionType.Exp)
            .WithMaxQueue(3)
            .Build();

        create.Transition = new CustomTransition(process1, process2);
        
        Model model = new Model(create, process1, process2);
        
        process1.OnQueueChanged += () => TryChangeQueue(model.Logger, process1, process2);
        process2.OnQueueChanged += () => TryChangeQueue(model.Logger, process1, process2);
        
        model.Simulate(1000.0);
    }

    public static void RunTask_3_1_3()
    {
        Create create = Create.New()
            .WithName("Create")
            .WithDelayMean(0.5)
            .WithDistribution(DistributionType.Exp)
            .Build();

        Process process1 = Process.New()
            .WithName("Process1")
            .WithDelayMean(0.3)
            .WithDistribution(DistributionType.Exp)
            .WithMaxQueue(3)
            .WithStartedCount(3)
            .Build();
        
        Process process2 = Process.New()
            .WithName("Process2")
            .WithDelayMean(0.3)
            .WithDistribution(DistributionType.Exp)
            .WithMaxQueue(3)
            .WithStartedCount(3)
            .Build();


        create.Transition = new CustomTransition(process1, process2);
        
        Model model = new Model(create, process1, process2);
        
        process1.OnQueueChanged += () => TryChangeQueue(model.Logger, process1, process2);
        process2.OnQueueChanged += () => TryChangeQueue(model.Logger, process1, process2);

        model.OnResultsPrinted += logger =>
        {
            logger.WriteLine($"Times changed queue: {process1.TimesChangedQueue}");
        };
        model.Simulate(1000.0);
    }

    private static void TryChangeQueue(ILogger logger, Process process1, Process process2)
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
        
        logger.WriteLine($"Queue changed: {process1.Name} {process1Queue} -> {process1.Queue}, {process2.Name} {process2Queue} -> {process2.Queue}");
    }
}


public class CustomTransition : ITransition
{
    public Element? Next => GetNext();

    private Process _process1;
    private Process _process2;

    public CustomTransition(Process process1, Process process2)
    {
        _process1 = process1;
        _process2 = process2;
    }

    private Element? GetNext()
    {
        if (_process1.Queue == _process2.Queue)
        {
            return _process1;
        }
        
        if (_process1.Queue < _process2.Queue)
        {
            return _process1;
        }
        else
        {
            return _process2;
        }
    }
}