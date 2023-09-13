public class GeneratorAnalytics
{
    public double CalculateMean(IEnumerable<double> values)
    {
        return values.Average();
    }
    
    public double CalculateVariance(IEnumerable<double> values)
    {
        double mean = CalculateMean(values);

        return values.Select(x => Math.Pow(x - mean, 2)).Average();
    }
}