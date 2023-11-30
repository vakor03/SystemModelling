using SystemModelling.SMO.Loggers;

namespace SystemModelling.SMO.Elements;

public class Dispose : Element
{
    public Dispose() : base()
    {
        TNext = double.MaxValue;
    }

    public override void InAct()
    {
        InQuantity++;
    }

    public override void PrintResult(ILogger logger)
    {
        logger.WriteLine($"\nDispose {Name}\n\tItems disposed: {InQuantity}");
    }

    public override void OutAct()
    {
        throw new Exception("Dispose element can't be out");
    }
}