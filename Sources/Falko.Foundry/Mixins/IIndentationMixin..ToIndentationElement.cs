using System.Runtime.CompilerServices;
using Falko.Foundry.Elements;

namespace Falko.Foundry.Mixins;

public static class IndentationMixinExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IndentationElement AsIndentationElement<T>(this T element)
        where T : ILanguageElement, IIndentationMixin<T>
    {
        return IndentationElement.Wrap(in element);
    }
}
