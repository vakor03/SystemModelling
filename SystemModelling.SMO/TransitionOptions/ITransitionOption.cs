using SystemModelling.SMO.Elements;

namespace SystemModelling.SMO.TransitionOptions;

public interface ITransitionOption
{
    Element? Next { get; }
}