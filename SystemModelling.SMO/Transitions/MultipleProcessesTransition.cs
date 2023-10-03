using SystemModelling.SMO.Elements;

namespace SystemModelling.SMO.Transitions;

public class MultipleProcessesTransition : ITransition
{
    public Element? Next => ChooseNext();
    private readonly Process[] _next;

    public MultipleProcessesTransition(params Process[] next)
    {
        _next = next;
    }

    private Process ChooseNext()
    {
        if (_next == null || _next.Length == 0)
        {
            throw new Exception("No next element");
        }

        foreach (var process in _next)
        {
            if (process.CurrentState == Element.State.Free)
            {
                return process;
            }
        }

        return FindProcessWithSmallestQueue();
    }

    private Process FindProcessWithSmallestQueue()
    {
        int minQueue = _next[0].Queue;
        Process processWithSmallestQueue = _next[0];

        for (var i = 1; i < _next.Length; i++)
        {
            var process = _next[i];
            if (process.Queue < minQueue)
            {
                minQueue = process.Queue;
                processWithSmallestQueue = process;
            }
        }

        return processWithSmallestQueue;
    }
}