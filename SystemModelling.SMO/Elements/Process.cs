using System.Text;
using SystemModelling.Shared;
using SystemModelling.SMO.Builders;

namespace SystemModelling.SMO.Elements;

public class Process : Element
{
    public int Queue { get; set; } = 0;
    public int MaxQueue { get; set; }
    public int Failure { get; private set; } = 0;

    private double _meanQueue;

    private readonly Subprocess[] _subprocesses;

    public Process(int subprocessesCount)
    {
        _subprocesses = CreateSubprocesses(subprocessesCount);
    }

    public override void InAct()
    {
        base.InAct();

        foreach (var subprocess in _subprocesses)
        {
            if (!subprocess.IsBusy)
            {
                SubprocessInAct(subprocess);
                UpdateState();
                return;
            }
        }

        if (Queue < MaxQueue)
        {
            Queue++;
        }
        else
        {
            Failure++;
        }

        UpdateState();
    }

    private void UpdateState()
    {
        CurrentState = Queue switch
        {
            0 => State.Free,
            _ => State.Busy
        };
    }

    public override void OutAct()
    {
        base.OutAct();

        foreach (var subprocess in _subprocesses)
        {
            if (subprocess.TNext <= TCurrent && subprocess.IsBusy)
            {
                SubprocessOutAct(subprocess);

                if (Queue > 0)
                {
                    Queue--;
                    SubprocessInAct(subprocess);
                }
            }
        }
        UpdateTNext();
        UpdateState();
    }

    private void SubprocessOutAct(Subprocess subprocess)
    {
        subprocess.IsBusy = false;
        subprocess.TNext = Double.MaxValue;
        Transition?.Next?.InAct();
    }

    private void SubprocessInAct(Subprocess subprocess)
    {
        subprocess.TNext = TCurrent + GetDelay();
        subprocess.IsBusy = true;

        UpdateTNext();
    }

    private void UpdateTNext()
    {
        TNext = _subprocesses.Min(sp => sp.TNext);
    }

    public override void DoStatistics(double delta)
    {
        _meanQueue += Queue * delta;
        foreach (var subprocess in _subprocesses)
        {
            subprocess.DoStatistics(delta);
        }
    }


    public override void PrintInfo(ILogger logger)
    {
        logger.WriteLine($"{Name} loaded={_subprocesses.Count(sp=>sp.IsBusy)}/{_subprocesses.Length}" +
                         $" queue={Queue} failured={Failure} quantity={Quantity} tNext={TNext}");
    }

    private readonly StringBuilder _sb = new();

    public override void PrintResult(ILogger logger)
    {
        base.PrintResult(logger);

        _sb.AppendLine($"Mean queue: {_meanQueue / TCurrent}");
        foreach (var subprocess in _subprocesses)
        {
            _sb.AppendLine($"\t{subprocess.Name} mean load: {subprocess.MeanLoad / TCurrent}");
        }

        _sb.AppendLine($"Failure probability: {(double)Failure / Quantity}");
        logger.WriteLine(_sb.ToString());

        _sb.Clear();
    }

    private Subprocess[] CreateSubprocesses(int subprocessesCount)
    {
        var subprocesses = new Subprocess[subprocessesCount];
        for (var i = 0; i < subprocessesCount; i++)
        {
            subprocesses[i] = new Subprocess
            {
                Name = $"subprocess {i}",
                TNext = Double.MaxValue,
                IsBusy = false
            };
        }

        return subprocesses;
    }

    public new static FluentProcessBuilder New() => new();
}