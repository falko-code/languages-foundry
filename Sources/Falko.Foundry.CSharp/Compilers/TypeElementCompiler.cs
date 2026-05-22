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
        CompileArgumentException.ThrowIfEmpty(typeName, nameof(element.Name));

        var genericTypes = element.GenericTypes;
        CompileArgumentException.ThrowIfDefault(genericTypes, nameof(element.GenericTypes));

        var leftAngleBracket = CSharpLanguageConstants.LeftAngleBracket;
        var rightAngleBracket = CSharpLanguageConstants.RightAngleBracket;
        var dot = CSharpLanguageConstants.Dot;
        var commaSpace = CSharpLanguageConstants.CommaSpace;

        var hasTypeNamespace = typeNamespace.IsEmpty is false;

        var dotBetweenLength = hasTypeNamespace ? dot.Length : 0;
        var typeLength = typeNamespace.Length + typeName.Length + dotBetweenLength;
        var genericTypesCount = genericTypes.Length;

        if (genericTypesCount is not 0)
        {
            typeLength += leftAngleBracket.Length + rightAngleBracket.Length; // for generic type brackets
            typeLength += checked(MinimumTypeLength * genericTypesCount); // for do fewer allocations when appending generic types
            typeLength += commaSpace.Length * (genericTypesCount - 1); // for comma and space between generic types
        }

        buffer.Allocate(typeLength);

        if (hasTypeNamespace)
        {
            buffer.Append(in typeNamespace);
            buffer.Append(in dot); // namespace and type name separator
        }

        buffer.Append(in typeName);

        if (genericTypesCount is 0) return;

        buffer.Append(in leftAngleBracket);

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
            buffer.Append(in commaSpace);
            goto genericTypeAppendLoop;
        }

        buffer.Append(in rightAngleBracket);
    }
}
