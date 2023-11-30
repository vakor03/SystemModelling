using SystemModelling.SMO.Elements;
using SystemModelling.SMO.Loggers;

namespace SystemModelling.Lab3.Scenarios;

internal class BankChangeQueueBehaviour
{
    public int QueueChangeCount { get; protected set; }

    public void TryChangeQueue(ILogger logger, Process process1, Process process2)
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
}