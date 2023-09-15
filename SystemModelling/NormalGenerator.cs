namespace SystemModelling;

public class NormalGenerator : Generator
{
    public double a { get; set; }
    public double Sigma { get; set; }

    public NormalGenerator(double a, double sigma)
    {
        this.a = a;
        Sigma = sigma;
    }

    public override double Next()
    {
        double u = Enumerable.Range(0, 12).Sum(_ => Random.Shared.NextDouble()) - 6;
        return Sigma * u + a;
    }
}

public class EvenDistributionGenerator : Generator
{
    public double a { get; set; }
    public double c { get; set; }
    
    private double zi;

    public EvenDistributionGenerator(double a, double c)
    {
        this.a = a;
        this.c = c;

        zi = 1;
    }

    public override double Next()
    {
        var z1 = (a * zi) % c;
        zi = z1;
        return z1 / c;
    }
}