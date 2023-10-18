namespace SystemModelling.SMO.Transitions;

public record struct ProbabilityOption
{
    public float Probability { get; set; }
    public ITransition Transition { get; set; }

    public ProbabilityOption(float probability, ITransition transition)
    {
        Probability = probability;
        Transition = transition;
    }
}