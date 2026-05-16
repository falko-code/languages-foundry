using System.Runtime.CompilerServices;
using Falko.Foundry.Elements;

namespace Falko.Foundry.CSharp.Elements;

public readonly struct LineElement : ILanguageElement, IIndentationElementMixin<LineElement>
{
    public static readonly LineElement Default = new();

    int IIndentationElementMixin<LineElement>.Indent => 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LineElement Copy
    (
        in LineElement element,
        int indent
    )
    {
        return element;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator IndentationElement(LineElement element)
    {
        return new IndentationElement(element.ToIndentationElement());
    }
}
