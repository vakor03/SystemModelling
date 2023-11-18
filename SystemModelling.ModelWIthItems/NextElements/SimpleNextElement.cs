using SystemModelling.ModelWIthItems.Elements;
using SystemModelling.ModelWIthItems.Patients;

namespace SystemModelling.ModelWIthItems.NextElements;

public class SimpleNextElement : INextElement
{
    public Element? Next { get; set; }

    public Element? GetNextElement(PatientType patientType)
    {
        return Next;
    }

    public static explicit operator SimpleNextElement(Element element)
    {
        return new SimpleNextElement { Next = element };
    }
}