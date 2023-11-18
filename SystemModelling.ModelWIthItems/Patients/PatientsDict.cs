namespace SystemModelling.ModelWIthItems.Patients;

public static class PatientsDict
{
    public static Dictionary<PatientType, PatientStats> PatientsStatsMap = new()
    {
        {
            PatientType.Type1,
            new PatientStats() { PatientType = PatientType.Type1, MeanTimeRegistration = 15, Probability = 0.5 }
        },
        {
            PatientType.Type2,
            new PatientStats() { PatientType = PatientType.Type2, MeanTimeRegistration = 40, Probability = 0.1 }
        },
        {
            PatientType.Type3,
            new PatientStats() { PatientType = PatientType.Type3, MeanTimeRegistration = 30, Probability = 0.4 }
        },
    };
}