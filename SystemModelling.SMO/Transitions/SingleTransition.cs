using SystemModelling.SMO.Elements;

namespace SystemModelling.SMO.Transitions;

public class SingleTransition : ITransition
{
    public Element? Next { get; }

    public SingleTransition(Element? next)
    {
        Next = next;
    }

    public static explicit operator SingleTransition (Element element) {
        return new SingleTransition(element);
    }
}