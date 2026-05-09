using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Falko.Foundry.Elements;

namespace Falko.Foundry.Caches;

public readonly struct CompilerOrLanguageElement<TElement> : ILanguageElement where TElement : ILanguageElement
{
    private readonly CompilerElement<TElement> _compilerElement;

    private readonly TElement? _languageElement;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CompilerOrLanguageElement(CompilerElement<TElement> compilerElement)
    {
        _compilerElement = compilerElement;
        _languageElement = default;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CompilerOrLanguageElement(TElement languageElement)
    {
        _compilerElement = default;
        _languageElement = languageElement;
    }

    public bool IsInit { get; } = true;

    public bool HasCompilerElement
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => IsInit && _compilerElement.IsInit;
    }

    public bool HasLanguageElement
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => IsInit && _compilerElement.IsInit is false;
    }

    public CompilerElement<TElement> CompilerElement
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => HasCompilerElement
            ? _compilerElement
            : throw new InvalidOperationException("No compiler element available.");
    }

    public TElement LanguageElement
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => HasLanguageElement
            ? _languageElement!
            : throw new InvalidOperationException("No language element available.");
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetCompilerElement(out CompilerElement<TElement> compilerElement)
    {
        if (HasCompilerElement)
        {
            compilerElement = _compilerElement;
            return true;
        }

        compilerElement = default;
        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetLanguageElement([MaybeNullWhen(false)] out TElement languageElement)
    {
        if (HasLanguageElement)
        {
            languageElement = _languageElement!;
            return true;
        }

        languageElement = default;
        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator CompilerOrLanguageElement<TElement>(TElement languageElement)
    {
        return new CompilerOrLanguageElement<TElement>(languageElement);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator CompilerOrLanguageElement<TElement>(CompilerElement<TElement> compilerElement)
    {
        return new CompilerOrLanguageElement<TElement>(compilerElement);
    }
}
