using SystemModelling;

public class ExponentialGenerator : IGenerator
{
    public double Lambda { get; set; }

    public ExponentialGenerator(double lambda)
    {
        Lambda = lambda;
    }

    public double Next()
    {
        double evenlyDistributedValue = Random.Shared.NextDouble();

        return -Math.Log(evenlyDistributedValue) / Lambda;
    }
}