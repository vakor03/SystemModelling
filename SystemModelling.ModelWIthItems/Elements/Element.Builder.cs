namespace SystemModelling.ModelWIthItems.Elements;

public partial class Element
{
    public class FluentElementBuilder : FluentElementBuilder<FluentElementBuilder>
    {
        public static FluentElementBuilder New() => new();
    }
    
    public class FluentElementBuilder<T> where T : FluentElementBuilder<T>
    {
        protected string Name = string.Empty;
    
        protected double DelayMean;
    
        protected double DelayDeviation;
    
        protected DistributionType Distribution = DistributionType.Exponential;
    
        public T WithName(string name)
        {
            Name = name;
            return (T)this;
        }
    
        public T WithDelayMean(double delayMean)
        {
            DelayMean = delayMean;
            return (T)this;
        }
    
        public T WithDelayDeviation(double delayDeviation)
        {
            DelayDeviation = delayDeviation;
            return (T)this;
        }
    
        public T WithDistribution(DistributionType distribution)
        {
            Distribution = distribution;
            return (T)this;
        }
    
        public virtual Element Build()
        {
            Element element = new Element
            {
                Name = Name,
                DelayGenerator = DelayGeneratorFactory.Create(DelayMean, DelayDeviation, Distribution),
                // DelayMean = DelayMean,
                // DelayDeviation = DelayDeviation,
                // Distribution = Distribution,
                // Id = Element.NextId++
            };
            return element;
        }
    }
}