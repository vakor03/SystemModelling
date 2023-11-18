namespace SystemModelling.ModelWIthItems.Patients;

public static class PatientsDict
{
    public static Dictionary<PatientType, PatientStats> PatientsStatsMap = new()
    {
        {
            PatientType.CompletedExaminationAndReferred,
            new PatientStats() { PatientType = PatientType.CompletedExaminationAndReferred, MeanTimeRegistration = 15, Probability = 0.5 }
        },
        {
            PatientType.IncompleteExaminationAdmission,
            new PatientStats() { PatientType = PatientType.IncompleteExaminationAdmission, MeanTimeRegistration = 40, Probability = 0.1 }
        },
        {
            PatientType.JustAdmittedForExamination,
            new PatientStats() { PatientType = PatientType.JustAdmittedForExamination, MeanTimeRegistration = 30, Probability = 0.4 }
        },
    };
}