using Falko.Foundry.Compilers;
using Falko.Foundry.CSharp.Elements;
using Falko.Foundry.Utf8Texts;

namespace Falko.Foundry.CSharp.Compilers;

internal sealed class LineElementCompiler : IElementCompiler<LineElement>
{
    public void Compile
    (
        ILanguageCompiler compiler,
        scoped in LineElement element,
        scoped ref Utf8Buffer buffer
    )
    {
        var lineEnd = CSharpLanguageConstants.LineEnd;
        buffer.Allocate(lineEnd.Length);
        buffer.Append(in lineEnd);
    }
}
