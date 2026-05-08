using Falko.Foundry.Caches;

namespace Falko.Foundry.Elements;

public interface ICacheElement<TSelfElement>
    where TSelfElement : notnull, ICacheElement<TSelfElement>, allows ref struct
{
    CompilerCacheFingerprint Cache { get; }

    static abstract TSelfElement Fingerprint
    (
        scoped in TSelfElement element,
        CompilerCacheFingerprint fingerprint
    );
}
