using Falko.Foundry.Utf8Text;
using NUnit.Framework;

namespace Falko.Tests.Testers;

public partial class Utf8StringTester
{
    [Test]
    public void Concat_TwoUtf8Strings_Ascii()
    {
        Utf8String left = "hello"u8;
        Utf8String right = " world"u8;
        Assert.That((left + right).ToString(), Is.EqualTo("hello world"));
    }

    [Test]
    public void Concat_Utf8StringWithStringRight_Ascii()
    {
        Utf8String left = "hello"u8;
        Assert.That((left + " world").ToString(), Is.EqualTo("hello world"));
    }

    [Test]
    public void Concat_StringWithUtf8StringLeft_Ascii()
    {
        Utf8String right = " world"u8;
        Assert.That(("hello" + right).ToString(), Is.EqualTo("hello world"));
    }

    [Test]
    public void Concat_TwoUtf8Strings_Cyrillic()
    {
        Utf8String left = "привет";
        Utf8String right = " мир";
        Assert.That((left + right).ToString(), Is.EqualTo("привет мир"));
    }
}
