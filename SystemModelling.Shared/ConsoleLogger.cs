namespace SystemModelling.Shared;

public class ConsoleLogger : ILogger
{
    public void WriteLine(string line)
    {
        Console.WriteLine(line);
    }

    public void Write(string line)
    {
        Console.Write(line);
    }
}

public class FileLogger : ILogger
{
    private readonly string _path;

    public FileLogger(string path)
    {
        _path = path;
        using (StreamWriter sw = File.CreateText(_path))
        {
            
        }
    }

    public void WriteLine(string line)
    {
        using StreamWriter sw = File.AppendText(_path);
        sw.WriteLine(line);
    }

    public void Write(string line)
    {
        using StreamWriter sw = File.AppendText(_path);
        sw.Write(line);
    }
}