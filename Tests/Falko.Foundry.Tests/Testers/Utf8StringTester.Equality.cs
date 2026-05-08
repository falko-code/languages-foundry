using Falko.Foundry.Utf8Text;
using NUnit.Framework;

namespace Falko.Tests.Testers;

public partial class Utf8StringTester
{
    [Test]
    public void Equals_SameContent_ReturnsTrue()
    {
        Utf8String left = "hello"u8;
        Utf8String right = "hello"u8;
        Assert.That(left, Is.EqualTo(right));
    }

    [Test]
    public void Equals_DifferentContent_ReturnsFalse()
    {
        Utf8String left = "hello"u8;
        Utf8String right = "world"u8;
        Assert.That(left, Is.Not.EqualTo(right));
    }

    [Test]
    public void OperatorEqual_SameContent_ReturnsTrue()
    {
        Utf8String left = "test"u8;
        Utf8String right = "test"u8;
        Assert.That(left == right, Is.True);
    }

    [Test]
    public void OperatorNotEqual_DifferentContent_ReturnsTrue()
    {
        Utf8String left = "foo"u8;
        Utf8String right = "bar"u8;
        Assert.That(left != right, Is.True);
    }

    [Test]
    public void GetHashCode_SameContent_SameHash()
    {
        Utf8String left = "hello"u8;
        Utf8String right = "hello"u8;
        Assert.That(left.GetHashCode(), Is.EqualTo((object)right.GetHashCode()));
    }

    [Test]
    public void GetHashCode_DifferentContent_DifferentHash()
    {
        Utf8String left = "hello"u8;
        Utf8String right = "world"u8;
        Assert.That(left.GetHashCode(), Is.Not.EqualTo((object)right.GetHashCode()));
    }

    [Test]
    public void CompareTo_EqualContent_ReturnsZero()
    {
        Utf8String left = "abc"u8;
        Utf8String right = "abc"u8;
        Assert.That(left.CompareTo(right), Is.EqualTo(0));
    }

    [Test]
    public void CompareTo_LessContent_ReturnsNegative()
    {
        Utf8String left = "abc"u8;
        Utf8String right = "abd"u8;
        Assert.That(left.CompareTo(right), Is.LessThan(0));
    }

    [Test]
    public void CompareTo_GreaterContent_ReturnsPositive()
    {
        Utf8String left = "abd"u8;
        Utf8String right = "abc"u8;
        Assert.That(left.CompareTo(right), Is.GreaterThan(0));
    }
}
