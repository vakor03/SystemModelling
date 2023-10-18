using SystemModelling.Shared;
using SystemModelling.SMO.Elements;
using SystemModelling.SMO.Transitions;

namespace SystemModelling.SMO.Scenarios.Lab3;

public abstract class Scenario_3_1 : Scenario
{
    public int QueueChangeCount { get; protected set; }

    protected void TryChangeQueue(ILogger logger, Process process1, Process process2)
    {
        if (Math.Abs(process1.Queue - process2.Queue) < 2)
        {
            return;
        }

        int process1Queue = process1.Queue;
        int process2Queue = process2.Queue;

        if (process1.Queue < process2.Queue)
        {
            process1.Queue++;
            process2.Queue--;
        }
        else
        {
            process1.Queue--;
            process2.Queue++;
        }

        QueueChangeCount++;

        logger.WriteLine(
            $"Queue changed: {process1.Name} {process1Queue} -> {process1.Queue}, {process2.Name} {process2Queue} -> {process2.Queue}");
    }

    protected class CustomTransition : ITransition
    {
        public Element? Next => GetNext();

        private Process _process1;
        private Process _process2;

        public CustomTransition(Process process1, Process process2)
        {
            _process1 = process1;
            _process2 = process2;
        }

        private Element? GetNext()
        {
            if (_process1.Queue == _process2.Queue)
            {
                return _process1;
            }

            if (_process1.Queue < _process2.Queue)
            {
                return _process1;
            }
            else
            {
                return _process2;
            }
        }
    }
}