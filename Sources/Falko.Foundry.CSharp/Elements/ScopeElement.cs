using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Falko.Foundry.Elements;
using Falko.Foundry.Mixins;

namespace Falko.Foundry.CSharp.Elements;

[method: MethodImpl(MethodImplOptions.AggressiveInlining)]
public readonly struct ScopeElement() : ILanguageElement,
    IIndentationMixin<ScopeElement>, IStructInitMixin<ScopeElement>
{
    public bool IsInit { get; } = true;

    public int Indent { get; init; }

    public ImmutableArray<IndentationElement> Elements { get; init; } = [];

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScopeElement Copy
    (
        scoped in ScopeElement element,
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
        return element.ToIndentationElement();
    }
}
