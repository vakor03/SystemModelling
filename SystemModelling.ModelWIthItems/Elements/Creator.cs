using SystemModelling.ModelWIthItems.Patients;

namespace SystemModelling.ModelWIthItems.Elements;

public partial class Creator : Element
{
    private readonly IPatientFactory _patientFactory = new PatientFactory();

    public Creator()
    {
    }

    public override void OutAct()
    {
        base.OutAct();

        Patient patient = _patientFactory.Create();
        patient.CreationTime = TCurrent;
        Next.GetNextElement(patient.PatientType).InAct(patient);

        TNext = GenerateTNext(patient);
    }

    public static FluentCreateBuilder New() => new();
}