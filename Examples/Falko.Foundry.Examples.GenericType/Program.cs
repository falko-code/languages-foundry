using Falko.Foundry.Compilers;
using Falko.Foundry.CSharp.Compilers;
using Falko.Foundry.CSharp.Elements;
using Falko.Foundry.Elements;

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

var loggerFirstVariable = new TypeIdentifierElement
{
    Name = "firstLogger"u8,
    Type = loggerType
};

var loggerSecondVariable = new TypeIdentifierElement
{
    Name = "secondLogger"u8,
    Type = loggerType
};

Console.WriteLine(compiler.CompileElement(in loggerFirstVariable));
Console.WriteLine(compiler.CompileElement(in loggerSecondVariable));
