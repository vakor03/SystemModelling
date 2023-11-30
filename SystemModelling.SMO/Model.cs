using SystemModelling.SMO.Elements;
using SystemModelling.SMO.Loggers;

namespace SystemModelling.SMO;

public class Model
{
    private List<IElement> _elements;
    public ILogger Logger { get; set; } = new NullLogger();

    private double _tCurrent = 0;
    private double _tNext = 0;
    private const double COMPARISON_TOLERANCE = 0.000001;

    public event Action<ILogger>? OnResultsPrinted;
    public bool DisableLogging { get; set; } = false;

    public Model(List<IElement> elements)
    {
        _elements = elements;
    }

    public Model(params IElement[] elements) : this(new List<IElement>(elements))
    {
    }

    public void Simulate(double time)
    {
        while (_tCurrent < time)
        {
            if (!DisableLogging)
                Logger.WriteLine("");

            _tNext = FindSmallestTNext();

            DoStatistics();

            UpdateTCurrent(_tNext);
            
            OutActRequiredElements();

            if (!DisableLogging)
                PrintElementsInfo();
        }

        if (!DisableLogging)
            PrintResults();
        OnResultsPrinted?.Invoke(Logger);
    }

    private void UpdateTCurrent(double newTCurrent)
    {
        _tCurrent = _tNext;

        UpdateTCurrentInElements();
    }

    private double FindSmallestTNext()
    {
        double result = Double.MaxValue;
    
        foreach (var element in _elements)
        {
            if (element.TNext < result)
            {
                result = element.TNext;
            }
        }
    
        return result;
    }
    
    private void UpdateTCurrentInElements()
    {
        foreach (var element in _elements)
        {
            element.TCurrent = _tCurrent;
        }
    }

    private void OutActRequiredElements()
    {
        foreach (var element in _elements)
        {
            if (Math.Abs(element.TNext - _tCurrent) < COMPARISON_TOLERANCE)
            {
                LogElementOutAct(element);
                OutActElement(element);
            }
        }
    }
    private void OutActElement(IElement element)
    {
        element.OutAct();
    }
    
    public void Reset()
    {
        _tNext = 0;
        _tCurrent = 0;
        
        foreach (var element in _elements)
        {
            element.Reset();
        }
    }

    private void LogElementOutAct(IElement element)
    {
    }
    //
    //
    private void DoStatistics()
    {
        for (var i = 0; i < _elements.Count; i++)
        {
            _elements[i].DoStatistics(_tNext - _tCurrent);
        }
    }
    //
    // private double CalculateMeanClientsInSystem()
    // {
    //     return _elements.Sum(el => el.ClientTimeProcessing) / _tCurrent;
    // }
    //
    // private double CalculateMeanTimeInSystem()
    // {
    //     return _elements.Sum(el => el.ClientTimeProcessing) /
    //            _elements.Where(el => el is Create).Sum(el => el.Quantity);
    // }
    //
    // private void LogCurrentEvent(Element currentElement)
    // {
    //     if (!DisableLogging)
    //         Logger.WriteLine($"It's time for event in {currentElement.Name}, time = {_tCurrent}");
    // }
    //
    //
    public void PrintElementsInfo()
    {
        Logger.WriteLine("\n-------------INFO-------------");
        Logger.WriteLine($"tCurrent = {_tCurrent}");
        foreach (var element in _elements)
        {
            element.PrintInfo(Logger);
        }
    }
    //
    public void PrintResults()
    {
        Logger.WriteLine("\n-------------RESULTS-------------");
        foreach (IElement element in _elements)
        {
            element.PrintResult(Logger);
        }
    
        // Logger.WriteLine($"Mean clients in system: {CalculateMeanClientsInSystem()}");
        // Logger.WriteLine($"Mean time in system: {CalculateMeanTimeInSystem()}");
    }
    //
}