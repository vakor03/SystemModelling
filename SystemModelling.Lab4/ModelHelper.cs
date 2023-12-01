using SystemModelling.SMO;
using SystemModelling.SMO.Elements;
using SystemModelling.SMO.RandomValuesProviders;
using SystemModelling.SMO.Transitions;

namespace SystemModelling.Lab4;

public static class ModelHelper
{
    public static Model CreateLinearModel(int processesCount)
    {
        const float createElementMeanDelay = 1;
        const float processElementMeanDelay = 1;

        List<IElement> elements = new List<IElement>();
        var build = CreateCreator(createElementMeanDelay);
        Create create = build;
        elements.Add(create);

        Element previousElement = create;
        for (int i = 0; i < processesCount; i++)
        {
            Process process = CreateProcessor(processElementMeanDelay);

            previousElement.Transition = new SingleTransition(process);
            elements.Add(process);

            previousElement = process;
        }

        Model model = new Model(elements);
        return model;
    }


    public static Model CreateBranchedModel(int layersCount)
    {
        const float createElementMeanDelay = 1;
        const float processElementMeanDelay = 1;

        List<IElement> elements = new List<IElement>();

        Create create = CreateCreator(createElementMeanDelay);

        elements.Add(create);

        int k = 3;
        for (int i = 0; i < layersCount; i++)
        {
            int elementsInLayer = (int)Math.Pow(2, i);
            for (int j = 1; j <= elementsInLayer; j++)
            {
                Process process1 = CreateProcessor(processElementMeanDelay);
                Process process2 = CreateProcessor(processElementMeanDelay);
                
                elements.Add(process1);
                elements.Add(process2);

                var previousElement = (Element)elements[^k];
                previousElement.Transition = new ProbabilityTransition(new List<ProbabilityOption>()
                {
                    new(0.5f, process1),
                    new(0.5f, process2),
                });
                
                k++;
            }
        }
        
        Model model = new Model(elements);
        return model;
    }
    private static Process CreateProcessor(float processElementMeanDelay)
    {
        return new Process()
        {
            DelayGenerator = new Exponential(processElementMeanDelay).ToGenerator(),
        };
    }

    private static Create CreateCreator(float createElementMeanDelay)
    {
        return new Create()
        {
            DelayGenerator = new Exponential(createElementMeanDelay).ToGenerator()
        };
    }
}