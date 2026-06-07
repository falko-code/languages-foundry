using System.Runtime.CompilerServices;
using Falko.Foundry.Compilers;
using Falko.Foundry.CSharp.Elements;
using Falko.Foundry.Exceptions;
using Falko.Foundry.Utf8Texts;

namespace Falko.Foundry.CSharp.Compilers;

internal sealed class TypeElementCompiler : IElementCompiler<TypeElement>
{
    private const int MinimumTypeLength = 64; // average type name length

    private static readonly int LeftRightAngleBracketsLength
        = CSharpLanguageConstants.LeftAngleBracket.Length + CSharpLanguageConstants.RightAngleBracket.Length;

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
        StructArgumentException.ThrowIfEmpty(typeName, nameof(element.Name));

        var genericTypes = element.GenericTypes;
        StructArgumentException.ThrowIfDefault(genericTypes, nameof(element.GenericTypes));

        var dot = CSharpLanguageConstants.Dot;
        var commaSpace = CSharpLanguageConstants.CommaSpace;

        var hasTypeNamespace = typeNamespace.IsEmpty is false;

        var dotBetweenLength = hasTypeNamespace ? dot.Length : 0;
        var typeLength = checked(typeNamespace.Length + typeName.Length + dotBetweenLength);
        var genericTypesCount = genericTypes.Length;

        var hasGenericTypes = genericTypesCount is not 0;

        if (hasGenericTypes)
        {
            typeLength += LeftRightAngleBracketsLength; // for generic type brackets
            typeLength += checked(MinimumTypeLength * genericTypesCount); // for do fewer allocations when appending generic types
            typeLength += commaSpace.Length * (genericTypesCount - 1); // for comma and space between generic types
        }

        buffer.Allocate(typeLength);

        if (hasTypeNamespace)
        {
            buffer.Append(typeNamespace);
            buffer.Append(dot); // namespace and type name separator
        }

        buffer.Append(typeName);

        if (hasGenericTypes is false) return;

        buffer.Append(CSharpLanguageConstants.LeftAngleBracket);

        var genericTypesSpan = genericTypes.AsSpan();
        var genericTypeIndex = 0;

        genericTypeAppendLoop:

        buffer.AllocateAppendCompilerElementOrAction
        (
            element: genericTypesSpan[genericTypeIndex],
            action: AppendTypeWithGenerics
        );

        if (++genericTypeIndex < genericTypesCount)
        {
            buffer.Append(commaSpace);
            goto genericTypeAppendLoop;
        }

        buffer.Append(CSharpLanguageConstants.RightAngleBracket);
    }
}
