public interface IGenerator
{
    double Next();

    IEnumerable<double> GenerateMany(int count)
    {
        return Enumerable.Range(0, count).Select(_ => Next());
    }
}