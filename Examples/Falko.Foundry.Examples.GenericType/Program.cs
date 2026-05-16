using Falko.Foundry.Common;
using Falko.Foundry.Compilers;
using Falko.Foundry.CSharp.Compilers;
using Falko.Foundry.CSharp.Elements;
using Falko.Foundry.Utf8Texts;

// scopes more optimized instead of create, cuz can use stackalloc and more safety, cuz control of lifetime by itself
var program = Utf8Buffer.StringScope(default(Unit), (scoped ref buffer, in _) =>
{
    var compiler = CSharpLanguageCompiler.Instance;

    UsingNamespaceElement usingSystem = "System"u8;
    UsingNamespaceElement usingCollectionsGeneric = "System.Collections.Generic"u8;

    var intType = new TypeElement { Name = "Int32"u8 };
    var intTypeCache = compiler.CompileElement(in intType); // for don't compile twice
    var listType = new TypeElement { Name = "List"u8, GenericTypes = [intTypeCache] };
    var pairType = new TypeElement { Name = "KeyValuePair"u8, GenericTypes = [intTypeCache, listType] };
    var pairVariable = new TypeIdentifierElement { Name = "pair"u8, Type = pairType };

    var scope = ScopeElement.Create
    (
        usingSystem.AsLine(),
        usingCollectionsGeneric.AsLine(),
        LineElement.Empty,
        ScopeElement.Empty,
        LineElement.Empty,
        pairVariable.AsLine(),
        ScopeElement.Create(pairVariable.AsLine())
    );

    compiler.CompileElement(ref buffer, in scope);
});

Console.WriteLine(program);
