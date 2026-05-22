using System.Runtime.CompilerServices;
using Falko.Foundry.Elements;
using Falko.Foundry.Mixins;

namespace Falko.Foundry.CSharp.Elements;

public readonly struct LineElement<TLineElement> : ILanguageElement,
    IIndentationMixin<LineElement<TLineElement>>, IStructInitMixin<LineElement<TLineElement>>
        where TLineElement : ILanguageElement
{
    public bool IsInit => Element.IsInit;

    public int Indent { get; init; }

    public required CompilerOrLanguageElement<TLineElement> Element { get; init; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LineElement<TLineElement> Copy
    (
        scoped in LineElement<TLineElement> element,
        int indent
    )
    {
        return element with { Indent = indent };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator IndentationElement(LineElement<TLineElement> element)
    {
        return element.ToIndentationElement();
    }
}
