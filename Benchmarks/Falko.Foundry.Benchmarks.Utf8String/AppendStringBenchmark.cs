using BenchmarkDotNet.Attributes;
using Falko.Foundry.Types;

namespace Falko.Benchmarks;

[MemoryDiagnoser]
public class AppendStringBenchmark
{
    private string _textValue = null!;
    private Utf8String _utf8TextValue = Utf8String.Empty;

    [Params(1, 5)]
    public int Iterations { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        var textValue = BenchmarkStringFactory.CreateString();

        _textValue = textValue;
        _utf8TextValue = textValue.ToUtf8String();
    }

    [Benchmark(Baseline = true)]
    public string AppendString()
    {
        var result = string.Empty;

        for (var i = 0; i < Iterations; i++)
        {
            result = _textValue + _textValue;
        }

        return result;
    }

    [Benchmark]
    public Utf8String AppendUtf8String()
    {
        var result = Utf8String.Empty;

        for (var i = 0; i < Iterations; i++)
        {
            result = _utf8TextValue + _utf8TextValue;
        }

        return result;
    }

    [Benchmark]
    public Utf8String AppendStringUtf8String()
    {
        var result = Utf8String.Empty;

        for (var i = 0; i < Iterations; i++)
        {
            result = _textValue + _utf8TextValue;
        }

        return result;
    }
}
