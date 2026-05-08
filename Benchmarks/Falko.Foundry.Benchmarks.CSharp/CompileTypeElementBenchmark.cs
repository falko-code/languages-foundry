using BenchmarkDotNet.Attributes;
using Falko.Foundry.Compilers;
using Falko.Foundry.CSharp.Compilers;
using Falko.Foundry.CSharp.Elements;
using Falko.Foundry.Utf8Texts;

namespace Falko.Benchmarks;

[MemoryDiagnoser]
public class CompileTypeElementBenchmark
{
    private TypeElement _loggerType = new() { Name = Utf8String.Empty };

    [Params(1, 8, 16)]
    public int Iterations { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        var serviceType = new TypeElement
        {
            Name = "Service"u8
        };

        var loggerType = new TypeElement
        {
            Namespace = "Falko.Logging.Loggers"u8,
            Name = "Logger"u8,
            GenericTypes = [serviceType]
        };

        _loggerType = loggerType;
    }

    [Benchmark]
    public Utf8String CompileTypeElement()
    {
        var result = Utf8String.Empty;

        for (var i = 0; i < Iterations; i++)
        {
            result = CSharpLanguageCompiler
                .Instance
                .CompileElement(in _loggerType);
        }

        return result;
    }
}
