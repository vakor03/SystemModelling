using SystemModelling.ModelWIthItems.DelayGenerators;
using SystemModelling.ModelWIthItems.Patients;

namespace SystemModelling.ModelWIthItems.Elements;

public static class Extensions
{
    public static SimpleDelayGenerator<Patient> ToDelayGenerator(this IRandomValueProvider randomValueProvider)
    {
        return new(randomValueProvider);
    }
}