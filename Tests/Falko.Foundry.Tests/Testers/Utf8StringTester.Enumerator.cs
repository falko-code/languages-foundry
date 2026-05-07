using System.Text;
using Falko.Foundry.Types;
using NUnit.Framework;

namespace Falko.Tests.Testers;

public partial class Utf8StringTester
{
    [Test]
    public void Foreach_Ascii_YieldsCorrectRunes()
    {
        Utf8String utf8String = "abc"u8;

        var runes = new List<int>();
        foreach (var rune in utf8String) runes.Add(rune.Value);

        Assert.That(runes, Is.EqualTo(new[] { 'a', 'b', 'c' }));
    }

    [Test]
    public void Foreach_Emoji_YieldsSingleRune()
    {
        Utf8String utf8String = "😀"u8;

        var runes = new List<Rune>();
        foreach (var rune in utf8String) runes.Add(rune);

        Assert.That(runes, Is.EqualTo(new[] { Rune.GetRuneAt("😀", 0) }));
    }
}
