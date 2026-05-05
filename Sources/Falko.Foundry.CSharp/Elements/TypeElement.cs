using System.Collections.Immutable;
using Falko.Foundry.Elements;
using Falko.Foundry.Types;

namespace Falko.Foundry.CSharp.Elements;

public readonly struct TypeElement : ILanguageElement
{
    public required Utf8String Name { get; init; }

    public Utf8String Namespace { get; init; }

    public ImmutableArray<TypeElement> GenericTypes { get; init; }
}
