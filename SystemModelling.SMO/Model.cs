using SystemModelling.SMO.Elements;
using SystemModelling.SMO.Loggers;

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
        _logger = new ConsoleLogger();
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

            LogCurrentEvent();

            DoAllElementsStatistics();

            _tCurrent = _tNext;

            UpdateTCurrentInAllElements();

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

    private void LogCurrentEvent()
    {
        _logger.WriteLine($"\nIt's time for event in {_currentElement.Name}, time = {_tNext}");
    }

    private double FindSmallestTNext(out Element correspondingElement)
    {
        double result = Double.MaxValue;
        correspondingElement = null;

        foreach (var element1 in _elements)
        {
            if (element1.TNext < result)
            {
                result = element1.TNext;
                correspondingElement = element1;
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