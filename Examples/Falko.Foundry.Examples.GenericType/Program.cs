using System.Diagnostics;
using Falko.Foundry.Compilers;
using Falko.Foundry.CSharp.Compilers;
using Falko.Foundry.CSharp.Elements;

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

var result = CSharpLanguageCompiler
    .Instance
    .CompileElement(in loggerType)
    .ToString();

Console.WriteLine(result);

Debug.Assert(result is "Falko.Logging.Loggers.Logger<Service>");
