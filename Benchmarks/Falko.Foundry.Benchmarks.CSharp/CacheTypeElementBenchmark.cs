using BenchmarkDotNet.Attributes;
using Falko.Foundry.Compilers;
using Falko.Foundry.CSharp.Compilers;
using Falko.Foundry.CSharp.Elements;
using Falko.Foundry.Elements;
using Falko.Foundry.Utf8Texts;

namespace Falko.Benchmarks;

[MemoryDiagnoser]
public class CacheTypeElementBenchmark
{
    private TypeElement _loggerTypeWithoutCache = new() { Name = Utf8String.Empty };

    private TypeElement _loggerTypeWithCache = new() { Name = Utf8String.Empty };

    [Params(1, 4, 8)]
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

        _loggerTypeWithoutCache = loggerType;
        _loggerTypeWithCache = loggerType.WithCache(CSharpLanguageCompiler.Instance.CompileElement(in loggerType));
    }

    [Benchmark(Baseline = true)]
    public Utf8String CompileVariableWithoutTypeElementCache()
    {
        var result = Utf8String.Empty;

        var loggerVariable = new TypeIdentifierElement
        {
            Name = "loggerVariable"u8,
            Type = _loggerTypeWithoutCache
        };

        for (var i = 0; i < Iterations; i++)
        {
            result = CSharpLanguageCompiler
                .Instance
                .CompileElement(in loggerVariable);
        }

        return result;
    }

    [Benchmark]
    public Utf8String CompileVariableWithTypeElementCache()
    {
        var result = Utf8String.Empty;

        var loggerVariable = new TypeIdentifierElement
        {
            Name = "loggerVariable"u8,
            Type = _loggerTypeWithCache
        };

        for (var i = 0; i < Iterations; i++)
        {
            result = CSharpLanguageCompiler
                .Instance
                .CompileElement(in loggerVariable);
        }

        return result;
    }
}
