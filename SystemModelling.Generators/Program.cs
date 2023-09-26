// See https://aka.ms/new-console-template for more information

using MathNet.Numerics;
using SystemModelling.Generators;

int numberOfElements = 10_000;
// double lambda = 312.12;
//
// RunTask1(lambda, numberOfElements);

double a = 232;
double sigma = 128;

RunTask2(a, sigma, numberOfElements);

// double a = Math.Pow(2.0,20.0);
// double c = Math.Pow(13.0,2.0);
//
// RunTask3(a, c, numberOfElements);

Console.ReadKey();

static void RunTask1(double lambda, int numberOfElements)
{
    Func<double, double> Fx = x => 1 - Math.Exp(-1 * lambda * x);
    Func<double, double, double> piFunc = (start, end) => Fx(end) - Fx(start);

    Generator exponentialGenerator = new ExponentialGenerator(lambda);

    CalculateChiSquared(exponentialGenerator, numberOfElements, piFunc);
}

static void RunTask2(double a, double sigma, int numberOfElements)
{
    Func<double, double> fx = x =>
        1 / (Math.Sqrt(2 * Math.PI) * sigma) * Math.Exp(-1 * Math.Pow(x - a, 2) / (2 * Math.Pow(sigma, 2)));
    Func<double, double, double> piFunc = (start, end) => Integrate.OnClosedInterval(fx, start, end);

    Generator generator = new NormalGenerator(a, sigma);

    CalculateChiSquared(generator, numberOfElements, piFunc);
}

static void RunTask3(double a, double c, int numberOfElements)
{
    Func<double, double, double> piFunc = (start, end) => Integrate.OnClosedInterval(_ => 1, start, end);

    Generator generator = new EvenDistributionGenerator(a, c);

    CalculateChiSquared(generator, numberOfElements, piFunc);
}

static void PrintResults(double chiSquared, List<Interval> intervals)
{
    Console.WriteLine($"Chi-squared: {chiSquared}; Used intervals count: {intervals.Count}");
    // Console.WriteLine($"Intervals count: {intervals.Count}");
    // Console.WriteLine("Intervals:");
    // IntervalExtensions.PrintIntervals(intervals);
}

static void CalculateChiSquared(Generator exponentialGenerator, int numberOfElements,
    Func<double, double, double> piFunc)
{
    double[] generatedValues = exponentialGenerator.GenerateMany(numberOfElements);

    List<Interval> intervals = generatedValues.SplitIntoIntervals(20);
    intervals.PrintIntervals();
    
    intervals.UniteSmallIntervals();


    double chiSquared =
        GeneratorAnalytics.FindChiSquared(intervals, piFunc, numberOfElements);
    
    double mean = GeneratorAnalytics.CalculateMean(generatedValues);
    
    double variance = GeneratorAnalytics.CalculateVariance(generatedValues);

    Console.WriteLine($"\nMean: {mean}; \nVariance: {variance}");
    PrintResults(chiSquared, intervals);
}