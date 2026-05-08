using Falko.Foundry.Utf8Text;
using NUnit.Framework;

namespace Falko.Tests.Testers;

public partial class Utf8StringTester
{
    [Test]
    public void ToString_Ascii_RoundTrip()
    {
        Utf8String utf8String = "hello"u8;
        Assert.That(utf8String.ToString(), Is.EqualTo("hello"));
    }

    [Test]
    public void ToString_Cyrillic_RoundTrip()
    {
        Utf8String utf8String = "привет"u8;
        Assert.That(utf8String.ToString(), Is.EqualTo("привет"));
    }

    [Test]
    public void ToString_Emoji_RoundTrip()
    {
        Utf8String utf8String = "😀🎉"u8;
        Assert.That(utf8String.ToString(), Is.EqualTo("😀🎉"));
    }
}
