using System.Runtime.CompilerServices;
using Falko.Foundry.Common;
using Falko.Foundry.Utf8Texts;

namespace Falko.Foundry.Elements;

// ReSharper disable once UnusedTypeParameter
[method: MethodImpl(MethodImplOptions.AggressiveInlining)]
public readonly struct CompilerElement<TCompiledElement>(Utf8String elementText)
    : ILanguageElement, ISafeStruct where TCompiledElement : ILanguageElement
{
    public bool IsInit { get; } = true;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Utf8String AsString() => elementText;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ReadOnlySpan<byte> AsSpan() => elementText.AsSpan();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString() => elementText.ToString();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Utf8String(CompilerElement<TCompiledElement> compilerElement)
    {
        return compilerElement.AsString();
    }
}
