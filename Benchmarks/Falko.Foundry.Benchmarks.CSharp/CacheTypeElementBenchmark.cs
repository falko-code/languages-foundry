using BenchmarkDotNet.Attributes;
using Falko.Foundry.Compilers;
using Falko.Foundry.CSharp.Compilers;
using Falko.Foundry.CSharp.Elements;
using Falko.Foundry.Elements;

namespace Falko.Benchmarks;

[MemoryDiagnoser]
public class CacheTypeElementBenchmark
{
    private TypeElement _loggerType;

    private CompilerElement<TypeElement> _compilerLoggerType;

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

        _loggerType = loggerType;
        _compilerLoggerType = CSharpLanguageCompiler.Instance.CompileElement(in loggerType);
    }

    [Benchmark(Baseline = true)]
    public CompilerElement<TypeIdentifierElement> CompileVariableWithoutTypeElementCache()
    {
        var result = default(CompilerElement<TypeIdentifierElement>);

        var loggerVariable = new TypeIdentifierElement
        {
            Name = "loggerVariable"u8,
            Type = _loggerType
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
    public CompilerElement<TypeIdentifierElement> CompileVariableWithTypeElementCache()
    {
        var result = default(CompilerElement<TypeIdentifierElement>);

        var loggerVariable = new TypeIdentifierElement
        {
            Name = "loggerVariable"u8,
            Type = _compilerLoggerType
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
