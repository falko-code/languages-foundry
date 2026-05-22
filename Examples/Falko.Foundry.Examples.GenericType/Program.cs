using Falko.Foundry.Compilers;
using Falko.Foundry.CSharp.Compilers;
using Falko.Foundry.CSharp.Elements;

var compiler = CSharpLanguageCompiler.Instance;

var usingSystem = new UsingNamespaceElement { Namespace = "System"u8 };
var usingCollectionsGeneric = new UsingNamespaceElement { Namespace = "System.Collections.Generic"u8 };

var intType = new TypeElement { Name = "Int32"u8 };
var intTypeCache = compiler.CompileElement(in intType);
var listType = new TypeElement { Name = "List"u8, GenericTypes = [intTypeCache] };
var pairType = new TypeElement { Name = "KeyValuePair"u8, GenericTypes = [intTypeCache, listType] };
var pairVariable = new TypeIdentifierElement { Name = "pair"u8, Type = pairType };

var scope = ScopeElement.Create
(
    usingSystem.AsLine(),
    usingCollectionsGeneric.AsLine(),
    pairVariable.AsLine()
);

Console.WriteLine(compiler.CompileElement(in scope));
