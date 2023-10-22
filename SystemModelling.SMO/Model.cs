using SystemModelling.Shared;
using SystemModelling.SMO.Elements;

namespace SystemModelling.SMO;

public class Model
{
    private Element _currentElement;
    private List<Element> _elements;
    private ILogger _logger;

    public ILogger Logger => _logger;

    private double _tCurrent;
    private double _tNext;
    private const double COMPARISON_TOLERANCE = 0.000001;

    public event Action<ILogger>? OnResultsPrinted;

    public Model(List<Element> elements)
    {
        _elements = elements;
        // _logger = new ConsoleLogger();
        _logger = new FileLogger("log.txt");
        _tNext = 0;
        _tCurrent = 0;
        
        foreach (var element in _elements)
        {
            element.Logger = _logger;
        }
    }

    public Model(params Element[] elements) : this(new List<Element>(elements))
    {
    }

    public void Simulate(double time)
    {
        while (_tCurrent < time)
        {
            _logger.WriteLine("");
            
            _tNext = FindSmallestTNext(out _currentElement);

            DoStatistics();
            
            _tCurrent = _tNext;

            UpdateTCurrentInAllElements();
            
            LogAndOutActElement(_currentElement);
            
            ActElementsWithCurrentTNext();

            PrintElementsInfo();
        }

        PrintResults();
        OnResultsPrinted?.Invoke(_logger);
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
        foreach (var element in _elements)
        {
            element.DoStatistics(_tNext - _tCurrent);
        }
    }
    
    private double CalculateMeanClientsInSystem()
    {
        return _elements.Sum(el => el.ClientTimeProcessing)/_tCurrent;
    }

    private void LogCurrentEvent(Element currentElement)
    {
        _logger.WriteLine($"It's time for event in {currentElement.Name}, time = {_tCurrent}");
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
            element.PrintInfo(_logger);
        }
    }

    public void PrintResults()
    {
        _logger.WriteLine("\n-------------RESULTS-------------");
        foreach (Element element in _elements)
        {
            element.PrintResult(_logger);
        }

        _logger.WriteLine($"Mean clients in system: {CalculateMeanClientsInSystem()}");
    }
}