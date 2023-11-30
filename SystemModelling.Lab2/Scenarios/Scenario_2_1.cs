using SystemModelling.SMO;
using SystemModelling.SMO.Elements;
using SystemModelling.SMO.Loggers;
using SystemModelling.SMO.RandomValuesProviders;
using SystemModelling.SMO.Transitions;

namespace SystemModelling.Lab2.Scenarios;

public class Scenario_2_1 : Scenario
{
    public override void Run(double time = 1000.0)
    {
        Create create = new Create()
        {
            Name = "Creator",
            DelayGenerator = new Exponential(1.0).ToGenerator(),
        };
        
        Process process = new Process(1)
        {
            Name = "Processor",
            DelayGenerator = new Exponential(2.0).ToGenerator(),
            MaxQueue = 5,
        };
        process.AddStatistics(new StandardProcessStatistics());
        create.Transition = new SingleTransition(process);
        
        Model model = new Model(create, process)
        {
            Logger = new FileLogger("lab2_1.txt"),
        };
        
        model.Simulate(time);
    }
}