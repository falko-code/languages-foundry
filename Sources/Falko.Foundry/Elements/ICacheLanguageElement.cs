using Falko.Foundry.Caches;

namespace Falko.Foundry.Elements;

public interface ICacheLanguageElement<TSelf> : ILanguageElement
    where TSelf : ICacheLanguageElement<TSelf>, allows ref struct
{
    CompilerCache Cache { get; }

    static abstract TSelf CacheCopy
    (
        scoped in TSelf element,
        CompilerCache cache
    );
}
