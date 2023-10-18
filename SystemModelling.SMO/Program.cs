
using SystemModelling.SMO;
using SystemModelling.SMO.Builders;
using SystemModelling.SMO.Elements;
using SystemModelling.SMO.Enums;
using SystemModelling.SMO.Transitions;

// RunDefaultTask();
// RunTask3();
// RunTask6();
// RunTask5();

static void RunDefaultTask()
{
    Create createElement = FluentCreateBuilder.New()
        .WithDelayMean(2.0)
        .WithName("CREATOR")
        .WithDistribution(DistributionType.Exp)
        .Build();

    Process processElement = FluentProcessBuilder.New()
        .WithDelayMean(1.0)
        .WithName("PROCESSOR")
        .WithMaxQueue(5)
        .WithDistribution(DistributionType.Exp)
        .Build();

    createElement.Transition = (SingleTransition)processElement;

    Model model = new Model(createElement, processElement);
    model.Simulate(1000.0);
}

static void RunTask3()
{
    Create create = Create.New()
        .WithDelayMean(2.0)
        .WithName("CREATOR")
        .WithDistribution(DistributionType.Exp)
        .WithStartedDelay(0.1)
        .Build();
    
    Process process1 = Process.New()
        .WithDelayMean(3.0)
        .WithName("PROCESSOR1")
        .WithDistribution(DistributionType.Exp)
        .Build();
    
    Process process2 = Process.New()
        .WithDelayMean(1.0)
        .WithName("PROCESSOR2")
        .WithDistribution(DistributionType.Exp)
        .Build();
    
    Process process3 = Process.New()
        .WithDelayMean(1.0)
        .WithName("PROCESSOR3")
        .WithDistribution(DistributionType.Exp)
        .Build();
    
    create.Transition = (SingleTransition)process1;
    process1.Transition = (SingleTransition)process2;
    process2.Transition = (SingleTransition)process3;

    Model model = new Model(create, process1, process2, process3);
    model.Simulate(1000);
}

static void RunTask5()
{
    Create create = Create.New()
        .WithDelayMean(2.0)
        .WithName("CREATOR")
        .WithDistribution(DistributionType.Exp)
        .Build();
    
    Process process = Process.New()
        .WithDelayMean(3.0)
        .WithProcessesCount(2)
        .WithName("PROCESSOR")
        .WithDistribution(DistributionType.Exp)
        .Build();
    
    create.Transition = (SingleTransition)process;

    Model model = new Model(create, process);
    model.Simulate(500);
}

static void RunTask6()
{
    Create create = Create.New()
        .WithDelayMean(2.0)
        .WithName("CREATOR")
        .WithDistribution(DistributionType.Exp)
        .Build();
    
    Process process1 = Process.New()
        .WithDelayMean(3.0)
        .WithName("PROCESSOR1")
        .WithDistribution(DistributionType.Exp)
        .Build();
    
    Process process2 = Process.New()
        .WithDelayMean(1.0)
        .WithName("PROCESSOR2")
        .WithDistribution(DistributionType.Exp)
        .Build();
    
    create.Transition = (SingleTransition)process1;
    process1.Transition = new ProbabilitySetTransition(new List<ProbabilityOption>()
    {
        new() { Transition = (SingleTransition)process1, Probability = 0.4f },
        new() { Transition = (SingleTransition)process2, Probability = 0.5f },
    });

    Model model = new Model(create, process1, process2);
    model.Simulate(1000);
}