using SystemModelling.ModelWIthItems.DelayGenerators;
using SystemModelling.SMO;
using SystemModelling.SMO.Builders;
using SystemModelling.SMO.Elements;
using SystemModelling.SMO.Enums;
using SystemModelling.SMO.RandomValuesProviders;
using SystemModelling.SMO.Transitions;

namespace SystemModelling.Lab4;

public static class ModelHelper
{
    public static Model CreateLinearModel(int processesCount)
    {
        const float createElementMeanDelay = 1;
        const DistributionType distributionType = DistributionType.Exp;
        const float processElementMeanDelay = 1;

        List<IElement> elements = new List<IElement>();
        var build = CreateCreator(distributionType, createElementMeanDelay);
        Create create = build;
        elements.Add(create);

        Element previousElement = create;
        for (int i = 0; i < processesCount; i++)
        {
            Process process = CreateProcessor(distributionType, processElementMeanDelay);

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
        const DistributionType distributionType = DistributionType.Exp;

        List<IElement> elements = new List<IElement>();

        Create create = CreateCreator(distributionType, createElementMeanDelay);

        elements.Add(create);

        int k = 3;
        for (int i = 0; i < layersCount; i++)
        {
            int elementsInLayer = (int)Math.Pow(2, i);
            for (int j = 1; j <= elementsInLayer; j++)
            {
                Process process1 = CreateProcessor(distributionType, processElementMeanDelay);
                Process process2 = CreateProcessor(distributionType, processElementMeanDelay);
                
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
    private static FluentProcessBuilder _processBuilder = FluentProcessBuilder.New();
    private static FluentCreateBuilder _createBuilder = FluentCreateBuilder.New();

    private static Process CreateProcessor(DistributionType distributionType, float processElementMeanDelay)
    {
        return _processBuilder
            // .WithName($"Process {i + 1}")
            .WithDelayGenerator(new Exponential(processElementMeanDelay).ToGenerator())
            // .WithDistribution(distributionType)
            // .WithDelayMean(processElementMeanDelay)
            .Build();
    }

    private static Create CreateCreator(DistributionType distributionType, float createElementMeanDelay)
    {
        return _createBuilder
            // .WithName("Create")
            .WithDelayGenerator(new Exponential(createElementMeanDelay).ToGenerator())
            .Build();
    }
}