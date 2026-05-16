using System.Runtime.CompilerServices;
using Falko.Foundry.Common;
using Falko.Foundry.Elements;
using Falko.Foundry.Utf8Texts;

namespace Falko.Foundry.CSharp.Elements;

[method: MethodImpl(MethodImplOptions.AggressiveInlining)]
public readonly struct UsingNamespaceElement() : ILanguageElement, ISafeStruct
{
    public bool IsInit { get; } = true;


    public required Utf8String Namespace { get; init; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator UsingNamespaceElement(Utf8String namespaceText)
    {
        return new UsingNamespaceElement { Namespace = namespaceText };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator UsingNamespaceElement(ReadOnlySpan<byte> namespaceTextUtf8Bytes)
    {
        return new UsingNamespaceElement { Namespace = namespaceTextUtf8Bytes };
    }
}
