using System.Runtime.CompilerServices;
using Falko.Foundry.Elements;
using Falko.Foundry.Exceptions;
using Falko.Foundry.Mixins;
using Falko.Foundry.Utf8Texts;

namespace Falko.Foundry.Compilers;

public sealed class IndentationElementCompiler<T> : IIndentationElementCompiler
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

    public void Compile(ILanguageCompiler compiler, scoped ref Utf8Buffer buffer, int indent)
    {
        DebugArgumentException.ThrowIfDebug
        (
            throwIf: ArgumentOutOfRangeException.ThrowIfNegative, indent
        );

        if (_elementIndent == indent) compiler.CompileElement(ref buffer, in _element);
        else compiler.CompileElement(ref buffer, T.Copy(in _element, indent));
    }
}
