namespace SystemModelling.Generators;

public class NormalGenerator : Generator
{
    public double a { get; set; }
    public double Sigma { get; set; }

    public NormalGenerator(double a, double sigma) {
        this.a = a;
        Sigma = sigma;
    }

    public override double Next()
    {
        double u = Enumerable.Range(0, 12).Sum(_ => Random.Shared.NextDouble()) - 6;
        return Sigma * u + a;
    }
}