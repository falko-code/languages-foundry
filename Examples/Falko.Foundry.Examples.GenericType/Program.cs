using Falko.Foundry.Common;
using Falko.Foundry.Compilers;
using Falko.Foundry.CSharp.Compilers;
using Falko.Foundry.CSharp.Elements;
using Falko.Foundry.Utf8Texts;

UsingNamespaceElement usingSystem = "System"u8.ToUtf8String();

CSharpLanguageCompiler.Instance.CompileElement
(
    element: usingSystem.AsLine(),
    argument: default(Unit),
    action: static (scoped in e, in _) => Console.WriteLine(e)
);

var intType = new TypeElement
{
    Name = "Int32"u8,
    Namespace = "System"u8
};

var listType = new TypeElement
{
    Name = "List"u8,
    Namespace = "System.Collections.Generic"u8,
    GenericTypes = [intType]
};

var pairType = new TypeElement
{
    Name = "KeyValuePair"u8,
    Namespace = "System.Collections.Generic"u8,
    GenericTypes = [intType, listType]
};

var pairVariable = new TypeIdentifierElement
{
    Name = "pair"u8,
    Type = pairType
};

CSharpLanguageCompiler.Instance.CompileElement
(
    element: pairVariable.AsLine(),
    argument: default(Unit),
    action: static (scoped in e, in _) => Console.WriteLine(e)
);
