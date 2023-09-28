using SystemModelling.SMO.Elements;

namespace SystemModelling.SMO.TransitionOptions;

public record struct ProbabilityOption
{
    public float Probability { get; set; }
    public Element Element { get; set; }
}

public static class TransitionOptionExtensions
{
    public static SingleTransitionOption ToSingleTransitionOption(this Element element)
    {
        return new SingleTransitionOption(element);
    }
}