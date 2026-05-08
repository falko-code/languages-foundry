using System.Runtime.CompilerServices;
using Falko.Foundry.Compilers;
using Falko.Foundry.CSharp.Elements;
using Falko.Foundry.Elements;
using Falko.Foundry.Exceptions;
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
        var name = element.Name;
        CompilerException.ThrowIfEmptyOrDefault(name, nameof(element.Name));

        var type = element.Type;
        compiler.AppendFromCacheOrCompile(ref buffer, in type);

        buffer.Allocate(Utf8Char.Length + element.Name.Length); // 1 is for space between

        buffer.Append(CSharpLanguageConstants.Space);
        buffer.Append(element.Name);
    }
}
