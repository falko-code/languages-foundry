using System.Collections.Immutable;
using Falko.Foundry.Elements;
using Falko.Foundry.Utf8Texts;

namespace Falko.Foundry.CSharp.Elements;

public sealed class TypeElement : ILanguageElement
{
    public required Utf8String Name { get; init; }

    public Utf8String Namespace { get; init; }

    public ImmutableArray<CompilerOrLanguageElement<TypeElement>> GenericTypes { get; init; }
        = ImmutableArray<CompilerOrLanguageElement<TypeElement>>.Empty;
}
