using SystemModelling.ModelWIthItems.Elements;
using SystemModelling.ModelWIthItems.Patients;
using SystemModelling.Shared;

namespace SystemModelling.ModelWIthItems;

public class Laboratory : Processor
{
    private double _previousInActTime = -1;
    private double _totalTimeBetweenInActs;

    public Laboratory(int subprocessesCount) : base(subprocessesCount)
    {
    }

    public override void InAct(Patient patient)
    {
        CalculateTimeBetweenInActs();
        base.InAct(patient);
    }

    private void CalculateTimeBetweenInActs()
    {
        if (_previousInActTime != -1)
        {
            _totalTimeBetweenInActs += TCurrent - _previousInActTime;
        }

        _previousInActTime = TCurrent;
    }

    public override void PrintResults(ILogger logger)
    {
        base.PrintResults(logger);
        if (PatientsEntered <= 1)
        {
            logger.WriteLine("Not enough data to calculate average time between in acts");
        }
        else
        {
            logger.WriteLine($"Average time between in acts: {_totalTimeBetweenInActs / (PatientsEntered - 1)}");
        }
    }
}