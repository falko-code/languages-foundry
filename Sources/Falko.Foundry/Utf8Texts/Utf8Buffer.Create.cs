using System.Runtime.CompilerServices;

namespace Falko.Foundry.Utf8Texts;

public ref partial struct Utf8Buffer
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Utf8Buffer Create(int capacity = MaxHeapBufferSize)
    {
        ThrowIfDebug(ArgumentOutOfRangeException.ThrowIfNegative, capacity);
        capacity = Math.Max(capacity, MinArrayBufferSize); // if array we can't use less capacity
        return new Utf8Buffer(capacity);
    }
}
