using SystemModelling.Shared;
using SystemModelling.SMO.Elements;

namespace SystemModelling.SMO;

public class Model
{
    private Element _currentElement;
    private List<Element> _elements;
    private ILogger _logger;
    private double _tCurrent;
    private double _tNext;
    private const double COMPARISON_TOLERANCE = 0.000001;

    public Model(List<Element> elements)
    {
        _elements = elements;
        // _logger = new ConsoleLogger();
        _logger = new FileLogger("log.txt");
        _tNext = 0;
        _tCurrent = 0;
    }

    public Model(params Element[] elements) : this(new List<Element>(elements))
    {
    }

    public void Simulate(double time)
    {
        while (_tCurrent < time)
        {
            _tNext = FindSmallestTNext(out _currentElement);
            
            DoAllElementsStatistics();

            _tCurrent = _tNext;
            // LogCurrentEvent();
            
            UpdateTCurrentInAllElements();
            
            _logger.WriteLine("");
            ActElementsWithCurrentTNext();

            PrintElementsInfo();
        }

        PrintResults();
    }

    private void ActElementsWithCurrentTNext()
    {
        foreach (var element in _elements)
        {
            if (Math.Abs(element.TNext - _tCurrent) < COMPARISON_TOLERANCE)
            {
                element.OutAct();
                LogCurrentEvent(element);
                element.LogTransition(_logger);
            }
        }
    }

    private void UpdateTCurrentInAllElements()
    {
        foreach (var element in _elements)
        {
            element.TCurrent = _tCurrent;
        }
    }

    private void DoAllElementsStatistics()
    {
        foreach (var element in _elements)
        {
            element.DoStatistics(_tNext - _tCurrent);
        }
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
    }
}