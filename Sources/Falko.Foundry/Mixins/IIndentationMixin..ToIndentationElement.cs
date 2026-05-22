using System.Runtime.CompilerServices;
using Falko.Foundry.Compilers;
using Falko.Foundry.Elements;

namespace Falko.Foundry.Mixins;

public static class IndentationMixinExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IndentationElement ToIndentationElement<T>(this T element)
        where T : ILanguageElement, IIndentationMixin<T>
    {
        return new IndentationElement(new IndentationElementCompiler<T>(in element));
    }
}
