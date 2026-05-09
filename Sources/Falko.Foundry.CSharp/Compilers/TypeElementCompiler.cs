using System.Runtime.CompilerServices;
using Falko.Foundry.Compilers;
using Falko.Foundry.CSharp.Elements;
using Falko.Foundry.Exceptions;
using Falko.Foundry.Utf8Texts;

namespace Falko.Foundry.CSharp.Compilers;

internal sealed class TypeElementCompiler : IElementCompiler<TypeElement>
{
    private const int MinimumTypeLength = 64; // average type name length

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
        CompilerException.ThrowIfEmptyOrDefault(typeName, nameof(element.Name));

        var genericTypes = element.GenericTypes;
        CompilerException.ThrowIfDefault(genericTypes, nameof(element.GenericTypes));

        var hasTypeNamespace = typeNamespace.IsEmpty is false;

        var dotBetweenLength = hasTypeNamespace ? Utf8Char.Length : 0;
        var typeLength = typeNamespace.Length + typeName.Length + dotBetweenLength;
        var genericTypesCount = genericTypes.Length;

        if (genericTypesCount is not 0)
        {
            typeLength = Math.Max(typeLength, MinimumTypeLength); // if current type is too short
            var typeLengthWithGenerics = typeLength + MinimumTypeLength * genericTypesCount;
            typeLength += typeLengthWithGenerics + 2; // 2 is for brackets
        }

        buffer.Allocate(typeLength);

        if (hasTypeNamespace)
        {
            buffer.Append(typeNamespace);
            buffer.Append(CSharpLanguageConstants.Dot); // namespace and type name separator
        }

        buffer.Append(typeName);

        if (genericTypesCount is 0) return;

        buffer.Append(CSharpLanguageConstants.LeftAngleBracket);

        var genericTypesSpan = genericTypes.AsSpan();
        var genericTypeIndex = 0;

        genericTypeAppendLoop:

        AppendTypeWithGenerics(ref buffer, in genericTypesSpan[genericTypeIndex]);

        if (++genericTypeIndex < genericTypesCount)
        {
            buffer.Append(CSharpLanguageConstants.Comma);
            goto genericTypeAppendLoop;
        }

        buffer.Append(CSharpLanguageConstants.RightAngleBracket);
    }
}
