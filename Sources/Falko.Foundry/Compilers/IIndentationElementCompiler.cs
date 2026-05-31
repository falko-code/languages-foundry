using Falko.Foundry.Utf8Texts;

namespace Falko.Foundry.Compilers;

public interface IIndentationElementCompiler
{
    void Compile(ILanguageCompiler compiler, scoped ref Utf8Buffer buffer, int sourceIndent);

    void Compile(ILanguageCompiler compiler, scoped ref Utf8Buffer buffer, int sourceIndent, int elementIndent);
}
