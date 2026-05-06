> [!WARNING]
> This project is under active development. The underlying C# libraries are subject to change.

# Falko Languages Foundry

[![NuGet Version](https://img.shields.io/nuget/v/Falko.Foundry?style=for-the-badge&color=green)](https://www.nuget.org/packages?q=Falko.Foundry&prerel=false)
[![NuGet Version](https://img.shields.io/nuget/vpre/Falko.Foundry?style=for-the-badge&color=red)](https://www.nuget.org/packages?q=Falko.Foundry&prerel=true)
[![SDK Version](https://img.shields.io/badge/.NET-10%2C9-gray?style=for-the-badge)](https://dotnet.microsoft.com/en-us/download)
[![CSharp Version](https://img.shields.io/badge/CSharp-14-gray?style=for-the-badge)](https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-version-history)
[![GitHub License](https://img.shields.io/github/license/falko-code/zero-logger?style=for-the-badge&color=gray)](License.md)

Fluent and Zero-Allocation Code Generation for Source Generators.

## Why?

_Tired of StringBuilder hacks and verbose SyntaxFactory?_
LanguagesFoundry gives you a clean, zero-allocation API for code generation inside SourceGenerators.

This library is designed to be a powerful and efficient tool for generating C# code at compile time,
without the overhead of string manipulation or complex syntax trees.

It provides a fluent API that allows you to build code structures in a clear and concise manner,
while ensuring optimal performance.

## Example

```csharp
// example element to compile
var stringType = new TypeElement { Namespace = "System"u8, Name = "String"u8 };

// compile the element to a utf8 result that can be output without allocations
var result = CSharpLanguageCompiler.Instance.CompileElement(in stringType);

// output to file without allocations
File.WriteAllBytes("output.cs", result.AsSpan());

// output without allocations but longer
foreach (var part in result) Console.Write(part);

// output with allocations but faster
Console.WriteLine(result.ToString());
```

## License

This project is licensed under the **[GNU General Public License v3.0](License.md)**.

**© 2026, Falko**
