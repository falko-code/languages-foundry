using System.Runtime.CompilerServices;
using Falko.Foundry.Exceptions;
using Falko.Foundry.Mixins;
using Falko.Foundry.Utf8Texts;

namespace Falko.Foundry.Elements;

// ReSharper disable once UnusedTypeParameter

public readonly struct CompilerElement<TCompiledElement> : ILanguageElement,
    IStructInitMixin<CompilerElement<TCompiledElement>>
        where TCompiledElement : ILanguageElement
{
    private readonly Utf8String _elementText;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CompilerElement(Utf8String elementText)
    {
        DebugArgumentException.ThrowIfDebug
        (
            throwIf: StructArgumentException.ThrowIfEmpty, elementText
        );

        _elementText = elementText;
    }

    public bool IsInit => _elementText.IsEmpty is false;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Utf8String AsString() => _elementText;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ReadOnlySpan<byte> AsSpan() => _elementText.AsSpan();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString() => _elementText.ToString();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Utf8String(CompilerElement<TCompiledElement> compilerElement)
    {
        return compilerElement.AsString();
    }
}
