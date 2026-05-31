> [!WARNING]
> This project is under active development. The underlying C# libraries are subject to change.

<div align="center">

#

### **LANGUAGES FOUNDRY**

<img src="Sticker512.png" width="128"/>

#

</div>

[![NuGet Version](https://img.shields.io/nuget/v/Falko.Foundry?style=for-the-badge&color=green)](https://www.nuget.org/packages?q=Falko.Foundry&prerel=false)
[![NuGet Version](https://img.shields.io/nuget/vpre/Falko.Foundry?style=for-the-badge&color=red)](https://www.nuget.org/packages?q=Falko.Foundry&prerel=true)
[![SDK Version](https://img.shields.io/badge/.NET-10%2C9-gray?style=for-the-badge)](https://dotnet.microsoft.com/en-us/download)
[![CSharp Version](https://img.shields.io/badge/CSharp-14-gray?style=for-the-badge)](https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-version-history)
[![GitHub License](https://img.shields.io/github/license/falko-code/zero-logger?style=for-the-badge&color=gray)](License.md)

Fluent and Zero-Allocation Code Generation for Source Generators.

# <img src="Sticker128.png" width="25" hspace="5" /> Example

Create `Example.cs` file with the following content:

```csharp
#!/usr/bin/env -S dotnet --
#:package Falko.Foundry.CSharp@0.1.0

using Falko.Foundry.Compilers;
using Falko.Foundry.CSharp.Compilers;
using Falko.Foundry.CSharp.Elements;

var intType = new TypeElement { Name = "Int32"u8, Namespace = "System"u8 };
var intVar = new TypeIdentifierElement { Name = "myInt"u8, Type = intType };

var compiler = CSharpLanguageCompiler.Instance;
Console.WriteLine(compiler.CompileElement(intVar.AsLine()));
```

**Run** directly with dotnet:
```bash
dotnet run Example.cs
```

**Or** make it an executable CLI and run in shorter command name:
```bash
ln -s Example.cs example
chmod +x example
```
```bash
./example
```

**This** simple example will output the following code:

```csharp
System.Int32 myInt;
```

For how use the library in modern ways, see the [Wiki](https://github.com/falko-code/languages-foundry/wiki) and the [Examples](Examples/) folder.

# <img src="Sticker128.png" width="25" hspace="5" /> License

This project is licensed under the **[GNU General Public License v3.0](License.md)**.

**© 2026, Falko**
