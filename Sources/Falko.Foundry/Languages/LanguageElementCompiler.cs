using System.Runtime.CompilerServices;
using Falko.Foundry.Buffers;
using Falko.Foundry.Elements;

namespace Falko.Foundry.Languages;

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
        scoped in TElement element,
        scoped ref SpanByteBuffer buffer
    )
    {
        elementCompiler.Compile(languageCompiler, in element, ref buffer);
    }
}
