using Falko.Foundry.Utf8Text;
using NUnit.Framework;

namespace Falko.Tests.Testers;

public partial class Utf8StringTester
{
    [Test]
    public void FromString_Ascii_ByteLengthIsCharCount()
    {
        Utf8String utf8String = "hello";
        Assert.That(utf8String.Length, Is.EqualTo(5));
    }

    [Test]
    public void FromString_Cyrillic_ByteLengthIsTwiceCharCount()
    {
        Utf8String utf8String = "привет";
        Assert.That(utf8String.Length, Is.EqualTo(12));
    }

    [Test]
    public void FromString_Emoji_ByteLengthIs4()
    {
        Utf8String utf8String = "😀";
        Assert.That(utf8String.Length, Is.EqualTo(4));
    }

    [Test]
    public void FromSpan_Ascii_RoundTrip()
    {
        var bytes = "hello"u8.ToArray();
        var utf8String = new Utf8String(bytes.AsSpan());
        Assert.That(utf8String.ToString(), Is.EqualTo("hello"));
    }
}
