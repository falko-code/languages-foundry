using System.Runtime.CompilerServices;
using Falko.Foundry.CSharp.Elements;
using Falko.Foundry.Languages;

namespace Falko.Foundry.CSharp.Languages;

public sealed class CSharpLanguageCompiler : LanguageCompiler<CSharpLanguageCompiler>
{
    public static readonly CSharpLanguageCompiler Instance = new();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private CSharpLanguageCompiler()
    {
        SetElementCompiler<TypeElementCompiler, TypeElement>();
    }
}
