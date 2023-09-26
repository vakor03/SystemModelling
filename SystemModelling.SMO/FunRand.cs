using MathNet.Numerics.Distributions;

namespace SystemModelling.SMO;

public class FunRand
{
    public static double Exp(double mean)
    {
        double a = 0;
        while (a == 0)
        {
            a = Random.Shared.NextDouble();
        }

        a = -mean * Math.Log(a);
        return a;
    }

    public static double Unif(double min, double max)
    {
        double a = 0;
        while (a == 0)
        {
            a = Random.Shared.NextDouble();
        }

        a = min + a * (max - min);
        return a;
    }

    public static double Norm(double mean, double deviation)
    {
        double a;
        var normalDist = new Normal();
        a = mean + deviation * normalDist.Sample();
        return a;
    }
}