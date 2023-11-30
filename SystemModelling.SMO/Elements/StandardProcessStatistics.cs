using System.Text;
using SystemModelling.SMO.Loggers;

namespace SystemModelling.SMO.Elements;

public class StandardProcessStatistics : IProcessStatistics
{
    public double ClientTimeProcessing { get; set; }
    public double MeanQueue => _meanQueue / _process.TCurrent;
    public double MeanLoad => _process.Subprocesses.Sum(s => s.MeanLoad) / _process.TCurrent;
    public int Failure => _process.Failure;
    public int OutQuantity => _process.OutQuantity;
    public int InQuantity => _process.InQuantity;

    private double _meanQueue;
    private IProcess _process;
    private StringBuilder _sb = new();

    public void Init(IProcess process)
    {
        _process = process;
    }

    public void DoStatistics(double delta)
    {
        _meanQueue += _process.Queue * delta;
        foreach (var subprocess in _process.Subprocesses)
        {
            subprocess.DoStatistics(delta);
        }

        ClientTimeProcessing += (_process.BusySubprocessesCount + _process.Queue) * delta;
    }

    public void PrintResult(ILogger logger)
    {
        _sb.Clear();
        _sb.AppendLine($"\tItems processed: {_process.OutQuantity}");
        _sb.AppendLine($"\tMean queue: {_meanQueue / _process.TCurrent}");

        int i = 0;
        if (_process.Subprocesses.Count() > 1)
        {
            foreach (var subprocess in _process.Subprocesses)
            {
                _sb.AppendLine($"\t\tSubprocess {i++} mean load: {subprocess.MeanLoad / _process.TCurrent}");
            }
        }
        else
        {
            _sb.AppendLine($"\tMean load: {(_process.Subprocesses.First().MeanLoad / _process.TCurrent)}");
        }

        _sb.AppendLine(
            $"\tFailure probability: {(double)_process.Failure / (_process.OutQuantity + _process.Failure)}");


        logger.WriteLine(_sb.ToString());
    }
}