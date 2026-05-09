using System.Runtime.CompilerServices;
using Falko.Foundry.Common;
using Falko.Foundry.Compilers;
using Falko.Foundry.CSharp.Elements;
using Falko.Foundry.Utf8Texts;

namespace Falko.Foundry.CSharp.Compilers;

public sealed class TypeIdentifierElementCompiler : IElementCompiler<TypeIdentifierElement>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Compile
    (
        ILanguageCompiler compiler,
        scoped in TypeIdentifierElement element,
        scoped ref Utf8Buffer buffer
    )
    {
        CompilerException.ThrowIfDefault(element);

        var name = element.Name;
        CompilerException.ThrowIfEmptyOrDefault(name, nameof(element.Name));

        var postfixLength = Utf8Char.Length + element.Name.Length; // 1 for space between

        buffer.AllocateAppendCompilerElementOrCompile
        (
            compiler: compiler,
            element: element.Type,
            allocateAdditional: postfixLength // we can not invoke allocate 2 times
        );

        buffer.Append(CSharpLanguageConstants.Space);
        buffer.Append(element.Name);
    }
}
