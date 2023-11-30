#region

using SystemModelling.SMO.Loggers;

#endregion

namespace SystemModelling.SMO.Elements;

public class Process : Element, IProcess
{
    private readonly Subprocess[] _subprocesses;
    public IEnumerable<Subprocess> Subprocesses => _subprocesses;
    private HashSet<IProcessStatistics> _statistics = new();

    public Process(int subprocessesCount = 1)
    {
        _subprocesses = CreateSubprocesses(subprocessesCount);
    }

    public int Queue { get; set; } = 0;

    public int MaxQueue { get; set; }

    public int Quantity { get; }
    public int Failure { get; private set; } = 0;

    public event Action OnOutAct;
    public event Action OnQueueChange;

    public override void InAct()
    {
        base.InAct();

        foreach (var subprocess in _subprocesses)
        {
            if (!subprocess.IsBusy)
            {
                SubprocessInAct(subprocess);
                return;
            }
        }

        if (Queue < MaxQueue)
        {
            Queue++;
            OnQueueChange?.Invoke();
        }
        else
        {
            Failure++;
        }
    }

    public override void OutAct()
    {
        base.OutAct();
        OnOutAct?.Invoke();

        foreach (var subprocess in _subprocesses)
        {
            if (subprocess.TNext <= TCurrent && subprocess.IsBusy)
            {
                SubprocessOutAct(subprocess);

                if (Queue > 0)
                {
                    SubprocessInAct(subprocess);
                    Queue--;
                    OnQueueChange?.Invoke();
                }
            }
        }

        UpdateTNext();
    }


    public int BusySubprocessesCount { get; private set; } = 0;

    private void SubprocessOutAct(Subprocess subprocess)
    {
        subprocess.IsBusy = false;
        subprocess.TNext = Double.MaxValue;
        BusySubprocessesCount--;

        PerformTransitionToNext();
    }

    private void SubprocessInAct(Subprocess subprocess)
    {
        subprocess.TNext = TCurrent + DelayGenerator.GetDelay();
        subprocess.IsBusy = true;
        BusySubprocessesCount++;

        UpdateTNext();
    }

    private void UpdateTNext()
    {
        TNext = _subprocesses.Min(sp => sp.TNext);
    }

    public void AddStatistics(IProcessStatistics statistics)
    {
        statistics.Init(this);
        _statistics.Add(statistics);
    }

    public override void DoStatistics(double delta)
    {
        foreach (var processStatistics in _statistics)
        {
            processStatistics.DoStatistics(delta);
        }
    }

    public override void PrintInfo(ILogger logger)
    {
        logger.WriteLine(
            $"{Name} loaded={_subprocesses.Count(sp => sp.IsBusy)}/{_subprocesses.Length} queue={Queue} failured={Failure} quantity={OutQuantity} tNext={TNext}");
    }

    public override void PrintResult(ILogger logger)
    {
        logger.WriteLine($"{Name}");
        foreach (var processStatistics in _statistics)
        {
            processStatistics.PrintResult(logger);
        }
    }

    private Subprocess[] CreateSubprocesses(int subprocessesCount)
    {
        var subprocesses = new Subprocess[subprocessesCount];
        for (var i = 0; i < subprocessesCount; i++)
        {
            subprocesses[i] = new Subprocess
            {
                TNext = Double.MaxValue,
                IsBusy = false
            };
        }

        return subprocesses;
    }
}