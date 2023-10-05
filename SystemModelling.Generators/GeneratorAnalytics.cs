using SystemModelling.Generators.Generators;
using SystemModelling.Generators.Intervals;
using SystemModelling.Shared;

namespace SystemModelling.Generators
{
    public class GeneratorAnalytics
    {
        private Generator _generator;
        private double[] _generatedValues;
        private ILogger _logger;

        public GeneratorAnalytics(Generator generator, ILogger? logger = null)
        {
            _generator = generator;
            _logger = logger ?? new NullLogger();
        }
        
        public GeneratorAnalytics(Generator generator, int count, ILogger? logger = null) : this(generator, logger)
        {
            GenerateNewValues(count);
        }

        public void GenerateNewValues(int count)
        {
            _generatedValues = _generator.GenerateMany(count);
            _logger.WriteLine($"Generated {_generatedValues.Length} values");
            _logger.WriteLine($"Mean: {CalculateMean()}");
            _logger.WriteLine($"Variance: {CalculateVariance()}");
        }
        public double CalculateMean()
        {
            return _generatedValues.Average();
        }

        public double CalculateVariance()
        {
            double mean = CalculateMean();

            return _generatedValues.Select(x => Math.Pow(x - mean, 2)).Average();
        }

        public bool CheckChiSquared(double chiSquared, int intervalsCount)
        {
            double chiSquaredTableValue = ChiSquaredTable.GetChiSquared(intervalsCount - 2);
            _logger.WriteLine($"ChiSquared = {chiSquared}, Table value = {chiSquaredTableValue}");

            var isSuccessful = chiSquared < chiSquaredTableValue;
            _logger.WriteLine($"Is successful: {isSuccessful}");
            return isSuccessful;
        }
        
        public bool CheckChiSquared(int intervalsCount)
        {
            double chiSquared = CalculateChiSquared(intervalsCount,out int outIntervalsCount);
            _logger.WriteLine($"Intervals: {intervalsCount} -> {outIntervalsCount}");
            return CheckChiSquared(chiSquared, outIntervalsCount);
        }

        public double CalculateChiSquared(int defaultIntervalsCount, out int outIntervalsCount)
        {
            List<Interval> intervals = _generatedValues.SplitIntoIntervals(defaultIntervalsCount);
            intervals.UniteSmallIntervals();
            outIntervalsCount = intervals.Count;

            double chiSquared = FindChiSquared(intervals, _generator.PiFunc, _generatedValues.Length);
            return chiSquared;
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