// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Running;
using SystemModelling.Lab4;
using SystemModelling.SMO;

// BenchmarkRunner.Run<LinearModelBenchmark>();
BenchmarkRunner.Run<BranchedModelBenchmark>();

// Model model = ModelHelper.CreateBranchedModel(10);
// model.DisableLogging = true;
// var simulationTime = 100_000;
//
// for(int i = 0; i < 10; i++)
// {
//     model.Simulate(simulationTime);
//     model.Reset();
// }