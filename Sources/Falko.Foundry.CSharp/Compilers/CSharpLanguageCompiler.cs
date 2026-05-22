using Falko.Foundry.Compilers;
using Falko.Foundry.CSharp.Elements;
using Falko.Foundry.Mixins;

namespace Falko.Foundry.CSharp.Compilers;

public sealed class CSharpLanguageCompiler : LanguageCompiler<CSharpLanguageCompiler>,
    ISingletonMixin<CSharpLanguageCompiler>
{
    public static CSharpLanguageCompiler Instance { get; } = new();

    private CSharpLanguageCompiler()
    {
        SetElementCompiler<TypeElementCompiler, TypeElement>();
        SetElementCompiler<TypeIdentifierElementCompiler, TypeIdentifierElement>();
        SetElementCompiler<UsingNamespaceElementCompiler, UsingNamespaceElement>();

        SetElementCompiler<ScopeElementCompiler, ScopeElement>();

        SetElementCompiler<LineElementCompiler, LineElement>();

        SetElementCompiler<LineElementCompiler<TypeIdentifierElement>, LineElement<TypeIdentifierElement>>();
        SetElementCompiler<LineElementCompiler<UsingNamespaceElement>, LineElement<UsingNamespaceElement>>();
    }
}
