using System.Runtime.CompilerServices;
using Falko.Foundry.Elements;
using Falko.Foundry.Utf8Texts;

namespace Falko.Foundry.Compilers;

[method: MethodImpl(MethodImplOptions.AggressiveInlining)]
public readonly ref struct LanguageElementCompiler<TElement>
(
    ILanguageCompiler languageCompiler,
    IElementCompiler<TElement> elementCompiler
) where TElement : struct, ILanguageElement, allows ref struct
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Compile
    (
        scoped ref Utf8Buffer buffer,
        scoped in TElement element
    )
    {
        elementCompiler.Compile(languageCompiler, in element, ref buffer);
    }
}
