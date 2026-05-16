using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Falko.Foundry.Common;
using Falko.Foundry.Elements;

namespace Falko.Foundry.CSharp.Elements;

[method: MethodImpl(MethodImplOptions.AggressiveInlining)]
public readonly struct ScopeElement() : ILanguageElement, ISafeStruct, IIndentationElementMixin<ScopeElement>
{
    public static readonly ScopeElement Empty = new();

    public bool IsInit { get; } = true;

    public int Indent { get; init; }

    public ImmutableArray<IndentationElement> Elements { get; init; } = [];

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScopeElement Copy
    (
        in ScopeElement element,
        int indent
    )
    {
        return element with { Indent = indent };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScopeElement Create(params ImmutableArray<IndentationElement> elements)
    {
        return new ScopeElement { Elements = elements };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator ScopeElement(ImmutableArray<IndentationElement> elements)
    {
        return new ScopeElement { Elements = elements };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator IndentationElement(ScopeElement element)
    {
        return new IndentationElement(element.ToIndentationElement());
    }
}
