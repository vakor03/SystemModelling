using SystemModelling.ModelWIthItems.Patients;
using SystemModelling.ModelWIthItems.RandomValuesProviders;

namespace SystemModelling.ModelWIthItems.DelayGenerators;

public class MultipleDelayGenerator : IDelayGenerator<Patient>
{
    private readonly Dictionary<PatientType, IRandomValueProvider> _randomValueProviders;

    public MultipleDelayGenerator(Dictionary<PatientType, IRandomValueProvider> randomValueProviders)
    {
        _randomValueProviders = randomValueProviders;
    }

    public double GetDelay(Patient item)
    {
        return _randomValueProviders[item.PatientType].GetRandomValue();
    }
}