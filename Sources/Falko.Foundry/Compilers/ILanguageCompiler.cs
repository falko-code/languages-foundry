using Falko.Foundry.Elements;

namespace Falko.Foundry.Compilers;

public interface ILanguageCompiler
{
    LanguageElementCompiler<TElement> GetElementCompiler<TElement>()
        where TElement : struct, ILanguageElement, allows ref struct;
}
