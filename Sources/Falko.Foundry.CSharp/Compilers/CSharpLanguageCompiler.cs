using System.Runtime.CompilerServices;
using Falko.Foundry.Compilers;
using Falko.Foundry.CSharp.Elements;

namespace Falko.Foundry.CSharp.Compilers;

public sealed class CSharpLanguageCompiler : LanguageCompiler<CSharpLanguageCompiler>
{
    public static readonly CSharpLanguageCompiler Instance = new();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private CSharpLanguageCompiler()
    {
        SetElementCompiler<TypeElementCompiler, TypeElement>();
    }
}
