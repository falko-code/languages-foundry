using BenchmarkDotNet.Attributes;
using Falko.Foundry.Compilers;
using Falko.Foundry.CSharp.Compilers;
using Falko.Foundry.CSharp.Elements;
using Falko.Foundry.Elements;

namespace Falko.Benchmarks;

[MemoryDiagnoser]
public class CacheTypeElementBenchmark
{
    private TypeElement? _loggerType;

    [Params(1, 2, 4, 8, 16)]
    public int Iterations { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        var serviceType = new TypeElement
        {
            Name = "Service"u8,
            Namespace = "MyProject.Services"u8
        };

        var loggerType = new TypeElement
        {
            Namespace = "Falko.Logging.Loggers"u8,
            Name = "Logger"u8,
            GenericTypes = [serviceType]
        };

        _loggerType = loggerType;
    }

    [Benchmark(Baseline = true)]
    public CompilerElement<TypeIdentifierElement> CompileVariableWithoutTypeElementCache()
    {
        var result = default(CompilerElement<TypeIdentifierElement>);

        var loggerVariable = new TypeIdentifierElement
        {
            Name = "loggerVariable"u8,
            Type = _loggerType!
        };

        for (var i = 0; i < Iterations; i++)
        {
            result = CSharpLanguageCompiler.Instance
                .CompileElement(in loggerVariable);
        }

        return result;
    }

    [Benchmark]
    public CompilerElement<TypeIdentifierElement> CompileVariableWithTypeElementCache()
    {
        var result = default(CompilerElement<TypeIdentifierElement>);

        CSharpLanguageCompiler.Instance.CompileElement
        (
            element: in _loggerType!,
            argument: (Iterations, result),
            action: static (scoped in loggerType, in context) =>
            {
                var result = context.result;
                var iterations = context.Iterations;

                var loggerVariable = new TypeIdentifierElement
                {
                    Name = "loggerVariable"u8,
                    Type = loggerType
                };

                for (var i = 0; i < iterations; i++)
                {
                    result = CSharpLanguageCompiler.Instance
                        .CompileElement(in loggerVariable);
                }
            }
        );

        return result;
    }
}
