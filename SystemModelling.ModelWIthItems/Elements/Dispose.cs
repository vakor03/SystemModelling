using SystemModelling.ModelWIthItems.Patients;
using SystemModelling.Shared;

namespace SystemModelling.ModelWIthItems.Elements;

public class Dispose : Element
{
    private double _totalTimeInSystem;
    public Dispose()
    {
        Name = "Dispose";
        TNext = double.MaxValue;
    }

    public override void InAct(Patient patient)
    {
        base.InAct(patient);
        
        _totalTimeInSystem += TCurrent - patient.CreationTime;
    }

    public override void PrintResults(ILogger logger)
    {
        base.PrintResults(logger);
        logger.WriteLine($"Average time in system: {_totalTimeInSystem / PatientsEntered}");
    }

    public override void OutAct()
    {
        throw new Exception("Dispose element can't be out");
    }
}