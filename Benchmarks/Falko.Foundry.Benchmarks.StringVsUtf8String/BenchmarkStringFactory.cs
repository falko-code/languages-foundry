namespace Falko.Benchmarks;

public static class BenchmarkStringFactory
{
    public static string CreateString()
    {
        const string repeatText = nameof(Falko);
        const int repeatDeep = 5;

        return string.Concat(Enumerable.Repeat(repeatText, repeatDeep));
    }
}
