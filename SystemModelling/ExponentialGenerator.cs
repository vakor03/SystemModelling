namespace SystemModelling;

public class ExponentialGenerator : Generator
{
    public double Lambda { get; set; }

    public ExponentialGenerator(double lambda)
    {
        Lambda = lambda;
    }

    public override double Next()
    {
        double evenlyDistributedValue = Random.Shared.NextDouble();

        return -Math.Log(evenlyDistributedValue) / Lambda;
    }
}