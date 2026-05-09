using System.Runtime.CompilerServices;
using Falko.Foundry.Utf8Texts;

namespace Falko.Foundry.Elements;

// ReSharper disable once UnusedTypeParameter
[method: MethodImpl(MethodImplOptions.AggressiveInlining)]
public readonly struct CompilerElement<TCompiledElement>
(
    Utf8String elementText
) : ILanguageElement where TCompiledElement : ILanguageElement
{
    public bool IsInit { get; } = true;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Utf8String AsString() => elementText;
}
