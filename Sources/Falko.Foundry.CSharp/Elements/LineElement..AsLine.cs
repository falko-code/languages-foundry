using System.Runtime.CompilerServices;
using Falko.Foundry.Elements;

namespace Falko.Foundry.CSharp.Elements;

public static class LineElementExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LineElement<T> AsLine<T>
    (
        this T element,
        int indent = 0
    ) where T : ILanguageElement
    {
        return new LineElement<T>
        {
            Element = element,
            Indent = indent
        };
    }
}
