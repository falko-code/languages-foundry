using System.Collections.Immutable;
using Falko.Foundry.Caches;
using Falko.Foundry.Elements;
using Falko.Foundry.Utf8Texts;

namespace Falko.Foundry.CSharp.Elements;

public readonly struct TypeElement() : ILanguageElement, ICacheElement<TypeElement>
{
    public CompilerCache Cache { get; private init; }

    public required Utf8String Name { get; init; }

    public Utf8String Namespace { get; init; } = Utf8String.Empty;

    public ImmutableArray<TypeElement> GenericTypes { get; init; } = ImmutableArray<TypeElement>.Empty;

    public static TypeElement CacheCopy(scoped in TypeElement element, CompilerCache cache)
    {
        return element with
        {
            Cache = cache
        };
    }
}
