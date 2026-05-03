using Falko.Foundry.Elements;

namespace Falko.Foundry.Languages;

public interface ILanguageCompiler
{
    LanguageElementCompiler<TElement> GetElementCompiler<TElement>()
        where TElement : struct, ILanguageElement, allows ref struct;
}
