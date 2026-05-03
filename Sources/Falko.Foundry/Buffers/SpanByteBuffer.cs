namespace Falko.Foundry.Buffers;

public ref struct SpanByteBuffer
{
    public void Write(ReadOnlySpan<char> data)
    {

    }

    public void Write(byte data)
    {

    }

    public void Write(char data)
    {

    }

    public ReadOnlySpan<byte> ToSpan() => ReadOnlySpan<byte>.Empty;

    public static ReadOnlyMemory<byte> Create<T>(T argument, Action<T, SpanByteBuffer> allocator)
        where T : struct, allows ref struct
    {
        throw new NotImplementedException();
    }
}
