namespace SystemModelling.Generators;

public class EvenDistributionGenerator : Generator
{
    public double a { get; set; }
    public double c { get; set; }
    
    private double zi;

    public EvenDistributionGenerator(double a, double c, double zDefault = 1) {
        this.a = a;
        this.c = c;
        zi = zDefault;
    }

    public override double Next()
    {
        var z1 = (a * zi) % c;
        zi = z1;
        return z1 / c;
    }
}