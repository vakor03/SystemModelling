using SystemModelling.ModelWIthItems.DelayGenerators;
using SystemModelling.ModelWIthItems.Patients;
using SystemModelling.ModelWIthItems.RandomValuesProviders;

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

        protected IDelayGenerator<Patient> DelayGenerator = new Exponential(1).ToDelayGenerator();

        public T WithName(string name)
        {
            Name = name;
            return (T)this;
        }
        
        public T WithDelayGenerator(IDelayGenerator<Patient> delayGenerator)
        {
            DelayGenerator = delayGenerator;
            return (T)this;
        }

        public virtual Element Build()
        {
            Element element = new Element
            {
                Name = Name,
                DelayGenerator = DelayGenerator,
            };
            return element;
        }
    }
}