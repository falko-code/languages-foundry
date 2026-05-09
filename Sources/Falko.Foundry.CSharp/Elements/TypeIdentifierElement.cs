using Falko.Foundry.Caches;
using Falko.Foundry.Common;
using Falko.Foundry.Elements;
using Falko.Foundry.Utf8Texts;

namespace Falko.Foundry.CSharp.Elements;

public readonly record struct TypeIdentifierElement() : ILanguageElement, ISafeStruct
{
    public bool IsInit { get; } = true;

    public required Utf8String Name { get; init; }

    public required CompilerOrLanguageElement<TypeElement> Type { get; init; }
}
