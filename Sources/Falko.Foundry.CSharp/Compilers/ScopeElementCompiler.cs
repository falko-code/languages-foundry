using Falko.Foundry.Common;
using Falko.Foundry.Compilers;
using Falko.Foundry.CSharp.Elements;
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
        CompileArgumentException.ThrowIfDefault(element);

        var elements = element.Elements;
        CompileArgumentException.ThrowIfDefault(elements);

        var elementIndent = element.Indent;
        var space = CSharpLanguageConstants.Space;
        var spaceCount = elementIndent * 4;

        if (spaceCount > 0)
        {
            buffer.Allocate(space.Length, spaceCount);
            buffer.Append(space, spaceCount);
        }

        if (elements.IsEmpty)
        {
            Utf8String emptyScope = "{ }\n"u8;
            buffer.Allocate(emptyScope.Length);
            buffer.Append(in emptyScope);
            return;
        }

        var lineEnd = CSharpLanguageConstants.LineEnd;

        var leftBracket = CSharpLanguageConstants.LeftBracket;
        buffer.Allocate(leftBracket.Length + lineEnd.Length);
        buffer.Append(in leftBracket);
        buffer.Append(in lineEnd);

        var elementsIndent = elementIndent + 1;
        foreach (var indentationElement in elements)
        {
            indentationElement.Compiler.Compile(compiler, ref buffer, elementsIndent);
        }

        if (spaceCount > 0)
        {
            buffer.Allocate(space.Length, spaceCount);
            buffer.Append(space, spaceCount);
        }

        var rightBracket = CSharpLanguageConstants.RightBracket;
        buffer.Allocate(leftBracket.Length + lineEnd.Length);
        buffer.Append(in rightBracket);
        buffer.Append(in lineEnd);
    }
}
