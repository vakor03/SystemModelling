namespace SystemModelling.SMO.Loggers;

public interface ILogger
{
    void WriteLine(string line);
    void Write(string line);
}