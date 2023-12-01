using SystemModelling.SMO;
using SystemModelling.SMO.Elements;
using SystemModelling.SMO.Loggers;
using SystemModelling.SMO.RandomValuesProviders;
using SystemModelling.SMO.Transitions;

namespace SystemModelling.Lab2.Scenarios;

public class Scenario_2_5 : Scenario
{
    public override void Run(double time)
    {
        Create create = new Create()
        {
            Name = "Creator",
            DelayGenerator = new Exponential(2.0).ToGenerator(),
        };
        
        Process process = new Process(2)
        {
            Name = "Processor",
            DelayGenerator = new Exponential(3.0).ToGenerator(),
            MaxQueue = 5,
        };
        process.AddStatistics(new StandardProcessStatistics());
        
        create.Transition = (SingleTransition)process;

        Model model = new Model(create, process)
        {
            Logger = new FileLogger("lab2_5.txt"),
        };
        model.Simulate(time);
    }
}