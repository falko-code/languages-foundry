using Falko.Foundry.Compilers;
using Falko.Foundry.CSharp.Elements;
using Falko.Foundry.Elements;
using Falko.Foundry.Utf8Texts;

namespace Falko.Foundry.CSharp.Compilers;

internal sealed class LineElementCompiler<T> : IElementCompiler<LineElement<T>> where T : ILanguageElement
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
        var spaceCount = padding * 4;
        var spacesLength = checked(space.Length * spaceCount);

        if (spacesLength > 0)
        {
            buffer.Allocate(spacesLength);
            buffer.Append(space, spaceCount);
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
