// See https://aka.ms/new-console-template for more information

using MathNet.Numerics;
using SystemModelling;

// RunTask1();
// RunTask2();
// RunTask3();

static void RunTask1()
{
    int numberOfElements = 10000;
    double lambda = 0.001201020120;
    
    Func<double, double> Fx = x => 1 - Math.Exp(-1 * lambda * x);
    Func<double, double, double> piFunc = (start, end)=> Fx(end) - Fx(start);

    Generator exponentialGenerator = new ExponentialGenerator(lambda);

    CalculateChiSquared(exponentialGenerator, numberOfElements, piFunc);
}

static void RunTask2()
{
    double a = 100;
    double sigma = 100000;
    int numberOfElements = 10000;
    
    Func<double, double> fx = x =>
        1 / (Math.Sqrt(2 * Math.PI) * sigma) * Math.Exp(-1 * Math.Pow(x - a, 2) / (2 * Math.Pow(sigma, 2)));
    Func<double, double, double> piFunc = (start, end) => Integrate.OnClosedInterval(fx, start, end);

    Generator generator = new NormalGenerator(a, sigma);

    CalculateChiSquared(generator, numberOfElements, piFunc);
}

static void RunTask3()
{
    double a = Math.Pow(5, 13);
    double c = Math.Pow(2, 31);
    int numberOfElements = 10000;
    Func<double, double, double> piFunc = (start, end) => Integrate.OnClosedInterval(_ => 1, start, end);

    Generator generator = new EvenDistributionGenerator(a, c);

    CalculateChiSquared(generator, numberOfElements, piFunc);
}

static void PrintResults(double chiSquared, List<Interval> intervals)
{
    Console.WriteLine($"Chi-squared: {chiSquared}");
    // Console.WriteLine("Intervals:");
    // IntervalExtensions.PrintIntervals(intervals);
    Console.WriteLine($"Intervals count: {intervals.Count}");
}

static void CalculateChiSquared(Generator exponentialGenerator, int numberOfElements, Func<double, double, double> piFunc)
{
    double[] generatedValues = exponentialGenerator.GenerateMany(numberOfElements);

    List<Interval> rawIntervals = generatedValues.SplitIntoIntervals(20);
    List<Interval> intervals = rawIntervals.UniteSmallIntervals();

    double chiSquared =
        GeneratorAnalytics.FindChiSquared(intervals, piFunc, numberOfElements);

    PrintResults(chiSquared, intervals);
}