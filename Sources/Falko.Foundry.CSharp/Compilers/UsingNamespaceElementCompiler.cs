using Falko.Foundry.Compilers;
using Falko.Foundry.CSharp.Elements;
using Falko.Foundry.Exceptions;
using Falko.Foundry.Utf8Texts;

namespace Falko.Foundry.CSharp.Compilers;

internal sealed class UsingNamespaceElementCompiler : IElementCompiler<UsingNamespaceElement>
{
    public void Compile(ILanguageCompiler compiler, scoped in UsingNamespaceElement element, scoped ref Utf8Buffer buffer)
    {
        StructArgumentException.ThrowIfNotInit(in element);

        var nameSpace = element.Namespace;
        StructArgumentException.ThrowIfEmpty(nameSpace, nameof(element.Namespace));

        var usingNamespace = CSharpLanguageConstants.UsingNamespace;

        buffer.Allocate(usingNamespace.Length + nameSpace.Length);
        buffer.Append(usingNamespace);
        buffer.Append(nameSpace);
    }
}
