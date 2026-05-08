using System.Collections.Immutable;
using Falko.Foundry.Compilers;
using Falko.Foundry.CSharp.Compilers;
using Falko.Foundry.CSharp.Elements;
using Falko.Foundry.Elements;
using Falko.Foundry.Utf8Texts;

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

var loggerVariables = ImmutableArray.Create<Utf8String>("firstLogger"u8, "secondLogger"u8);

Parallel.ForEach(loggerVariables, loggerVariableName =>
{
    var loggerVariable = new TypeIdentifierElement
    {
        Name = loggerVariableName,
        Type = loggerType,
    };

    Console.WriteLine(compiler.CompileElement(in loggerVariable));
});
