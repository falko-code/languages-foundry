using Falko.Foundry.Elements;
using Falko.Foundry.Utf8Texts;

namespace Falko.Foundry.Compilers;

public interface IElementCompiler<TElement> where TElement : ILanguageElement
{
    void Compile
    (
        ILanguageCompiler compiler,
        scoped in TElement element,
        scoped ref Utf8Buffer buffer
    );
}
