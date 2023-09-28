#region

using System.Text;
using SystemModelling.SMO.Loggers;

#endregion

namespace SystemModelling.SMO.Elements;

public class Process : Element
{
    private int _failure;
    private int _maxQueue;
    private double _meanLoad;
    private double _meanQueue;
    private int _queue;

    private StringBuilder _sb = new();

    public Process(double delay, int maxQueue = Int32.MaxValue) : base(delay)
    {
        _queue = 0;
        _meanQueue = 0;

        _maxQueue = maxQueue;
    }

    public int Queue => _queue;

    public int MaxQueue
    {
        get => _maxQueue;
        set => _maxQueue = value;
    }

    public int Failure => _failure;

    public double MeanQueue => _meanQueue;

    public override void InAct()
    {
        base.InAct();

        if (CurrentState == State.Free)
        {
            CurrentState = State.Busy;
            TNext = TCurrent + GetDelay();
        }
        else
        {
            if (_queue < _maxQueue)
            {
                _queue += 1;
            }
            else
            {
                _failure++;
            }
        }
    }

    public override void OutAct()
    {
        base.OutAct();

        ActNextElementIfNeeded();
        TNext = double.MaxValue;
        CurrentState = State.Free;

        if (_queue > 0)
        {
            _queue--;
            CurrentState = State.Busy;
            TNext = TCurrent + GetDelay();
        }
    }

    private void ActNextElementIfNeeded()
    {
        Element? nextElement = TransitionOption?.Next;
        if (nextElement != null)
        {
            Console.WriteLine($"Process {Name} -> {nextElement.Name}");
            nextElement.InAct();
        }
        else
        {
            Console.WriteLine($"Process {Name} -> null");
        }
    }

    public override void PrintInfo(ILogger logger)
    {
        base.PrintInfo(logger);
        logger.Log($"failure = {_failure}");
    }

    public override void PrintResult(ILogger logger)
    {
        base.PrintResult(logger);
        _sb.Clear();
        
        _sb.AppendLine($"mean length of queue = {_meanQueue / TCurrent}");
        _sb.AppendLine($"mean load = {_meanLoad / TCurrent}");
        _sb.AppendLine($"failure probability = {(double)_failure / Quantity}");
        logger.Log(
            _sb.ToString());
        
        _sb.Clear();
    }

    public override void DoStatistics(double delta)
    {
        _meanQueue += _queue * delta;
        _meanLoad += (CurrentState == State.Busy ? 1 : 0) * delta;
    }

    // public static ProcessBuilder Create() => ProcessBuilder.New();
}