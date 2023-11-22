using SystemModelling.Shared;
using SystemModelling.SMO.Elements;

namespace SystemModelling.SMO;

public class Model
{
    private Element _currentElement;
    private List<Element> _elements;
    // private ILogger _logger { get; set; } = new NullLogger()

    public ILogger Logger { get; set; } = new NullLogger();

    private double _tCurrent;
    private double _tNext;
    private const double COMPARISON_TOLERANCE = 0.000001;

    public event Action<ILogger>? OnResultsPrinted;
    public bool DisableLogging { get; set; } = false;

    public Model(List<Element> elements)
    {
        _elements = elements;
        // _logger = new ConsoleLogger();
        // _logger = new FileLogger("log.txt");
        _tNext = 0;
        _tCurrent = 0;

        foreach (var element in _elements)
        {
            element.Logger = Logger;
        }
    }

    public Model(params Element[] elements) : this(new List<Element>(elements))
    {
    }

    public void Simulate(double time)
    {
        while (_tCurrent < time)
        {
            if (!DisableLogging)
                Logger.WriteLine("");

            _tNext = FindSmallestTNext(out _currentElement);

            DoStatistics();

            _tCurrent = _tNext;

            UpdateTCurrentInAllElements();

            LogAndOutActElement(_currentElement);

            ActElementsWithCurrentTNext();

            if (!DisableLogging)
                PrintElementsInfo();
        }

        if (!DisableLogging)
            PrintResults();
        OnResultsPrinted?.Invoke(Logger);
    }

    private void ActElementsWithCurrentTNext()
    {
        foreach (var element in _elements)
        {
            if (Math.Abs(element.TNext - _tCurrent) < COMPARISON_TOLERANCE)
            {
                LogAndOutActElement(element);
            }
        }
    }

    private void LogAndOutActElement(Element element)
    {
        LogCurrentEvent(element);

        element.OutAct();
        // element.LogTransition(_logger);
    }

    private void UpdateTCurrentInAllElements()
    {
        foreach (var element in _elements)
        {
            element.TCurrent = _tCurrent;
        }
    }

    private void DoStatistics()
    {
        for (var i = 0; i < _elements.Count; i++)
        {
            _elements[i].DoStatistics(_tNext - _tCurrent);
        }
    }

    private double CalculateMeanClientsInSystem()
    {
        return _elements.Sum(el => el.ClientTimeProcessing) / _tCurrent;
    }

    private double CalculateMeanTimeInSystem()
    {
        return _elements.Sum(el => el.ClientTimeProcessing) /
               _elements.Where(el => el is Create).Sum(el => el.Quantity);
    }

    private void LogCurrentEvent(Element currentElement)
    {
        if (!DisableLogging)
            Logger.WriteLine($"It's time for event in {currentElement.Name}, time = {_tCurrent}");
    }

    private double FindSmallestTNext(out Element correspondingElement)
    {
        double result = Double.MaxValue;
        correspondingElement = null;

        foreach (var element in _elements)
        {
            if (element.TNext < result)
            {
                result = element.TNext;
                correspondingElement = element;
            }
        }

        return result;
    }

    public void PrintElementsInfo()
    {
        foreach (var element in _elements)
        {
            element.PrintInfo(Logger);
        }
    }

    public void PrintResults()
    {
        Logger.WriteLine("\n-------------RESULTS-------------");
        foreach (Element element in _elements)
        {
            element.PrintResult(Logger);
        }

        Logger.WriteLine($"Mean clients in system: {CalculateMeanClientsInSystem()}");
        Logger.WriteLine($"Mean time in system: {CalculateMeanTimeInSystem()}");
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
}