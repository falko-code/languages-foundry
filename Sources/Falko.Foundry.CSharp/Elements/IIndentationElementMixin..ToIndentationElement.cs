using System.Runtime.CompilerServices;
using Falko.Foundry.CSharp.Compilers;
using Falko.Foundry.Elements;

namespace Falko.Foundry.CSharp.Elements;

public static class IndentationElementMixinExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IIndentationElementCompiler ToIndentationElement<T>(this T element)
        where T : ILanguageElement, IIndentationElementMixin<T>
    {
        return new IndentationElementCompiler<T>(element);
    }
}
