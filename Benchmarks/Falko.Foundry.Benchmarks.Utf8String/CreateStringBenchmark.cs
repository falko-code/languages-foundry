using BenchmarkDotNet.Attributes;
using Falko.Foundry.Types;

namespace Falko.Benchmarks;

[MemoryDiagnoser]
public class CreateStringBenchmark
{
    private ReadOnlyMemory<char> _textValue = ReadOnlyMemory<char>.Empty;
    private ReadOnlyMemory<byte> _utf8TextValue = ReadOnlyMemory<byte>.Empty;

    [Params(5, 25)]
    public int Iterations { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        var textValue = BenchmarkStringFactory.CreateString();

        _textValue = textValue.AsMemory();
        _utf8TextValue = textValue.ToUtf8String().AsMemory();
    }

    [Benchmark(Baseline = true)]
    public string CreateString()
    {
        var result = string.Empty;

        for (var i = 0; i < Iterations; i++)
        {
            result = new string(_textValue.Span);
        }

        return result;
    }

    [Benchmark]
    public Utf8String CreateUtf8String()
    {
        var result = Utf8String.Empty;

        for (var i = 0; i < Iterations; i++)
        {
            result = new Utf8String(_utf8TextValue.Span);
        }

        return result;
    }
}
