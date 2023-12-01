using SystemModelling.SMO;
using SystemModelling.SMO.Elements;
using SystemModelling.SMO.Loggers;
using SystemModelling.SMO.RandomValuesProviders;
using SystemModelling.SMO.Transitions;

namespace SystemModelling.Lab2.Scenarios;

public class Scenario_2_3 : Scenario
{
    public override void Run(double time)
    {
        var maxQueue = 5;
        var creationTime = 2;
        
        Create create = new Create()
        {
            Name = "Creator",
            DelayGenerator = new Exponential(creationTime).ToGenerator(),
        };

        Process process1 = new Process(1)
        {
            Name = "Processor",
            DelayGenerator = new Exponential(2.0).ToGenerator(),
            MaxQueue = maxQueue,
        };
        
        Process process2 = new Process(1)
        {
            Name = "Processor2",
            DelayGenerator = new Exponential(3.0).ToGenerator(),
            MaxQueue = maxQueue,
        };
        
        Process process3 = new Process(1)
        {
            Name = "Processor3",
            DelayGenerator = new Exponential(4.0).ToGenerator(),
            MaxQueue = maxQueue,
        };
        process1.AddStatistics(new StandardProcessStatistics());
        process2.AddStatistics(new StandardProcessStatistics());
        process3.AddStatistics(new StandardProcessStatistics());
        
        create.Transition = (SingleTransition)process1;
        process1.Transition = (SingleTransition)process2;
        process2.Transition = (SingleTransition)process3;

        Model model = new Model(create, process1, process2, process3)
        {
            Logger = new FileLogger("lab2_3.txt")
        };
        model.Simulate(time);
    }
}