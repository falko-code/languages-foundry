using Falko.Foundry.Common;
using Falko.Foundry.Compilers;
using Falko.Foundry.CSharp.Elements;
using Falko.Foundry.Utf8Texts;

namespace Falko.Foundry.CSharp.Compilers;

internal sealed class UsingNamespaceElementCompiler : IElementCompiler<UsingNamespaceElement>
{
    public void Compile(ILanguageCompiler compiler, scoped in UsingNamespaceElement element, scoped ref Utf8Buffer buffer)
    {
        CompileArgumentException.ThrowIfDefault(element);

        var nameSpace = element.Namespace;
        CompileArgumentException.ThrowIfEmpty(nameSpace, nameof(element.Namespace));

        var usingNamespace = CSharpLanguageConstants.UsingNamespace;

        buffer.Allocate(usingNamespace.Length + nameSpace.Length);
        buffer.Append(in usingNamespace);
        buffer.Append(in nameSpace);
    }
}
