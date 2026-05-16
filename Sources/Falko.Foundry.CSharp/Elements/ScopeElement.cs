using System.Collections.Immutable;
using Falko.Foundry.Common;
using Falko.Foundry.Elements;

namespace Falko.Foundry.CSharp.Elements;

public readonly struct ScopeElement() : ILanguageElement, ISafeStruct, IIndentationElementMixin<ScopeElement>
{
    public static readonly ScopeElement Empty = new();

    public bool IsInit { get; } = true;

    public int Indent { get; init; }

    public ImmutableArray<IndentationElement> Elements { get; init; }
        = ImmutableArray<IndentationElement>.Empty;

    public static ScopeElement MutateIndent
    (
        in ScopeElement element,
        int indent
    )
    {
        return element with { Indent = indent };
    }

    public static ScopeElement Create(params ImmutableArray<IndentationElement> elements) => new() { Elements = elements };

    public static implicit operator ScopeElement(ImmutableArray<IndentationElement> elements)
    {
        return new ScopeElement { Elements = elements };
    }
}
