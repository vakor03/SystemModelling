using SystemModelling.SMO.Elements;

namespace SystemModelling.SMO.Transitions;

public record struct ProbabilityOption(float Probability, Element Element);