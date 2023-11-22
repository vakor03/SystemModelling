using BenchmarkDotNet.Attributes;
using SystemModelling.SMO;

namespace SystemModelling.Lab4;

public class BranchedModelBenchmark
{
    private Model _model;
    [Params(3,5,10/*,15,20*/)] public int LayersCount { get; set; }

    public float SimulationTime { get; set; } = 100_000;

    [GlobalSetup]
    public void GlobalSetup()
    {
        _model = ModelHelper.CreateBranchedModel(LayersCount);
        _model.DisableLogging = true;
    }

    [IterationSetup]
    public void IterationSetup()
    {
        _model.Reset();
    }

    [Benchmark]
    public void SimulateBranchedModel()
    {
        _model.Simulate(SimulationTime);
    }
}