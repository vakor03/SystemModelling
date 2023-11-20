using SystemModelling.ModelWIthItems.Collections;
using SystemModelling.ModelWIthItems.Patients;

namespace SystemModelling.ModelWIthItems.Elements;

public partial class Processor{
    public class FluentProcessBuilder : FluentElementBuilder<FluentProcessBuilder>
    {
        protected int SubprocessesCount = 1;
        protected IQueue<Patient> Queue = new SimpleQueue<Patient>(Int32.MaxValue);

        public static FluentProcessBuilder New() => new();
    
        public FluentProcessBuilder WithProcessesCount(int count)
        {
            SubprocessesCount = count;
            return this;
        }
        
        public FluentProcessBuilder WithQueue(IQueue<Patient> queue)
        {
            Queue = queue;
            return this;
        }

        public override Processor Build()
        {
            Processor process = new Processor(SubprocessesCount)
            {
                Name = Name,
                TNext = Double.MaxValue,
                DelayGenerator = DelayGenerator,
                PatientsQueue = Queue,
            };

            return process;
        }
    }
}