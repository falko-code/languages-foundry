using System.Runtime.CompilerServices;
using Falko.Foundry.Compilers;
using Falko.Foundry.Mixins;

namespace Falko.Foundry.Elements;


public readonly record struct IndentationElement : ILanguageElement, IStructInitMixin<IndentationElement>
{
    private readonly IIndentationElementCompiler _elementCompiler;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private IndentationElement(IIndentationElementCompiler elementCompiler)
    {
        _elementCompiler = elementCompiler;
    }

    public bool IsInit => _elementCompiler is not null;

    public IIndentationElementCompiler Compiler
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _elementCompiler;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IndentationElement Wrap<T>(scoped in T indentationElement)
        where T : ILanguageElement, IIndentationMixin<T>
    {
        return new IndentationElement
        (
            elementCompiler: new IndentationElementCompiler<T>(in indentationElement)
        );
    }
}
