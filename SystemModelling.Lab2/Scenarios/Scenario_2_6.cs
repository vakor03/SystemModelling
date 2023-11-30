using SystemModelling.SMO;
using SystemModelling.SMO.Elements;
using SystemModelling.SMO.Enums;
using SystemModelling.SMO.Loggers;
using SystemModelling.SMO.RandomValuesProviders;
using SystemModelling.SMO.Transitions;

namespace SystemModelling.Lab2.Scenarios;

public class Scenario_2_6 : Scenario
{
    public override void Run(double time)
    {
        Create create = new Create()
        {
            Name = "Creator",
            DelayGenerator = new Exponential(2.0).ToGenerator(),
        };

        Process process1 = new Process(1)
        {
            Name = "Processor",
            DelayGenerator = new Exponential(3.0).ToGenerator(),
            MaxQueue = 5,
        };

        Process process2 = new Process(1)
        {
            Name = "Processor2",
            DelayGenerator = new Exponential(1.0).ToGenerator(),
            MaxQueue = 5,
        };
        process1.AddStatistics(new StandardProcessStatistics());
        process2.AddStatistics(new StandardProcessStatistics());

        create.Transition = (SingleTransition)process1;
        process1.Transition = new ProbabilityTransition(new List<ProbabilityOption>()
        {
            new() { Element = process1, Probability = 0.25f },
            new() { Element = process2, Probability = 0.75f },
        });

        Model model = new Model(create, process1, process2)
        {
            Logger = new FileLogger("lab2_6.txt")
        };
        model.Simulate(time);
    }
}