namespace SystemModelling;

public class NormalGenerator : IGenerator
{
    public double a { get; set; }
    public double Sigma { get; set; }

    public NormalGenerator(double a, double sigma)
    {
        this.a = a;
        Sigma = sigma;
    }

    public double Next()
    {
        double u = Enumerable.Range(0, 12).Sum(_ => Random.Shared.NextDouble() - 6);
        return Sigma * u + a;
    }
}