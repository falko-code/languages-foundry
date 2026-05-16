namespace Falko.Foundry.CSharp.Elements;

public readonly struct IndentationElement(IIndentationCompiler compiler)
{
    public readonly IIndentationCompiler Compiler = compiler;
}
