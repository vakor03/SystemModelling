namespace SystemModelling.ModelWIthItems.DelayGenerators;

public interface IDelayGenerator<T> where T : IItem
{
    double GetDelay(T item);
}

public interface IItem
{
}