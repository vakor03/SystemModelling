using SystemModelling.ModelWIthItems.Collections;
using SystemModelling.ModelWIthItems.Patients;
using SystemModelling.Shared;

namespace SystemModelling.ModelWIthItems.Elements;

public partial class Processor : Element
{
    private IQueue<Patient> _patientsQueue;

    private Subprocess[] _subprocesses;
    private int FreeSubprocessesCount => _subprocesses.Count(s => !s.IsBusy);

    public Processor()
    {
    }

    public override void InAct(Patient patient)
    {
        base.InAct(patient);

        if (HasFreeSubprocesses())
        {
            var subprocess = _subprocesses.First(s => !s.IsBusy);
            SubprocessInAct(subprocess, patient);
        }
        else if (!_patientsQueue.IsFull())
        {
            _patientsQueue.Add(patient);
        }
        else
        {
            Failures++;
        }

        bool HasFreeSubprocesses()
        {
            return FreeSubprocessesCount > 0;
        }
    }

    private void RecalculateTNext()
    {
        TNext = _subprocesses.Min(s => s.TNext);
    }

    private void SubprocessInAct(Subprocess subprocess, Patient patient)
    {
        subprocess.IsBusy = true;
        subprocess.TNext = GenerateTNext(patient);
        subprocess.CurrentPatient = patient;
        RecalculateTNext();
    }
public event Action<Patient> OnAfterOutAct; 
    private void SubprocessOutAct(Subprocess subprocess)
    {
        Patient currentPatient = subprocess.CurrentPatient!;
        Next.GetNextElement(currentPatient.PatientType).InAct(currentPatient);
        OnAfterOutAct?.Invoke(currentPatient);

        subprocess.IsBusy = false;
        subprocess.TNext = Double.MaxValue;
        subprocess.CurrentPatient = null;
    }

    public override void OutAct()
    {
        var subprocess = _subprocesses.Where(sp => sp.TNext == TCurrent);

        foreach (var sp in subprocess)
        {
            base.OutAct();
            SubprocessOutAct(sp);
        }

        RecalculateTNext();

        if (!_patientsQueue.IsEmpty())
        {
            var nextPatient = _patientsQueue.Get()!;
            SubprocessInAct(_subprocesses.First(sp => !sp.IsBusy), nextPatient);
        }
    }

    public override void PrintInfo(ILogger logger)
    {
        base.PrintInfo(logger);
        logger.WriteLine($"\tQueue: {_patientsQueue.QueueLength}/{_patientsQueue.MaxQueue}\tSubprocesses:{_subprocesses.Count(s => s.IsBusy)}/{_subprocesses.Length}");
    }

    public static FluentProcessBuilder New() => new();
}