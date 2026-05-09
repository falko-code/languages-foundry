using System.Collections.Immutable;
using System.Diagnostics;
using Falko.Foundry.Compilers;
using Falko.Foundry.CSharp.Compilers;
using Falko.Foundry.CSharp.Elements;
using Falko.Foundry.Elements;
using Falko.Foundry.Utf8Texts;

var allocate = GC.GetAllocatedBytesForCurrentThread();

var watch = Stopwatch.StartNew();

var compiler = CSharpLanguageCompiler.Instance;

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

loggerType = loggerType.WithCache(compiler.CompileElement(in loggerType));

var loggerVariables = Enumerable
    .Range(1, 1000)
    .Select(i => (Utf8String)"logger"u8 + i.ToString())
    .ToImmutableArray();

Parallel.ForEach(loggerVariables, loggerVariableName =>
{
    var loggerVariable = new TypeIdentifierElement
    {
        Name = loggerVariableName,
        Type = loggerType,
    };

    compiler.CompileElement
    (
        element: in loggerVariable,
        argument: loggerVariableName,
        action: static (scoped utf8Bytes, in loggerVariableName) =>
        {
            File.WriteAllBytes($"{loggerVariableName}.variable.cs", utf8Bytes);
        }
    );
});

watch.Stop();

allocate = GC.GetAllocatedBytesForCurrentThread() - allocate;

Console.WriteLine($"Generated {loggerVariables.Length} logger variables in {watch.Elapsed.TotalMilliseconds} milliseconds, allocated {allocate} bytes");
