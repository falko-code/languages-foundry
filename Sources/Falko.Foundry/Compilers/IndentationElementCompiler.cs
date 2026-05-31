using System.Runtime.CompilerServices;
using Falko.Foundry.Elements;
using Falko.Foundry.Mixins;
using Falko.Foundry.Utf8Texts;

namespace Falko.Foundry.Compilers;

internal sealed class IndentationElementCompiler<T> : IIndentationElementCompiler
    where T : ILanguageElement, IIndentationMixin<T>
{
    private readonly T _element;
    private readonly int _elementIndent;

    [method: MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IndentationElementCompiler(scoped in T element)
    {
        var elementIndent = element.Indent;
        ArgumentOutOfRangeException.ThrowIfNegative(elementIndent, nameof(element.Indent));

        _elementIndent = elementIndent;
        _element = element;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public void Compile
    (
        ILanguageCompiler compiler,
        scoped ref Utf8Buffer buffer,
        int sourceIndent
    )
    {
        ArgumentOutOfRangeException.ThrowIfNegative(sourceIndent);
        var elementIndent = _elementIndent;
        var globalIndent = checked(sourceIndent + elementIndent);
        if (elementIndent == globalIndent) compiler.CompileElement(ref buffer, in _element);
        else compiler.CompileElement(ref buffer, T.Copy(in _element, sourceIndent));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public void Compile
    (
        ILanguageCompiler compiler,
        scoped ref Utf8Buffer buffer,
        int sourceIndent,
        int elementIndent
    )
    {
        ArgumentOutOfRangeException.ThrowIfNegative(sourceIndent);
        ArgumentOutOfRangeException.ThrowIfNegative(elementIndent);
        var globalIndent = checked(sourceIndent + elementIndent);
        if (_elementIndent == globalIndent) compiler.CompileElement(ref buffer, in _element);
        else compiler.CompileElement(ref buffer, T.Copy(in _element, sourceIndent));
    }
}
