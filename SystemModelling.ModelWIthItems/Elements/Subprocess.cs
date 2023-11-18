using SystemModelling.ModelWIthItems.Patients;

namespace SystemModelling.ModelWIthItems.Elements;

public class Subprocess
{
    public bool IsBusy { get; set; }
    public double TNext { get; set; }
    public Patient? CurrentPatient { get; set; }
}