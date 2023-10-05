// See https://aka.ms/new-console-template for more information

using SystemModelling.Generators;
using SystemModelling.Generators.Generators;
using SystemModelling.Shared;

int numberOfElements = 10_000;
// double lambda = 312.12;
//
// RunTask1(lambda, numberOfElements);

// double a = 232;
// double sigma = 128;
//
// RunTask2(a, sigma, numberOfElements);

double a = Math.Pow(3,4.0);
double c = Math.Pow(6.0,15.0);

RunTask3(a, c, numberOfElements);

Console.ReadKey();

static void RunTask1(double lambda, int numberOfElements)
{
    Generator generator = new ExponentialGenerator(lambda);

    var goodGenerator = RunGeneratorAnalytics(generator, numberOfElements);
}

static void RunTask2(double a, double sigma, int numberOfElements)
{
    Generator generator = new NormalGenerator(a, sigma);

    var goodGenerator = RunGeneratorAnalytics(generator, numberOfElements);
}

static void RunTask3(double a, double c, int numberOfElements)
{
    Generator generator = new EvenDistributionGenerator(a, c);

    var goodGenerator = RunGeneratorAnalytics(generator, numberOfElements);
}

static bool RunGeneratorAnalytics(Generator generator, int numberOfElements, int intervalsCount = 100)
{
    GeneratorAnalytics generatorAnalytics = new(generator, numberOfElements, new ConsoleLogger());
    return generatorAnalytics.CheckChiSquared(intervalsCount);
}

static void CheckGeneratorCorrectness(Generator generator, int numberOfElements, int testsCount = 100, int intervalsCount = 100)
{
    GeneratorAnalytics generatorAnalytics = new(generator, numberOfElements);
    int successfulTestsCount = 0;
    for (int i = 0; i < testsCount; i++)
    {
        if (generatorAnalytics.CheckChiSquared(intervalsCount))
        {
            successfulTestsCount++;
        }
    }

    Console.WriteLine($"Successful tests: {successfulTestsCount} / {testsCount} ({successfulTestsCount * 100.0 / testsCount}%)");
}