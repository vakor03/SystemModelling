namespace SystemModelling.Generators.Generators;

public class Generator1 : Generator
{
    public double Lambda { get; set; }

    public Generator1(double lambda) {
        Lambda = lambda;
    }

    public override double Next()
    {
        double evenlyDistributedValue = Random.Shared.NextDouble();

        return -Math.Log(evenlyDistributedValue) / Lambda;
    }

    public override Func<double, double, double> PiFunc {
        get
        {
            Func<double, double> Fx = x => 1 - Math.Exp(-1 * Lambda * x);
            Func<double, double, double> piFunc = (start, end) => Fx(end) - Fx(start);

            return piFunc;
        }
    }
}