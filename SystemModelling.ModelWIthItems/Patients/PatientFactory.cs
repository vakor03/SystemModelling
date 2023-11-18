namespace SystemModelling.ModelWIthItems.Patients;

public class PatientFactory : IPatientFactory
{
    private readonly double _maxValue = PatientsDict.PatientsStatsMap.Sum(p => p.Value.Probability);

    public Patient Create()
    {
        double randomValue = new Random().NextDouble() * _maxValue;
        
        double sum = 0;
        foreach (var patient in PatientsDict.PatientsStatsMap)
        {
            sum += patient.Value.Probability;
            if (randomValue <= sum)
            {
                return new Patient
                {
                    PatientType = patient.Key
                };
            }
        }
        
        throw new Exception("Something went wrong");
    }
}