namespace SystemModelling.ModelWIthItems.Collections;

public class SimpleQueue<T> : IQueue<T>
{
    public int MaxQueue { get; }
    public int QueueLength => _queue.Count;
    
    private readonly Queue<T> _queue = new();

    public SimpleQueue(int maxQueue)
    {
        MaxQueue = maxQueue;
    }
    public void Add(T item)
    {
        if (IsFull())
        {
            throw new InvalidOperationException("Queue is full");
        }
        
        _queue.Enqueue(item);
    }

    public T? Get()
    {
        if (IsEmpty())
        {
            throw new InvalidOperationException("Queue is empty");
        }
        
        return _queue.Dequeue();
    }

    public bool IsFull()
    {
        return QueueLength >= MaxQueue;
    }

    public bool IsEmpty()
    {
        return QueueLength == 0;
    }
}