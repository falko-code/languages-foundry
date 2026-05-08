using System.Runtime.CompilerServices;

namespace Falko.Foundry.Utf8Texts;

public ref struct Utf8Buffer : IDisposable
{
    public const int StackAllocationThreshold = 256;

    private Utf8Buffer(Span<byte> stackAllocatedBuffer)
    {
        throw new NotImplementedException();
    }

    public Utf8Buffer(int capacity)
    {
        throw new NotImplementedException();
    }

    public void Allocate(int capacity)
    {
        throw new NotImplementedException();
    }

    public void Append(Utf8String data) => Append(data.AsSpan());

    public void Append(Utf8Char data) => Append(data.AsByte());

    private void Append(ReadOnlySpan<byte> data)
    {
        throw new NotImplementedException();
    }

    private void Append(byte data)
    {
        throw new NotImplementedException();
    }

    public Utf8String ToUtf8String() => throw new NotImplementedException();

    public override string ToString() => ToUtf8String().ToString();

    public void Dispose()
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Utf8String Create<T>
    (
        in T argument,
        Utf8BufferAction<T> action,
        int capacity = StackAllocationThreshold
    ) where T : struct, allows ref struct
    {
        scoped var builder = capacity > StackAllocationThreshold
            ? new Utf8Buffer(capacity)
            : new Utf8Buffer(stackalloc byte[StackAllocationThreshold]);

        try
        {
            action(ref builder, argument);
            return builder.ToString();
        }
        finally
        {
            builder.Dispose();
        }
    }
}
