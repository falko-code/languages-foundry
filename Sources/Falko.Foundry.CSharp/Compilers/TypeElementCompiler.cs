using Falko.Foundry.Buffers;
using Falko.Foundry.Compilers;
using Falko.Foundry.CSharp.Elements;

namespace Falko.Foundry.CSharp.Compilers;

internal sealed class TypeElementCompiler : IElementCompiler<TypeElement>
{
    public void Compile
    (
        ILanguageCompiler compiler,
        scoped in TypeElement element,
        scoped ref SpanByteBuffer buffer
    )
    {
        // TODO: Implement TypeElement compilation for C#
    }
}
