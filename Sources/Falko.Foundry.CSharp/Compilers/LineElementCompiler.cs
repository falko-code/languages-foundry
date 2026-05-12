using Falko.Foundry.Compilers;
using Falko.Foundry.CSharp.Elements;
using Falko.Foundry.Elements;
using Falko.Foundry.Utf8Texts;

namespace Falko.Foundry.CSharp.Compilers;

public sealed class LineElementCompiler<T> : IElementCompiler<LineElement<T>> where T : ILanguageElement
{
    public void Compile
    (
        ILanguageCompiler compiler,
        scoped in LineElement<T> element,
        scoped ref Utf8Buffer buffer
    )
    {
        var padding = element.Padding;

        var space = CSharpLanguageConstants.Space;
        var spacesLength = space.Length * padding * 4;

        if (spacesLength > 0)
        {
            buffer.Allocate(spacesLength);

            for (var i = 0; i < padding * 4; i++)
            {
                buffer.Append(CSharpLanguageConstants.Space);
            }
        }

        var lineEnd = CSharpLanguageConstants.LineBreak;

        buffer.AllocateAppendCompilerElementOrCompile
        (
            compiler: compiler,
            element: element.Element,
            allocateAdditional: lineEnd.Length // we can not invoke allocate 2 times
        );

        buffer.Append(in lineEnd);
    }
}
