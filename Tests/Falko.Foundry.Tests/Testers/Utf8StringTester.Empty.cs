using Falko.Foundry.Utf8Texts;
using NUnit.Framework;

namespace Falko.Tests.Testers;

public partial class Utf8StringTester
{
    [Test]
    public void Empty_IsEmpty()
    {
        Assert.That(Utf8String.Empty.IsEmpty, Is.True);
    }

    [Test]
    public void Default_IsEmpty()
    {
        Utf8String utf8String = default;
        Assert.That(utf8String.IsEmpty, Is.True);
    }

    [Test]
    public void NonEmpty_IsNotEmpty()
    {
        Utf8String utf8String = "x"u8;
        Assert.That(utf8String.IsEmpty, Is.False);
    }
}
