using Falko.Foundry.Utf8Texts;

namespace Falko.Foundry.CSharp.Compilers;

internal static class CSharpLanguageConstants
{
    public static readonly Utf8Char Space = " "u8;

    public static readonly Utf8Char Dot = "."u8;

    public static readonly Utf8Char LeftAngleBracket = "<"u8;

    public static readonly Utf8Char RightAngleBracket = ">"u8;

    public static readonly Utf8Char LeftBracket = "{"u8;

    public static readonly Utf8Char RightBracket = "}"u8;

    public static readonly Utf8Char LineEnd = "\n"u8;

    public static readonly Utf8String LineBreak = ";\n"u8;

    public static readonly Utf8String CommaSpace = ", "u8;

    public static readonly Utf8String UsingNamespace = "using "u8;
}
