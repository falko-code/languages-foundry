using System.Runtime.CompilerServices;
using Falko.Foundry.Compilers;
using Falko.Foundry.CSharp.Elements;
using Falko.Foundry.Elements;
using Falko.Foundry.Utf8Texts;

namespace Falko.Foundry.CSharp.Compilers;

[method: MethodImpl(MethodImplOptions.AggressiveInlining)]
public sealed class IndentationElementCompiler<T>(T element) : IIndentationElementCompiler
    where T : ILanguageElement, IIndentationElementMixin<T>
{
    public void Compile(ILanguageCompiler compiler, scoped ref Utf8Buffer buffer, int indent)
    {
        if (indent < 0) indent = 0;
        if (element.Indent == indent) compiler.CompileElement(ref buffer, in element);
        else compiler.CompileElement(ref buffer, T.Copy(in element, indent));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator IndentationElement(IndentationElementCompiler<T> compiler)
    {
        return new IndentationElement(compiler);
    }
}
