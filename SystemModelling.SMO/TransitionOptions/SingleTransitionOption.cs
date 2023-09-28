using SystemModelling.SMO.Elements;

namespace SystemModelling.SMO.TransitionOptions;

public class SingleTransitionOption : ITransitionOption
{
    private Element? _next;

    public Element? Next => _next;

    public SingleTransitionOption(Element? next)
    {
        _next = next;
    }

    public SingleTransitionOption()
    {
        _next = null;
    }

    public static implicit operator SingleTransitionOption (Element element) {
        return new SingleTransitionOption(element);
    }
}