> [!WARNING]
> This project is under active development. The underlying C# libraries are subject to change.

<div align="center">

<img src="Sticker512.png" width="128" style="margin: 0 0 -45px 0"/>

# Languages Foundry

</div>

[![NuGet Version](https://img.shields.io/nuget/v/Falko.Foundry?style=for-the-badge&color=green)](https://www.nuget.org/packages?q=Falko.Foundry&prerel=false)
[![NuGet Version](https://img.shields.io/nuget/vpre/Falko.Foundry?style=for-the-badge&color=red)](https://www.nuget.org/packages?q=Falko.Foundry&prerel=true)
[![SDK Version](https://img.shields.io/badge/.NET-10%2C9-gray?style=for-the-badge)](https://dotnet.microsoft.com/en-us/download)
[![CSharp Version](https://img.shields.io/badge/CSharp-14-gray?style=for-the-badge)](https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-version-history)
[![GitHub License](https://img.shields.io/github/license/falko-code/zero-logger?style=for-the-badge&color=gray)](License.md)

Fluent and Zero-Allocation Code Generation for Source Generators.

## <img src="Sticker128.png" width="18" hspace="5" /> Example

```csharp
// declare the element
var serviceType = new TypeElement { Name = "Service"u8 };

// compile the element
var compilerElement = CSharpLanguageCompiler.Instance.CompileElement(in serviceType);

// output compiled element
File.WriteAllBytes("output.g.cs", compilerElement.AsSpan());
```

## <img src="Sticker128.png" width="18" hspace="5" /> License

This project is licensed under the **[GNU General Public License v3.0](License.md)**.

**© 2026, Falko**
