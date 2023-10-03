namespace SystemModelling.SMO;

public interface ICloneable<out T>
{
    T Clone();
}