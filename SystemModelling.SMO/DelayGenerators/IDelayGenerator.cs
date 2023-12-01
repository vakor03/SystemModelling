namespace SystemModelling.SMO.DelayGenerators;

public interface IDelayGenerator<T> where T : IItem
{
    double GetDelay(T item);
}

public interface IDelayGenerator
{
    double GetDelay();
}

public interface IItem
{
}