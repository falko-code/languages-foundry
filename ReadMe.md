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
Console.Write(result.ToString());
```

## How Create Own Compilers?

The library is designed to be extensible, allowing you to create your own compilers for different languages or code structures.

At first, you need to define your own element types that represent the code structures you want to generate. For example, you can define a `PropertyElement` that represents a property in C#.

> [!NOTE]
> Use `Utf8String` for string properties to avoid allocations during compilation.
> And for lists of elements, use `ImmutableArray<T>` to ensure immutability and low memory overhead.

```csharp
public struct JsonPropertyElement
{
    public required Utf8String Name;
}
```

After that, you can create specific compilers for different element types by implementing the `ElementCompiler` class.

> [!NOTE]
> The `IElementCompiler` should be always with constructor without parameters.

> [!NOTE]
> We need to allocate the buffer with the exact size of the code we want to generate to avoid allocations during compilation.
> Better allocate the buffer less times as you can and incoke that as one time as possible.

```csharp
public sealed class JsonPropertyElementCompiler : IElementCompiler<PropertyElement>
{
    public override void Compile
    (
        ILanguageCompiler compiler,
        ref Utf8Buffer buffer,
        in PropertyElement element
    )
    {
        const string bracket = '"';
        buffer.Allocate(bracket.Length * 2 + element.Name.Length);
        builder.Append(bracket).Append(element.Name).Append(bracket);
    }
}
```

After implementing the element compilers, you need to create a class that implements the `LanguageCompiler` class, which defines the contract for compiling elements into code.

And in the constructor of your language compiler, you can register the compilers for different element types using the `SetElementCompiler` method.

> [!NOTE]
> Not forgot to set in the generic parameter of `LanguageCompiler` the type of your language compiler itself.

```csharp
public sealed class JsonLanguageCompiler : LanguageCompiler<JsonpLanguageCompiler>
{
    public static readonly JsonLanguageCompiler Instance = new();

    private JsonLanguageCompiler()
    {
        SetElementCompiler<JsonPropertyElementCompiler, JsonPropertyElement>();
    }
}
```

After all, you can use the `CompileElement` method of your language compiler to compile elements into code.

```csharp
var propertyElement = new JsonPropertyElement { Name = "MyProperty"u8 };
var result = JsonLanguageCompiler.Instance.CompileElement(in propertyElement);
```

That's it! You can now generate code for your custom elements using your own language compiler.

## License

This project is licensed under the **[GNU General Public License v3.0](License.md)**.

**© 2026, Falko**
