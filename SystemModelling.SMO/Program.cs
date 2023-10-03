using SystemModelling.SMO;
using SystemModelling.SMO.Builders;
using SystemModelling.SMO.Elements;
using SystemModelling.SMO.Enums;
using SystemModelling.SMO.Transitions;

// RunDefaultTask();

RunTask3();

// RunTask5();
//
// RunTask6();
Console.ReadKey();

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

    Console.WriteLine($"id0 = {createElement.Id} id1={processElement.Id}");

    createElement.Transition = (SingleTransition)processElement;

    List<Element> list = new()
    {
        createElement,
        processElement
    };

    Model model = new Model(list);
    model.Simulate(1000.0);
}

static void RunTask3()
{
    Create create = new Create(2.0) { Name = "CREATOR", Distribution = DistributionType.Exp };
    Process process1 = new Process(3.0) { Name = "PROCESSOR1", Distribution = DistributionType.Exp };
    Process process2 = new Process(1.0) { Name = "PROCESSOR2", Distribution = DistributionType.Exp };
    Process process3 = new Process(1.0) { Name = "PROCESSOR3", Distribution = DistributionType.Exp };

    create.Transition = (SingleTransition)process1;
    process1.Transition = (SingleTransition)process2;
    process2.Transition = (SingleTransition)process3;

    Model model = new Model(create, process1, process2, process3);
    model.Simulate(1000);
}

static void RunTask5()
{
    Create create = new Create(1.0) { Name = "CREATOR", Distribution = DistributionType.Exp };
    Process process1 = new Process(2.5) { Name = "PROCESSOR1", Distribution = DistributionType.Exp };
    Process process2 = new Process(2.0) { Name = "PROCESSOR2", Distribution = DistributionType.Exp };

    create.Transition = new MultipleProcessesTransition(process1, process2);

    Model model = new Model(create, process1, process2);
    model.Simulate(500);
}

static void RunTask6()
{
    Create create = new Create(2.0) { Name = "CREATOR", Distribution = DistributionType.Exp };
    Process process1 = new Process(3.0) { Name = "PROCESSOR1", Distribution = DistributionType.Exp };
    Process process2 = new Process(1.0) { Name = "PROCESSOR2", Distribution = DistributionType.Exp };

    create.Transition = (SingleTransition)process1;
    process1.Transition = new ProbabilitySetTransition(new List<ProbabilityOption>()
    {
        new() { Transition = (SingleTransition)process1, Probability = 0.4f },
        new() { Transition = (SingleTransition)process2, Probability = 0.5f },
    });

    Model model = new Model(create, process1, process2);
    model.Simulate(1000);
}