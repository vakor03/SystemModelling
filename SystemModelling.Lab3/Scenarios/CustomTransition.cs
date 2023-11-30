using SystemModelling.SMO.Elements;
using SystemModelling.SMO.Transitions;

namespace SystemModelling.Lab3.Scenarios;

internal class CustomTransition : ITransition
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