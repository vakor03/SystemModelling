using SystemModelling.ModelWIthItems.Elements;
using SystemModelling.ModelWIthItems.Patients;

namespace SystemModelling.ModelWIthItems.NextElements;

public interface INextElement
{
    Element? GetNextElement(PatientType patientType);
}