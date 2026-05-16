using Falko.Foundry.Compilers;
using Falko.Foundry.Elements;
using Falko.Foundry.Utf8Texts;

namespace Falko.Foundry.CSharp.Elements;

public static class IndentationElementMixinExtensions
{
    public static IndentationCompilerAction AsCompilerAction<T>(this T element) where T : ILanguageElement, IIndentationElementMixin<T>
    {
        return (compiler, scoped ref buffer, indent) => compiler.CompileElement(ref buffer, T.MutateIndent(in element, indent));
    }
}

public delegate void IndentationCompilerAction(ILanguageCompiler compiler, scoped ref Utf8Buffer buffer, int indent);
