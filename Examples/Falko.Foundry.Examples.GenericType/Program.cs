using Falko.Foundry.Common;
using Falko.Foundry.Compilers;
using Falko.Foundry.CSharp.Compilers;
using Falko.Foundry.CSharp.Elements;

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
    element: in pairVariable,
    argument: default(Unit),
    action: static (scoped in e, in _) => Console.WriteLine(e)
);
