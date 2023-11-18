using SystemModelling.ModelWIthItems.Patients;

namespace SystemModelling.ModelWIthItems.Collections;

public class PatientsPriorityQueue : IQueue<Patient>
{
    public int MaxQueue { get; }
    public int QueueLength => _prioritizedPatients.Count + _otherPatients.Count;
    
    private readonly Queue<Patient> _prioritizedPatients = new();
    private readonly Queue<Patient> _otherPatients = new();

    public PatientsPriorityQueue(int maxQueue)
    {
        MaxQueue = maxQueue;
    }
    public void Add(Patient item)
    {
        if (IsFull())
        {
            throw new InvalidOperationException("Queue is full");
        }
        
        if (item.PatientType == PatientType.Type1)
        {
            _prioritizedPatients.Enqueue(item);
        }
        else
        {
            _otherPatients.Enqueue(item);
        }
    }

    public Patient? Get()
    {
        if (_prioritizedPatients.Count > 0)
        {
            return _prioritizedPatients.Dequeue();
        }

        if (_otherPatients.Count > 0)
        {
            return _otherPatients.Dequeue();
        }

        return null;
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