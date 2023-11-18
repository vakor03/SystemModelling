using SystemModelling.ModelWIthItems.DelayGenerators;

namespace SystemModelling.ModelWIthItems.Elements;

public partial class Creator
{
    public class FluentCreateBuilder : Element.FluentElementBuilder<FluentCreateBuilder>
    {
        public static FluentCreateBuilder New() => new();

        public FluentCreateBuilder WithStartedDelay(double startedDelay)
        {
            StartedDelay = startedDelay;
            return this;
        }

        public double StartedDelay { get; set; }

        public override Creator Build()
        {
            Creator create = new Creator
            {
                Name = Name,
                // DelayMean = DelayMean,
                // DelayDeviation = DelayDeviation,
                // Distribution = Distribution,
                // Id = Element.NextId++,
                DelayGenerator = DelayGeneratorFactory.Create(DelayMean, DelayDeviation, Distribution),
                TNext = StartedDelay
            };

            return create;
        }
    }
}

public enum DistributionType
{
    Normal,
    Exponential,
    Uniform,
    Erlang
}

public static class DelayGeneratorFactory
{
    public static IDelayGenerator Create(double delayMean, double delayDeviation, DistributionType distributionType)
    {
        return distributionType switch
        {
            DistributionType.Erlang => new ErlangDelayGenerator(delayMean, 2),
            DistributionType.Normal => new NormalDelayGenerator(delayMean, delayDeviation),
            DistributionType.Exponential => new ExponentialDelayGenerator(delayMean),
            DistributionType.Uniform => new UniformDelayGenerator(delayMean, delayDeviation),
            _ => throw new ArgumentOutOfRangeException(nameof(distributionType), distributionType, null)
        };
    }
}