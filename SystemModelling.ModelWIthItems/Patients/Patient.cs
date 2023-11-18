namespace SystemModelling.ModelWIthItems.Patients;

public class PatientStats
{
    public PatientType PatientType { get; set; }
    public double MeanTimeRegistration { get; set; }
    public double Probability { get; set; }
}

public class Patient
{
    public PatientType PatientType { get; set; }
}