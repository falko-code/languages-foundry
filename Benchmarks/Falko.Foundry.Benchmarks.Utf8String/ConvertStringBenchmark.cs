using BenchmarkDotNet.Attributes;
using Falko.Foundry.Utf8Texts;

namespace Falko.Benchmarks;

[MemoryDiagnoser]
public class ConvertStringBenchmark
{
    private string _textValue = null!;
    private Utf8String _utf8TextValue = Utf8String.Empty;

    [Params(5, 25)]
    public int Iterations { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        var textValue = BenchmarkStringFactory.CreateString();

        _textValue = textValue;
        _utf8TextValue = textValue.ToUtf8String();
    }

    [Benchmark(Baseline = true)]
    public string CovertUtf8StringToString()
    {
        var result = string.Empty;

        for (var i = 0; i < Iterations; i++)
        {
            result = _utf8TextValue.ToString();
        }

        return result;
    }

    [Benchmark]
    public Utf8String ConvertStringToUtf8String()
    {
        var result = Utf8String.Empty;

        for (var i = 0; i < Iterations; i++)
        {
            result = _textValue.ToUtf8String();
        }

        return result;
    }
}
