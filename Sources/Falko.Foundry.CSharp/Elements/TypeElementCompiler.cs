using Falko.Foundry.Buffers;
using Falko.Foundry.Elements;
using Falko.Foundry.Languages;

namespace Falko.Foundry.CSharp.Elements;

public sealed class TypeElementCompiler : IElementCompiler<TypeElement>
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
