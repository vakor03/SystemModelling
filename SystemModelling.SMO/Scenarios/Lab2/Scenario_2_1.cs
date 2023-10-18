using SystemModelling.SMO.Builders;
using SystemModelling.SMO.Elements;
using SystemModelling.SMO.Enums;
using SystemModelling.SMO.Transitions;

namespace SystemModelling.SMO.Scenarios.Lab2;

public class Scenario_2_1 : Scenario
{
    public override void Run(double time = 1000.0)
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
        model.Simulate(time);
    }
}