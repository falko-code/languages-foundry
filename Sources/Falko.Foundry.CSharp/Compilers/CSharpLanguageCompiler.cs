using Falko.Foundry.Compilers;
using Falko.Foundry.CSharp.Elements;

namespace Falko.Foundry.CSharp.Compilers;

public sealed class CSharpLanguageCompiler : LanguageCompiler<CSharpLanguageCompiler>
{
    public static readonly CSharpLanguageCompiler Instance = new();

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
