using SystemModelling.ModelWIthItems.Elements;
using SystemModelling.Shared;

namespace SystemModelling.ModelWIthItems;

public class Model : IPrintResults
{
    public double SimulationTime { get; set; }
    public double TCurrent { get; private set; }
    
    private HashSet<Element> _elements = new();
    private ILogger _logger = new FileLogger("log.txt");
    
    public void AddElements(params Element[] elements)
    {
        _elements.UnionWith(elements);
    }
    
    public void Simulate()
    {
        while (TCurrent < SimulationTime)
        {
            TCurrent = FindNewTCurrent();

            UpdateElementsTCurrent();
            
            PerformOutAct();

            PrintElementsInfo();
        }
        
        PrintFinalResults();
    }

    private void PrintFinalResults()
    {
        PrintResults(_logger);

        foreach (var element in _elements)
        {
            element.PrintResults(_logger);
        }
    }

    public void PrintResults(ILogger logger)
    {
        // throw new NotImplementedException();
    }

    private void PrintElementsInfo()
    {
        foreach (var element in _elements)
        {
            element.PrintInfo(_logger);
        }
    }

    private double FindNewTCurrent()
    {
        return _elements.Min(e => e.TNext);
    }   
    
    private void PerformOutAct()
    {
        _logger.WriteLine("");
        var elements = _elements.Where(e => e.TNext == TCurrent);
        foreach (var element in elements)
        {
            element.OutAct();
            _logger.WriteLine($"{element.Name} OUT at {TCurrent}");
        }
    }

    private void UpdateElementsTCurrent()
    {
        foreach (var element in _elements)
        {
            element.UpdateTCurrent(TCurrent);
        }
    }
}