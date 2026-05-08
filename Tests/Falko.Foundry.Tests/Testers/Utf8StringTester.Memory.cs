using Falko.Foundry.Utf8Text;
using NUnit.Framework;

namespace Falko.Tests.Testers;

public partial class Utf8StringTester
{
    [Test]
    public void Wrap_ByteArray_AsSpanMatchesSource()
    {
        var bytes = "hello"u8.ToArray();
        var utf8String = Utf8String.Wrap(bytes);
        Assert.That(utf8String.AsSpan().ToArray(), Is.EqualTo(bytes));
    }

    [Test]
    public void AsMemory_MatchesAsSpan()
    {
        Utf8String utf8String = "test"u8;
        Assert.That(utf8String.AsMemory().ToArray(), Is.EqualTo((object)utf8String.AsSpan().ToArray()));
    }
}
