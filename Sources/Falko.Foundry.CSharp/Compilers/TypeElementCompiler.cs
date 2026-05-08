using System.Runtime.CompilerServices;
using Falko.Foundry.Compilers;
using Falko.Foundry.CSharp.Elements;
using Falko.Foundry.Utf8Texts;

namespace Falko.Foundry.CSharp.Compilers;

internal sealed class TypeElementCompiler : IElementCompiler<TypeElement>
{
    private const int MinimumTypeLength = 64; // average type name length

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
        buffer.Append(CSharpLanguageConstants.DotChar);
        buffer.Append(typeName);

        if (genericTypes.Length is 0) return;

        buffer.Append(CSharpLanguageConstants.LeftBracketChar);

        var genericTypesSpan = genericTypes.AsSpan();

        AppendTypeWithGenerics(ref buffer, in genericTypesSpan[0]);

        for (var i = 1; i < genericTypesSpan.Length; i++)
        {
            buffer.Append(CSharpLanguageConstants.CommaChar);
            AppendTypeWithGenerics(ref buffer, in genericTypesSpan[i]);
        }

        buffer.Append(CSharpLanguageConstants.RightBracketChar);
    }
}
