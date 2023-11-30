using SystemModelling.ModelWIthItems.DelayGenerators;
using SystemModelling.ModelWIthItems.Patients;
using SystemModelling.ModelWIthItems.RandomValuesProviders;

namespace SystemModelling.ModelWIthItems;

public static class Extensions
{
    public static SimpleDelayGenerator<Patient> ToDelayGenerator(this IRandomValueProvider randomValueProvider)
    {
        return new(randomValueProvider);
    }
}