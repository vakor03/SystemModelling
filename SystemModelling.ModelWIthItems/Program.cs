using SystemModelling.ModelWIthItems;
using SystemModelling.ModelWIthItems.Collections;
using SystemModelling.ModelWIthItems.DelayGenerators;
using SystemModelling.ModelWIthItems.Elements;
using SystemModelling.ModelWIthItems.NextElements;
using SystemModelling.ModelWIthItems.Patients;

var model = CreateHospitalModel();

model.SimulationTime = 10000;

model.Simulate();

static Model CreateHospitalModel()
{
    const int dutyDoctorsCount = 2;
    const int suprovidniyCount = 3;
    const int laborantsCount = 2;
    const int coridorCount = 100;

    const double creatorDelayMean = 15;

    Creator creator = Creator.New()
        .WithName("Creator")
        .WithDelayGenerator(new Exponential(creatorDelayMean).ToDelayGenerator())
        .Build();

    Dispose dispose = new Dispose();

    Processor prReception = Processor.New()
        .WithName("Reception")
        .WithProcessesCount(dutyDoctorsCount)
        .WithQueue(new PatientsPriorityQueue(int.MaxValue))
        .WithDelayGenerator(new MultipleDelayGenerator(new Dictionary<PatientType, IRandomValueProvider>()
        {
            { PatientType.Type1, new Exponential(15) },
            { PatientType.Type2, new Exponential(40) },
            { PatientType.Type3, new Exponential(30) }
        }))
        .Build();

    Processor prWayToChamber = Processor.New()
        .WithName("To Chamber")
        .WithProcessesCount(suprovidniyCount)
        .WithDelayGenerator(new Uniform(3, 8).ToDelayGenerator())
        .Build();

    Processor prWayToLaboratory = Processor.New()
        .WithName("To Laboratory")
        .WithProcessesCount(coridorCount)
        .WithDelayGenerator(new Uniform(2, 5).ToDelayGenerator())
        .Build();

    Processor prLabRegistry = Processor.New()
        .WithName("Lab Registry")
        .WithDelayGenerator(new Erlang(4.5, 3).ToDelayGenerator())
        .Build();

    Processor prLaboratory = Processor.New()
        .WithName("Laboratory")
        .WithProcessesCount(laborantsCount)
        .WithDelayGenerator(new Erlang(4, 2).ToDelayGenerator())
        .Build();

    Processor prWayToReception = Processor.New()
        .WithName("To Reception")
        .WithDelayGenerator(new Uniform(2, 5).ToDelayGenerator())
        .WithProcessesCount(coridorCount)
        .Build();

    creator.Next = (SimpleNextElement)prReception;
    prReception.Next = new TypeSpecificNextElement(new Dictionary<PatientType, Element>()
    {
        { PatientType.Type1, prWayToChamber },
        { PatientType.Type2, prWayToLaboratory },
        { PatientType.Type3, prWayToLaboratory }
    });
    prWayToLaboratory.Next = (SimpleNextElement)prLabRegistry;
    prLabRegistry.Next = (SimpleNextElement)prLaboratory;
    prLaboratory.Next = new TypeSpecificNextElement(new Dictionary<PatientType, Element>()
    {
        { PatientType.Type2, prWayToReception },
        { PatientType.Type3, dispose }
    });
    prWayToChamber.Next = (SimpleNextElement)dispose;
    prWayToReception.Next = (SimpleNextElement)prReception;

    prLaboratory.OnAfterOutAct += (patient) => patient.PatientType = PatientType.Type1;

    Model model = new Model();
    model.AddElements(creator, dispose, prReception, prWayToChamber, prWayToLaboratory, prLabRegistry, prLaboratory,
        prWayToReception);

    return model;
}