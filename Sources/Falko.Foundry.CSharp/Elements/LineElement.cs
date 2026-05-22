using System.Runtime.CompilerServices;
using Falko.Foundry.Elements;
using Falko.Foundry.Mixins;

namespace Falko.Foundry.CSharp.Elements;

public readonly struct LineElement : ILanguageElement,
    IIndentationMixin<LineElement>, ISingletonMixin<LineElement>
{
    public static LineElement Instance => default;

    public int Indent => 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LineElement Copy
    (
        scoped in LineElement element,
        int indent
    )
    {
        return element;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator IndentationElement(LineElement element)
    {
        return element.ToIndentationElement();
    }
}
