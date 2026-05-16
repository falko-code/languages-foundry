using System.Runtime.CompilerServices;
using Falko.Foundry.Common;
using Falko.Foundry.Compilers;

namespace Falko.Foundry.Elements;

[method: MethodImpl(MethodImplOptions.AggressiveInlining)]
public readonly struct IndentationElement(IIndentationElementCompiler compiler)
    : ILanguageElement, ISafeStruct
{
    public bool IsInit { get; } = true;

    public IIndentationElementCompiler Compiler
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => compiler;
    }
}
