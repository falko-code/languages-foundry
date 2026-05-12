using Falko.Foundry.Elements;

namespace Falko.Foundry.CSharp.Elements;

public static class TypeElementExtensions
{
    public static LineElement<T> AsLine<T>
    (
        this T element,
        int padding = 0
    ) where T : ILanguageElement
    {
        return new LineElement<T>
        {
            Element = element,
            Padding = padding
        };
    }
}
