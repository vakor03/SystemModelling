using SystemModelling.ModelWIthItems;
using SystemModelling.ModelWIthItems.Collections;
using SystemModelling.ModelWIthItems.Elements;
using SystemModelling.ModelWIthItems.NextElements;

var model = CreateHospitalModel();

model.SimulationTime = 10;

model.Simulate();

static Model CreateHospitalModel()
{
    Creator creator = Creator.New()
        .WithName("Creator")
        .Build();

    Processor processor = Processor.New()
        .WithName("Reception")
        .Build();

    Processor processor2 = Processor.New()
        .WithName("Test")
        .Build();

    creator.Next = (SimpleNextElement)processor;
    processor.Next = (SimpleNextElement)processor2;
    
    Model model = new Model();
    model.AddElements(creator, processor);
    
    return model;
}