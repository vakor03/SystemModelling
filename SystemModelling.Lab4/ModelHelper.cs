using SystemModelling.SMO;
using SystemModelling.SMO.Elements;
using SystemModelling.SMO.Enums;
using SystemModelling.SMO.Transitions;

namespace SystemModelling.Lab4;

public static class ModelHelper
{
    public static Model CreateLinearModel(int processesCount)
    {
        const float createElementMeanDelay = 1;
        const DistributionType distributionType = DistributionType.Exp;
        const float processElementMeanDelay = 1;

        List<Element> elements = new List<Element>();
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

        List<Element> elements = new List<Element>();

        Create create = CreateCreator(distributionType, createElementMeanDelay);

        elements.Add(create);

        for (int i = 0; i < layersCount; i++)
        {
            int elementsInLayer = (int)Math.Pow(2, i);
            for (int j = 0; j < elementsInLayer; j++)
            {
                Process process = CreateProcessor(distributionType, processElementMeanDelay);

                var previousElement = elements[^(j + 1 + j / 2)];
                previousElement.Transition = new SingleTransition(process);
            }
        }

        Model model = new Model(elements);
        return model;
    }

    private static Process CreateProcessor(DistributionType distributionType, float processElementMeanDelay)
    {
        return Process.New()
            // .WithName($"Process {i + 1}")
            .WithDistribution(distributionType)
            .WithDelayMean(processElementMeanDelay)
            .Build();
    }

    private static Create CreateCreator(DistributionType distributionType, float createElementMeanDelay)
    {
        return Create.New()
            // .WithName("Create")
            .WithDistribution(distributionType)
            .WithDelayMean(createElementMeanDelay)
            .Build();
    }
}