using Falko.Foundry.Elements;

namespace Falko.Foundry.CSharp.Elements;

public readonly struct LineElement<TLineElement> : ILanguageElement, IIndentationElementMixin<LineElement<TLineElement>> where TLineElement : ILanguageElement
{
    public int Indent { get; init; }

    public required CompilerOrLanguageElement<TLineElement> Element { get; init; }

    public static LineElement<TLineElement> MutateIndent
    (
        in LineElement<TLineElement> element,
        int indent
    )
    {
        return element with { Indent = indent };
    }
}
