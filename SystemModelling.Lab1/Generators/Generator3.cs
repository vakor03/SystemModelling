using MathNet.Numerics;

namespace SystemModelling.Lab1.Generators;

public class Generator3 : Generator
{
    public double a { get; set; }
    public double c { get; set; }
    
    private double zi;

    public Generator3(double a, double c, double zDefault = 1) {
        this.a = a;
        this.c = c;
        zi = 0.3;
    }

    public override double Next()
    {
        var z1 = (a * zi) % c;
        zi = z1;
        return z1 / c;
    }

    public override Func<double, double, double> PiFunc {
        get
        {
            Func<double, double, double> piFunc = (start, end) => Integrate.OnClosedInterval(_ => 1, start, end);
            return piFunc;
        }
    }
}