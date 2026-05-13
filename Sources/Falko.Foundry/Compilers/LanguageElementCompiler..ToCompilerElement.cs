using System.Runtime.CompilerServices;
using Falko.Foundry.Elements;
using Falko.Foundry.Utf8Texts;

namespace Falko.Foundry.Compilers;

public static partial class LanguageElementCompilerExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static CompilerElement<T> ToCompilerElement<T>
    (
        this Utf8String elementText
    ) where T : ILanguageElement
    {
        return new CompilerElement<T>(elementText);
    }
}
