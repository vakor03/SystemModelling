// using SystemModelling.SMO.Elements;
// using SystemModelling.SMO.Enums;
// using SystemModelling.SMO.Transitions;
//
// namespace SystemModelling.SMO.Scenarios.Lab2;
//
// public class Scenario_2_3 : Scenario
// {
//     public override void Run(double time = 1000.0)
//     {
//         Create create = Create.New()
//             .WithDelayMean(2.0)
//             .WithName("CREATOR")
//             .WithDistribution(DistributionType.Exp)
//             .WithStartedDelay(0.1)
//             .Build();
//     
//         Process process1 = Process.New()
//             .WithDelayMean(3.0)
//             .WithName("PROCESSOR1")
//             .WithDistribution(DistributionType.Exp)
//             .Build();
//     
//         Process process2 = Process.New()
//             .WithDelayMean(1.0)
//             .WithName("PROCESSOR2")
//             .WithDistribution(DistributionType.Exp)
//             .Build();
//     
//         Process process3 = Process.New()
//             .WithDelayMean(1.0)
//             .WithName("PROCESSOR3")
//             .WithDistribution(DistributionType.Exp)
//             .Build();
//     
//         create.Transition = (SingleTransition)process1;
//         process1.Transition = (SingleTransition)process2;
//         process2.Transition = (SingleTransition)process3;
//
//         Model model = new Model(create, process1, process2, process3);
//         model.Simulate(time);
//     }
// }
//
// public class Scenario_2_5 : Scenario
// {
//     public override void Run(double time = 1000.0)
//     {
//         Create create = Create.New()
//             .WithDelayMean(2.0)
//             .WithName("CREATOR")
//             .WithDistribution(DistributionType.Exp)
//             .Build();
//     
//         Process process = Process.New()
//             .WithDelayMean(3.0)
//             .WithProcessesCount(2)
//             .WithName("PROCESSOR")
//             .WithDistribution(DistributionType.Exp)
//             .Build();
//     
//         create.Transition = (SingleTransition)process;
//
//         Model model = new Model(create, process);
//         model.Simulate(time);
//     }
// }
//
// public class Scenario_2_6 : Scenario
// {
//     public override void Run(double time = 1000.0)
//     {
//         Create create = Create.New()
//             .WithDelayMean(2.0)
//             .WithName("CREATOR")
//             .WithDistribution(DistributionType.Exp)
//             .Build();
//     
//         Process process1 = Process.New()
//             .WithDelayMean(3.0)
//             .WithName("PROCESSOR1")
//             .WithDistribution(DistributionType.Exp)
//             .Build();
//     
//         Process process2 = Process.New()
//             .WithDelayMean(1.0)
//             .WithName("PROCESSOR2")
//             .WithDistribution(DistributionType.Exp)
//             .Build();
//     
//         create.Transition = (SingleTransition)process1;
//         process1.Transition = new ProbabilityTransition(new List<ProbabilityOption>()
//         {
//             new() { Element = process1, Probability = 0.4f },
//             new() { Element = process2, Probability = 0.5f },
//         });
//
//         Model model = new Model(create, process1, process2);
//         model.Simulate(time);
//     }
// }