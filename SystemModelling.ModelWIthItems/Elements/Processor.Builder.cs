using SystemModelling.ModelWIthItems.Collections;
using SystemModelling.ModelWIthItems.Patients;

namespace SystemModelling.ModelWIthItems.Elements;

public partial class Processor{
    public class FluentProcessBuilder : FluentElementBuilder<FluentProcessBuilder>
    {
        protected int MaxQueue = 0;
        protected int Quantity = 1;
        protected IQueue<Patient> Queue = new SimpleQueue<Patient>(Int32.MaxValue);

        public static FluentProcessBuilder New() => new();
    
        public FluentProcessBuilder WithProcessesCount(int count)
        {
            Quantity = count;
            return this;
        }
        
        public FluentProcessBuilder WithQueue(IQueue<Patient> queue)
        {
            Queue = queue;
            return this;
        }

        public override Processor Build()
        {
            Processor process = new Processor
            {
                Name = Name,
                // DelayMean = DelayMean,
                // DelayDeviation = DelayDeviation,
                // Distribution = Distribution,
                TNext = Double.MaxValue,
                DelayGenerator = DelayGeneratorFactory.Create(DelayMean, DelayDeviation, Distribution),
                _patientsQueue = Queue,
                _subprocesses = GenerateSubprocesses(Quantity)
                // Id = Element.NextId++
            };

            return process;
        }

        private Subprocess[] GenerateSubprocesses(int subprocessesCount)
        {
            var subprocesses = new Subprocess[subprocessesCount];
            for (int i = 0; i < subprocessesCount; i++)
            {
                subprocesses[i] = new Subprocess
                {
                    TNext = Double.MaxValue
                };
            }

            return subprocesses;
        }
    }
}