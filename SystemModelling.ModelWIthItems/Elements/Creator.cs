using SystemModelling.ModelWIthItems.Patients;

namespace SystemModelling.ModelWIthItems.Elements;

public partial class Creator : Element
{
    private readonly IPatientFactory _patientFactory = new PatientFactory();

    private Creator()
    {
    }

    public override void OutAct()
    {
        base.OutAct();

        Patient patient = _patientFactory.Create();
        Next.GetNextElement(patient.PatientType)?.InAct(patient);

        TNext = GenerateTNext();
    }

    public static FluentCreateBuilder New() => new();
}