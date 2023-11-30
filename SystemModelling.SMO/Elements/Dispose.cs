using SystemModelling.SMO.Loggers;

namespace SystemModelling.SMO.Elements;

public class Dispose : Element
{
    public override void PrintResult(ILogger logger)
    {
        logger.WriteLine($"Dispose {Name}\n\tItems disposed: {InQuantity}");
    }

    public override void OutAct()
    {
        throw new Exception("Dispose element can't be out");
    }
}