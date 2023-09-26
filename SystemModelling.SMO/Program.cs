using SystemModelling.SMO;

// RunDefaultTask();

RunTask3();
Console.ReadKey();

static void RunDefaultTask()
{
    Create createElement = new Create(2.0);
    Process processElement = new Process(1.0);

    Console.WriteLine($"id0 = {createElement.Id} id1={processElement.Id}");

    createElement.NextElement = processElement;
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

    create.NextElement = process1;
    process1.NextElement = process2;
    process2.NextElement = process3;

    Model model = new Model(create, process1, process2, process3);
    model.Simulate(1000);
}

