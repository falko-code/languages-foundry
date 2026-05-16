using System.Runtime.CompilerServices;
using Falko.Foundry.Elements;

namespace Falko.Foundry.CSharp.Elements;

public readonly struct LineElement<TLineElement>
    : ILanguageElement, IIndentationElementMixin<LineElement<TLineElement>>
        where TLineElement : ILanguageElement
{
    public int Indent { get; init; }

    public required CompilerOrLanguageElement<TLineElement> Element { get; init; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LineElement<TLineElement> Copy
    (
        in LineElement<TLineElement> element,
        int indent
    )
    {
        return element with { Indent = indent };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator IndentationElement(LineElement<TLineElement> element)
    {
        return new IndentationElement(element.ToIndentationElement());
    }
}
