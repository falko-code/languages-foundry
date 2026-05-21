using Falko.Foundry.Common;
using Falko.Foundry.Compilers;
using Falko.Foundry.CSharp.Elements;
using Falko.Foundry.CSharp.Utf8Texts;
using Falko.Foundry.Utf8Texts;

namespace Falko.Foundry.CSharp.Compilers;

internal sealed class ScopeElementCompiler : IElementCompiler<ScopeElement>
{
    public void Compile
    (
        ILanguageCompiler compiler,
        scoped in ScopeElement element,
        scoped ref Utf8Buffer buffer
    )
    {
        StructArgumentException.ThrowIfNotInit(element);

        var elements = element.Elements;
        CompileArgumentException.ThrowIfDefault(elements);

        var lineEnd = CSharpLanguageConstants.LineEnd;
        var elementIndent = element.Indent;

        buffer.AllocateAppendIndent(elementIndent);
        var leftBracket = CSharpLanguageConstants.LeftBracket;
        buffer.Allocate(leftBracket.Length + lineEnd.Length);
        buffer.Append(in leftBracket);
        buffer.Append(in lineEnd);

        var elementsIndent = elementIndent + 1;
        foreach (var indentationElement in elements)
        {
            indentationElement.Compiler.Compile(compiler, ref buffer, elementsIndent);
        }

        buffer.AllocateAppendIndent(elementIndent);
        var rightBracket = CSharpLanguageConstants.RightBracket;
        buffer.Allocate(leftBracket.Length + lineEnd.Length);
        buffer.Append(in rightBracket);
        buffer.Append(in lineEnd);
    }
}
