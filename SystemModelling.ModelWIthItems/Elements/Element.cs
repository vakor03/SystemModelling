using System.Text;
using SystemModelling.ModelWIthItems.DelayGenerators;
using SystemModelling.ModelWIthItems.NextElements;
using SystemModelling.ModelWIthItems.Patients;
using SystemModelling.SMO.Loggers;

namespace SystemModelling.ModelWIthItems.Elements;

public partial class Element : IPrintResults
{
    protected StringBuilder _sb = new();

    public string Name { get; init; }
    public double TCurrent { get; protected set; }
    public double TNext { get; set; }
    public INextElement Next { get; set; }
    public IDelayGenerator<Patient> DelayGenerator { get; init; }
    public int PatientsEntered { get; protected set; }
    public int PatientsProcessed { get; protected set; }
    public int Failures { get;protected set; }

    
    protected Element()
    {
    }

    public virtual void InAct(Patient patient)
    {
        PatientsEntered++;
    }
    
    public virtual void OutAct()
    {
        PatientsProcessed++;
    }
    
    protected void OutActFromSystem(Patient patient)
    {
    }
    
    public double GenerateTNext(Patient patient)
    {
        return TCurrent + DelayGenerator.GetDelay(patient);
    }

    public void UpdateTCurrent(double tCurrent)
    {
        TCurrent = tCurrent;
    }

    public virtual void PrintInfo(ILogger logger)
    {
        _sb.Clear();
        _sb.AppendLine($"{Name}:");
        _sb.Append($"\tTNext: {TNext}");
        _sb.AppendLine();
        _sb.Append($"\tEntered: {PatientsEntered}");
        _sb.Append($"\tProcessed: {PatientsProcessed}");
        _sb.Append($"\tFailures: {Failures}");
        
        logger.WriteLine(_sb.ToString());
        _sb.Clear();
    }

    public virtual void PrintResults(ILogger logger)
    {
    }
}