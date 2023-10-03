using SystemModelling.SMO.Elements;

namespace SystemModelling.SMO.Transitions;

public interface ITransition
{
    Element? Next { get; }
}