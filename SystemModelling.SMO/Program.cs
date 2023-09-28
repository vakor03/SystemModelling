using SystemModelling.SMO;
using SystemModelling.SMO.Elements;
using SystemModelling.SMO.Enums;
using SystemModelling.SMO.TransitionOptions;

// RunDefaultTask();

// RunTask3();
RunTask5();
Console.ReadKey();

static void RunDefaultTask()
{
    Create createElement = new Create(2.0);
    Process processElement = new Process(1.0);

    Console.WriteLine($"id0 = {createElement.Id} id1={processElement.Id}");

    createElement.TransitionOption = processElement.ToSingleTransitionOption();
    processElement.MaxQueue = 5;

    createElement.Name = "CREATOR";
    processElement.Name = "PROCESSOR";

    createElement.Distribution = DistributionType.Exp;
    processElement.Distribution = DistributionType.Exp;

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

    create.TransitionOption = process1.ToSingleTransitionOption();
    process1.TransitionOption = process2.ToSingleTransitionOption();
    process2.TransitionOption = process3.ToSingleTransitionOption();

    Model model = new Model(create, process1, process2, process3);
    model.Simulate(1000);
}

static void RunTask5()
{
    Create create = new Create(2.0) { Name = "CREATOR", Distribution = DistributionType.Exp };
    Process process1 = new Process(3.0) { Name = "PROCESSOR1", Distribution = DistributionType.Exp };
    Process process2 = new Process(1.0) { Name = "PROCESSOR2", Distribution = DistributionType.Exp };

    create.TransitionOption = process1.ToSingleTransitionOption();
    process1.TransitionOption = new ProbabilitySetTransitionOption(new List<ProbabilityOption>()
    {
        new() { Element = process1, Probability = 0.4f },
        new() { Element = process2, Probability = 0.5f },
    });
    
    Model model = new Model(create, process1, process2);
    model.Simulate(1000);
}