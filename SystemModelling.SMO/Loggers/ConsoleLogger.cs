namespace SystemModelling.SMO.Loggers;

public class ConsoleLogger : ILogger
{
    public void WriteLine(string message)
    {
        Console.WriteLine(message);
    }

    public void Write(string message)
    {
        Console.Write(message);
    }
}