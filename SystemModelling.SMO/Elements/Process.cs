#region

using System.Text;
using SystemModelling.Shared;
using SystemModelling.SMO.Builders;

#endregion

namespace SystemModelling.SMO.Elements;

public class Process : Element
{
    private readonly StringBuilder _sb = new();

    private readonly Subprocess[] _subprocesses;
    private int _failure = 0;

    private double _meanQueue;

    public Process(int subprocessesCount)
    {
        _subprocesses = CreateSubprocesses(subprocessesCount);
    }

    public int Queue { get; set; } = 0;

    public int MaxQueue { get; set; }

    private int Failure
    {
        get => _failure;
        set
        {
            _failure = value;
            OnFailure?.Invoke();
        }
    }

    public int SubprocessesCount => _subprocesses.Length;

    public event Action? OnEnter;
    public event Action? OnSuccess;
    public event Action? OnFailure;
    public event Action? OnQueueChanged;

    public override void InAct()
    {
        base.InAct();
        OnEnter?.Invoke();

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
            OnQueueChanged?.Invoke();
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
        OnSuccess?.Invoke();

        foreach (var subprocess in _subprocesses)
        {
            if (subprocess.TNext <= TCurrent && subprocess.IsBusy)
            {
                SubprocessOutAct(subprocess);

                if (Queue > 0)
                {
                    Queue--;
                    OnQueueChanged?.Invoke();
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
        
        
        PerformTransitionToNext();
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
        
        ClientTimeProcessing += (_subprocesses.Count(sp=>sp.IsBusy) + Queue) * delta;
    }


    public override void PrintInfo(ILogger logger)
    {
        logger.WriteLine($"{Name} loaded={_subprocesses.Count(sp => sp.IsBusy)}/{_subprocesses.Length}" +
                         $" queue={Queue} failured={Failure} quantity={Quantity} tNext={TNext}");
    }

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