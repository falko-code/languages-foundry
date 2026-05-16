using Falko.Foundry.Compilers;
using Falko.Foundry.Utf8Texts;

namespace Falko.Foundry.CSharp.Compilers;

public interface IIndentationElementCompiler
{
    void Compile(ILanguageCompiler compiler, scoped ref Utf8Buffer buffer, int indent);
}
