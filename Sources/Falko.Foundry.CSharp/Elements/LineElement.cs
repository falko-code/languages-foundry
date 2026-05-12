using Falko.Foundry.Caches;
using Falko.Foundry.Elements;

namespace Falko.Foundry.CSharp.Elements;

public readonly struct LineElement<T> : IPaddingElement where T : ILanguageElement
{
    public required CompilerOrLanguageElement<T> Element { get; init; }

    public int Padding { get; init; }
}
