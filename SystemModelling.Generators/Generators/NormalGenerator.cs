﻿using MathNet.Numerics;

namespace SystemModelling.Generators.Generators;

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

    public override Func<double, double, double> PiFunc
    {
        get
        {
            Func<double, double> fx = x =>
                1 / (Math.Sqrt(2 * Math.PI) * Sigma) * Math.Exp(-1 * Math.Pow(x - a, 2) / (2 * Math.Pow(Sigma, 2)));
            Func<double, double, double> piFunc = (start, end) => Integrate.OnClosedInterval(fx, start, end);
            
            return piFunc;
        }
    }
}