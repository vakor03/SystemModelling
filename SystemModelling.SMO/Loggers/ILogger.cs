namespace SystemModelling.SMO.Loggers;

public interface ILogger
{
    void WriteLine(string message);
    void Write(string message);
}