using Falko.Foundry.Utf8Texts;
using NUnit.Framework;

namespace Falko.Tests.Testers;

public partial class Utf8StringTester
{
    [Test]
    public void Count_Ascii_ReturnsCharCount()
    {
        Utf8String utf8String = "hello"u8;
        Assert.That(utf8String.Count(), Is.EqualTo(5));
    }

    [Test]
    public void Count_Cyrillic_ReturnsRuneCount()
    {
        Utf8String utf8String = "привет";
        Assert.That(utf8String.Count(), Is.EqualTo(6));
    }

    [Test]
    public void Count_Emoji_ReturnsRuneCount()
    {
        Utf8String utf8String = "😀🎉"u8;
        Assert.That(utf8String.Count(), Is.EqualTo(2));
    }

    [Test]
    public void Count_Empty_ReturnsZero()
    {
        Assert.That(Utf8String.Empty.Count(), Is.EqualTo(0));
    }
}
