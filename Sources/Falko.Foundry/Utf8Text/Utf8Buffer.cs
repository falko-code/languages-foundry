namespace Falko.Foundry.Utf8Text;

public ref struct Utf8Buffer : IDisposable
{
    public void Allocate(int capacity)
    {

    }

    public void Append(string data) => Append(data.AsSpan());

    public void Append(ReadOnlySpan<char> data)
    {

    }

    public void Append(char data)
    {

    }

    public void Append(Utf8String data) => Append(data.AsSpan());

    public void Append(ReadOnlySpan<byte> data)
    {

    }

    public void Append(byte data)
    {

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
