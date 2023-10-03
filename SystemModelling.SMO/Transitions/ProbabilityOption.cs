namespace SystemModelling.SMO.Transitions;

public record struct ProbabilityOption
{
    public float Probability { get; set; }
    public ITransition Transition { get; set; }
}