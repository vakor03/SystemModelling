// See https://aka.ms/new-console-template for more information

using SystemModelling.Generators;
using SystemModelling.Generators.Generators;
using SystemModelling.Shared;

int numberOfElements = 10_000;
// {
//     double lambda = 312.12;
//     double[] testLambdas = { 0.1, 1, 3.14, 25, 46, 222 };
//
//     foreach (var testLambda in testLambdas)
//     {
//         CheckGeneratorCorrectness(new Generator1(testLambda), numberOfElements, 250);
//     }
//
//     // RunGeneratorAnalytics(new Generator1(lambda), numberOfElements);
// }
//
// {
//     double a = 0.33;
//     double sigma = 0.75;
//
//     double[] testAlphas = { 0.1, 0.5, 1, 2, 10, 100, 500 };
//     double[] testSigmas = { 0.1, 0.5, 1, 2, 10, 100, 500 };
//
//     foreach (var testAlpha in testAlphas)
//     {
//         CheckGeneratorCorrectness(new Generator2(testAlpha, sigma), numberOfElements, 250);
//     }
//     
//     foreach (var testSigma in testSigmas)
//     {
//         CheckGeneratorCorrectness(new Generator2(a, testSigma), numberOfElements, 250);
//     }
// // RunGeneratorAnalytics(new Generator2(a, sigma), numberOfElements);
// }
//
//
{
    double a = Math.Pow(2, 13.0);
    double c = Math.Pow(2.0, 31.0);

    double[] testA = { 2, 4, 8, 13, 16 };
    double[] testC = { 4, 8, 16, 25, 50 };

    foreach (var testAValue in testA)
    {
        CheckGeneratorCorrectness(new Generator3(Math.Pow(5.0, testAValue), c), numberOfElements, 250);
    }

    Console.WriteLine();

    foreach (var testCValue in testC)
    {
        CheckGeneratorCorrectness(new Generator3(a, Math.Pow(2.0, testCValue)), numberOfElements, 250);
    }


    // RunGeneratorAnalytics(new Generator3(a, c), numberOfElements);
}


Console.ReadKey();

static void RunGeneratorAnalytics(Generator generator, int numberOfElements, int intervalsCount = 100)
{
    GeneratorAnalytics generatorAnalytics = new(generator, numberOfElements, new ConsoleLogger());
    generatorAnalytics.CheckChiSquared(intervalsCount);
    Console.WriteLine();
}

static void CheckGeneratorCorrectness(Generator generator, int numberOfElements, int testsCount = 100,
    int intervalsCount = 100)
{
    GeneratorAnalytics generatorAnalytics = new(generator, numberOfElements);
    int successfulTestsCount = 0;
    for (int i = 0; i < testsCount; i++)
    {
        generatorAnalytics.GenerateNewValues(numberOfElements);
        if (generatorAnalytics.CheckChiSquared(intervalsCount))
        {
            successfulTestsCount++;
        }
    }

    Console.WriteLine(
        $"Successful tests: {successfulTestsCount} / {testsCount} ({successfulTestsCount * 100.0 / testsCount}%)");
}