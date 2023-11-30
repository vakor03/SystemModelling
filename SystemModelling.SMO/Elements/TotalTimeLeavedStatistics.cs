using System.Text;
using SystemModelling.SMO.Loggers;

namespace SystemModelling.SMO.Elements;

public class TotalTimeLeavedStatistics : IProcessStatistics
{
    private readonly StringBuilder _sb = new();
    private IProcess _process;
    private double TotalTimeLeave { get; set; }
    private double PrevTimeLeave { get; set; }
    public void Init(IProcess process)
    {
        _process = process;
        process.OnOutAct += UpdateTotalTimeLeaved;
    }

    public void DoStatistics(double delta)
    {
        // no-op
    }
    
    private void UpdateTotalTimeLeaved()
    {
        if (PrevTimeLeave != 0)
        {
            TotalTimeLeave += _process.TCurrent - PrevTimeLeave;
        }

        PrevTimeLeave = _process.TCurrent;
    }

    public void PrintResult(ILogger logger)
    {
        _sb.Clear();
        _sb.AppendLine($"Mean time between leaving: {TotalTimeLeave / _process.OutQuantity}");
        logger.WriteLine(_sb.ToString());
    }
}