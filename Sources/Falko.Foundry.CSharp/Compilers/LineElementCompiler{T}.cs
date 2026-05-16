using Falko.Foundry.Compilers;
using Falko.Foundry.CSharp.Elements;
using Falko.Foundry.CSharp.Utf8Texts;
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
        buffer.AllocateAppendIndent(element.Indent);

        var lineBreak = CSharpLanguageConstants.LineBreak;

        buffer.AllocateAppendCompilerElementOrCompile
        (
            compiler: compiler,
            element: element.Element,
            allocateAdditional: lineBreak.Length // we can not invoke allocate 2 times
        );

        buffer.Append(in lineBreak);
    }
}
