using BenchmarkDotNet.Attributes;
using SystemModelling.SMO;

namespace SystemModelling.Lab4;

public class ModelBenchmark
{
    private Model _model;


    [Params(10, 100, 200, 500, 1000)] public int ProcessesCount { get; set; }

    [GlobalSetup]
    public void GlobalSetup()
    {
        _model = ModelHelper.CreateLinearModel(ProcessesCount);
        _model.DisableLogging = true;
    }

    [IterationSetup]
    public void IterationSetup()
    {
        _model.Reset();
    }

    [Benchmark]
    public void Simulate()
    {
        _model.Simulate(100_000);
    }
}