using Falko.Foundry.Compilers;
using Falko.Foundry.Elements;
using Falko.Foundry.Utf8Texts;

namespace Falko.Foundry.CSharp.Elements;

public static class IndentationElementMixinExtensions
{
    public static IIndentationCompiler AsCompilerIndentationElement<T>(this T element) where T : ILanguageElement, IIndentationElementMixin<T>
    {
        return new IndentationCompiler<T>(element);
    }
}

public interface IIndentationCompiler : ILanguageElement
{
    void Compile(ILanguageCompiler compiler, scoped ref Utf8Buffer buffer, int indent);
}

public sealed class IndentationCompiler<T>(T element) : IIndentationCompiler where T : ILanguageElement, IIndentationElementMixin<T>
{
    public void Compile(ILanguageCompiler compiler, scoped ref Utf8Buffer buffer, int indent)
    {
        compiler.CompileElement(ref buffer, T.MutateIndent(in element, indent));
    }

    public static implicit operator IndentationElement(IndentationCompiler<T> compiler)
    {
        return new IndentationElement(compiler);
    }
}
