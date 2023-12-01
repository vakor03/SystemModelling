using SystemModelling.ModelWIthItems.Elements;
using SystemModelling.SMO.Loggers;

namespace SystemModelling.ModelWIthItems;

public class Model : IPrintResults
{
    public double SimulationTime { get; set; }
    public double TCurrent { get; private set; }

    private HashSet<Element> _elements = new();
    public ILogger Logger { get; set; }
    private ILogger _infoLogger;

    public bool PrintInfo
    {
        set
        {
            if (value)
            {
                _infoLogger = Logger;
            }
            else
            {
                _infoLogger = new NullLogger();
            }
        }
    }

    public Model()
    {
        Logger = new FileLogger("log.txt");
        PrintInfo = false;
    }


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
        PrintResults(Logger);

        foreach (var element in _elements)
        {
            element.PrintResults(Logger);
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
            element.PrintInfo(_infoLogger);
        }
    }

    private double FindNewTCurrent()
    {
        return _elements.Min(e => e.TNext);
    }

    private void PerformOutAct()
    {
        _infoLogger.WriteLine("");
        var elements = _elements.Where(e => e.TNext == TCurrent);
        foreach (var element in elements)
        {
            element.OutAct();
            _infoLogger.WriteLine($"{element.Name} OUT at {TCurrent}");
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