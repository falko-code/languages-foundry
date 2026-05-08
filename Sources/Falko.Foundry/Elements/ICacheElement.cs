using Falko.Foundry.Caches;

namespace Falko.Foundry.Elements;

public interface ICacheElement<TSelfElement> : ILanguageElement
    where TSelfElement : ICacheElement<TSelfElement>, allows ref struct
{
    CompilerCache Cache { get; }

    static abstract TSelfElement CacheCopy
    (
        scoped in TSelfElement element,
        CompilerCache cache
    );
}
