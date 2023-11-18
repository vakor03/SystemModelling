namespace SystemModelling.ModelWIthItems.Collections;

public interface IQueue<T>
{
    int MaxQueue { get; }
    int QueueLength { get; }
    void Add(T item);
    T? Get();
    bool IsFull();
    bool IsEmpty();
}