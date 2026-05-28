using System.Runtime.CompilerServices;
using Falko.Foundry.Compilers;
using Falko.Foundry.CSharp.Elements;
using Falko.Foundry.Exceptions;
using Falko.Foundry.Utf8Texts;

namespace Falko.Foundry.CSharp.Compilers;

internal sealed class TypeIdentifierElementCompiler : IElementCompiler<TypeIdentifierElement>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Compile
    (
        ILanguageCompiler compiler,
        scoped in TypeIdentifierElement element,
        scoped ref Utf8Buffer buffer
    )
    {
        StructArgumentException.ThrowIfNotInit(in element);

        var name = element.Name;
        StructArgumentException.ThrowIfEmpty(name, nameof(element.Name));

        var space = CSharpLanguageConstants.Space;
        var postfixLength = space.Length + name.Length;

        buffer.AllocateAppendCompilerElementOrCompile
        (
            compiler: compiler,
            element: element.Type,
            allocateAdditional: postfixLength // we can not invoke allocate 2 times
        );

        buffer.Append(space);
        buffer.Append(name);
    }
}
