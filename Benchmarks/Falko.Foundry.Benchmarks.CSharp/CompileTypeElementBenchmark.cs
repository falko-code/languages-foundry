using BenchmarkDotNet.Attributes;
using Falko.Foundry.Compilers;
using Falko.Foundry.CSharp.Compilers;
using Falko.Foundry.CSharp.Elements;
using Falko.Foundry.Elements;

namespace Falko.Benchmarks;

[MemoryDiagnoser]
public class CompileTypeElementBenchmark
{
    private TypeElement? _loggerType;

    [Params(1, 4)]
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
    public CompilerElement<TypeElement> CompileTypeElement()
    {
        var result = default(CompilerElement<TypeElement>);

        for (var i = 0; i < Iterations; i++)
        {
            result = CSharpLanguageCompiler.Instance
                .CompileElement(in _loggerType!);
        }

        return result;
    }
}
