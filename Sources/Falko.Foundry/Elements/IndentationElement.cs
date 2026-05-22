using System.Runtime.CompilerServices;
using Falko.Foundry.Compilers;
using Falko.Foundry.Mixins;

namespace Falko.Foundry.Elements;

[method: MethodImpl(MethodImplOptions.AggressiveInlining)]
public readonly struct IndentationElement(IIndentationElementCompiler compiler)
    : ILanguageElement, IStructInitMixin<IndentationElement>
{
    public bool IsInit { get; } = true;

    public IIndentationElementCompiler Compiler
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => compiler;
    }
}
