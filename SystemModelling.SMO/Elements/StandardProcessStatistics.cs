using System.Text;
using SystemModelling.Shared;

namespace SystemModelling.SMO.Elements;

public class StandardProcessStatistics : IProcessStatistics
{
    public double ClientTimeProcessing { get; set; }

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
        _sb.AppendLine($"Mean queue: {_meanQueue / _process.TCurrent}");
        
        int i = 0;
        foreach (var subprocess in _process.Subprocesses)
        {
            _sb.AppendLine($"\tSubprocess {i++} mean load: {subprocess.MeanLoad / _process.TCurrent}");
        }
        _sb.AppendLine($"Failure probability: {(double)_process.Failure / _process.Quantity}");

        
        logger.WriteLine(_sb.ToString());
    }
}