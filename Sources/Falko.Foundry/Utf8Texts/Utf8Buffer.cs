namespace Falko.Foundry.Utf8Texts;

public ref struct Utf8Buffer : IDisposable
{
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
        // TODO release managed resources here
    }

    public static ReadOnlyMemory<byte> Create<T>(T argument, Action<T, Utf8Buffer> allocator)
        where T : struct, allows ref struct
    {
        throw new NotImplementedException();
    }
}
