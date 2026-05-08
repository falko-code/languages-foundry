using System.Runtime.CompilerServices;
using Falko.Foundry.Compilers;
using Falko.Foundry.CSharp.Elements;
using Falko.Foundry.Utf8Texts;

namespace Falko.Foundry.CSharp.Compilers;

internal sealed class TypeElementCompiler : IElementCompiler<TypeElement>
{
    private static readonly Utf8Char DotChar = "."u8;

    private static readonly Utf8Char CommaChar = ","u8;

    private static readonly Utf8Char LeftBracketChar = "<"u8;

    private static readonly Utf8Char RightBracketChar = ">"u8;

    private static readonly int MinimumTypeLength = GetMinimumTypeLength();

    public void Compile
    (
        ILanguageCompiler compiler,
        scoped in TypeElement element,
        scoped ref Utf8Buffer buffer
    )
    {
        AppendTypeWithGenerics(ref buffer, in element);
    }

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.AggressiveOptimization)]
    private static void AppendTypeWithGenerics
    (
        scoped ref Utf8Buffer buffer,
        scoped in TypeElement element
    )
    {
        var typeNamespace = element.Namespace;
        var typeName = element.Name;
        var genericTypes = element.GenericTypes;

        var dotLength = typeNamespace.IsEmpty ? 0 : Utf8Char.Length;
        var typeLength = typeNamespace.Length + typeName.Length + dotLength;

        if (genericTypes.Length is not 0)
        {
            typeLength = Math.Max(typeLength, MinimumTypeLength); // if current type is too short
            var typeLengthWithGenerics = typeLength * (1 + genericTypes.Length); // 1 is for current
            typeLength += typeLengthWithGenerics + 2; // 2 is for brackets
        }

        buffer.Allocate(typeLength);

        buffer.Append(typeNamespace);
        buffer.Append(DotChar);
        buffer.Append(typeName);

        if (genericTypes.Length is 0) return;

        buffer.Append(LeftBracketChar);

        var genericTypesSpan = genericTypes.AsSpan();

        AppendTypeWithGenerics(ref buffer, in genericTypesSpan[0]);

        for (var i = 1; i < genericTypesSpan.Length; i++)
        {
            buffer.Append(CommaChar);
            AppendTypeWithGenerics(ref buffer, in genericTypesSpan[i]);
        }

        buffer.Append(RightBracketChar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int GetMinimumTypeLength()
    {
        var length = 1;

        length += GetMinimumNamespaceLength(nameof(Falko));
        length += GetMinimumNamespaceLength(nameof(Foundry));
        length += GetMinimumNamespaceLength(nameof(CSharp));
        length += GetMinimumNamespaceLength(nameof(Compilers));
        length += GetMinimumNamespaceLength(nameof(TypeElementCompiler));

        return length;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int GetMinimumNamespaceLength(string name)
    {
        return name.Length + Utf8Char.Length; // Name + Dot
    }
}
