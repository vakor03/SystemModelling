// using SystemModelling.SMO.Elements;
// using SystemModelling.SMO.Enums;
//
// namespace SystemModelling.SMO.Scenarios.Lab3;
//
// public class Scenario_3_1_3 : Scenario_3_1
// {
//     public override void Run(double time = 1000.0)
//     {
//         Create create = Create.New()
//             .WithName("Create")
//             .WithDelayMean(0.15)
//             .WithDistribution(DistributionType.Exp)
//             .Build();
//
//         Process process1 = Process.New()
//             .WithName("Process1")
//             .WithDelayMean(0.3)
//             .WithDistribution(DistributionType.Exp)
//             .WithMaxQueue(3)
//             .WithStartedCount(3)
//             .Build();
//
//         Process process2 = Process.New()
//             .WithName("Process2")
//             .WithDelayMean(0.3)
//             .WithDistribution(DistributionType.Exp)
//             .WithMaxQueue(3)
//             .WithStartedCount(3)
//             .Build();
//
//
//         create.Transition = new CustomTransition(process1, process2);
//
//         Model model = new Model(create, process1, process2);
//
//         process1.OnQueueChanged += () => TryChangeQueue(model.Logger, process1, process2);
//         process2.OnQueueChanged += () => TryChangeQueue(model.Logger, process1, process2);
//
//         model.OnResultsPrinted += logger => { logger.WriteLine($"Times changed queue: {QueueChangeCount}"); };
//         
//         model.Simulate(time);
//     }
// }