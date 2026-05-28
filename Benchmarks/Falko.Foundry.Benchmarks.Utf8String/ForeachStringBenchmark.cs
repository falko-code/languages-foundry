using System.Text;
using BenchmarkDotNet.Attributes;
using Falko.Foundry.Utf8Texts;

namespace Falko.Benchmarks;

[MemoryDiagnoser]
public class ForeachStringBenchmark
{
    private string _textValue = null!;
    private Utf8String _utf8TextValue = Utf8String.Empty;

    [Params(1, 3)]
    public int Iterations { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        var textValue = BenchmarkStringFactory.CreateString();

        _textValue = textValue;
        _utf8TextValue = textValue.ToUtf8String();
    }

    [Benchmark(Baseline = true)]
    public char ForeachString()
    {
        var result = char.MinValue;

        for (var i = 0; i < Iterations; i++)
        {
            foreach (var c in _textValue)
            {
                result = c;
            }
        }

        return result;
    }

    [Benchmark]
    public Utf8Char ForeachUtf8String()
    {
        var result = default(Utf8Char);

        for (var i = 0; i < Iterations; i++)
        {
            foreach (var c in _utf8TextValue)
            {
                result = c;
            }
        }

        return result;
    }

    [Benchmark]
    public char ForeachUtf8StringToString()
    {
        var result = char.MinValue;

        for (var i = 0; i < Iterations; i++)
        {
            foreach (var c in _utf8TextValue.ToString())
            {
                result = c;
            }
        }

        return result;
    }
}
