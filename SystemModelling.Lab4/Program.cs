// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Running;
using SystemModelling.Lab4;
using SystemModelling.SMO;

// BenchmarkRunner.Run<LinearModelBenchmark>();
BenchmarkRunner.Run<BranchedModelBenchmark>();

// Model model = ModelHelper.CreateBranchedModel(50);
// model.DisableLogging = true;
//
// for(int i = 0; i < 100; i++)
// {
//     model.Reset();
//     model.Simulate(100_000);
// }