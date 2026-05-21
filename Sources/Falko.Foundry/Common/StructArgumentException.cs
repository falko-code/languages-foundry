using System.Runtime.CompilerServices;

namespace Falko.Foundry.Common;

public static class StructArgumentException
{
    public static void ThrowIfNotInit<T>
    (
        scoped in T element,
        [CallerArgumentExpression(nameof(element))] string? paramName = null
    ) where T : struct, ISafeStruct, allows ref struct
    {
        if (element.IsInit is false)
        {
            throw new ArgumentException("Structure must be initialized with constructor.", paramName);
        }
    }
}
