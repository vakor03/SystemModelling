using System.Text;
using SystemModelling.ModelWIthItems.DelayGenerators;
using SystemModelling.ModelWIthItems.NextElements;
using SystemModelling.ModelWIthItems.Patients;
using SystemModelling.Shared;

namespace SystemModelling.ModelWIthItems.Elements;

public partial class Element : IPrintResults
{
    protected StringBuilder _sb = new();
    public string Name { get; protected init; }
    public double TCurrent { get; protected set; }
    public double TNext { get; protected set; }
    // public Element? Next { get; set; }
    public INextElement Next { get; set; }
    public IDelayGenerator DelayGenerator { get; protected init; }
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
    
    public double GenerateTNext()
    {
        return TCurrent + DelayGenerator.GetDelay();
    }

    public void UpdateTCurrent(double tCurrent)
    {
        TCurrent = tCurrent;
    }

    public virtual void PrintInfo(ILogger logger)
    {
        _sb.Clear();
        _sb.AppendLine($"{Name}:");
        _sb.Append($"\tTCurrent: {TCurrent}");
        _sb.Append($"\tTNext: {TNext}");
        _sb.AppendLine();
        _sb.Append($"\tEntered: {PatientsEntered}");
        _sb.Append($"\tProcessed: {PatientsProcessed}");
        _sb.Append($"\tFailures: {Failures}");
        
        logger.WriteLine(_sb.ToString());
        _sb.Clear();
    }

    public void PrintResults(ILogger logger)
    {
    }
}