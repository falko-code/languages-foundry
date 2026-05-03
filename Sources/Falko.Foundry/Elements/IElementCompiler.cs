using Falko.Foundry.Buffers;
using Falko.Foundry.Languages;

namespace Falko.Foundry.Elements;

public interface IElementCompiler<TElement> where TElement : struct, ILanguageElement, allows ref struct
{
    void Compile
    (
        ILanguageCompiler compiler,
        scoped in TElement element,
        scoped ref SpanByteBuffer buffer
    );
}
