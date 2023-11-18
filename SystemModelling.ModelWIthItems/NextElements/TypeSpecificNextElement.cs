using SystemModelling.ModelWIthItems.Elements;
using SystemModelling.ModelWIthItems.Patients;

namespace SystemModelling.ModelWIthItems.NextElements;

public class TypeSpecificNextElement : INextElement
{
    private Dictionary<PatientType, Element?> _nextElements = new();
    
    public void AddNextElement(PatientType patientType, Element? nextElement)
    {
        _nextElements.Add(patientType, nextElement);
    }
    public Element? GetNextElement(PatientType patientType)
    {
        if (_nextElements.TryGetValue(patientType, out var element))
        {
            return element;
        }

        return null;
    }
}