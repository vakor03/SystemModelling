namespace SystemModelling.Generators
{
    public class GeneratorAnalytics
    {
        public static double CalculateMean(double[] generatedValues)
        {
            return generatedValues.Average();
        }

        public static double CalculateVariance(double[] generatedValues)
        {
            double mean = CalculateMean(generatedValues);

            return generatedValues.Select(x => Math.Pow(x - mean, 2)).Average();
        }

        public static double FindChiSquared(List<Interval> intervals, Func<double,double, double> piFunc, int numberOfElements)
        {
            double chiSquared = 0;
            foreach (var interval in intervals)
            {
                var npiValue = (piFunc(interval.Start,interval.End)) * numberOfElements;
                chiSquared += Math.Pow(interval.ElementsCount - npiValue, 2) / npiValue;
            }

            return chiSquared;
        }
    }
}