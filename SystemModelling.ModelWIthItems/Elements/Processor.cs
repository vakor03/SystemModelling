using SystemModelling.ModelWIthItems.Collections;
using SystemModelling.ModelWIthItems.Patients;
using SystemModelling.SMO.Loggers;

namespace SystemModelling.ModelWIthItems.Elements;

public partial class Processor : Element
{
    public IQueue<Patient> PatientsQueue { get; set; }

    private Subprocess[] _subprocesses;
    private int FreeSubprocessesCount => _subprocesses.Count(s => !s.IsBusy);

    public Processor(int subprocessesCount)
    {
        _subprocesses = GenerateSubprocesses(subprocessesCount);
        PatientsQueue = new SimpleQueue<Patient>(Int32.MaxValue);
    }
    
    private Subprocess[] GenerateSubprocesses(int subprocessesCount)
    {
        var subprocesses = new Subprocess[subprocessesCount];
        for (int i = 0; i < subprocessesCount; i++)
        {
            subprocesses[i] = new Subprocess
            {
                TNext = Double.MaxValue
            };
        }

        return subprocesses;
    }

    public override void InAct(Patient patient)
    {
        base.InAct(patient);

        if (HasFreeSubprocesses())
        {
            var subprocess = _subprocesses.First(s => !s.IsBusy);
            SubprocessInAct(subprocess, patient);
        }
        else if (!PatientsQueue.IsFull())
        {
            PatientsQueue.Add(patient);
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

        if (!PatientsQueue.IsEmpty())
        {
            var nextPatient = PatientsQueue.Get()!;
            SubprocessInAct(_subprocesses.First(sp => !sp.IsBusy), nextPatient);
        }
    }

    public override void PrintInfo(ILogger logger)
    {
        base.PrintInfo(logger);
        logger.WriteLine($"\tQueue: {PatientsQueue.QueueLength}/{PatientsQueue.MaxQueue}\tSubprocesses:{_subprocesses.Count(s => s.IsBusy)}/{_subprocesses.Length}");
    }

    public static FluentProcessBuilder New() => new();
}