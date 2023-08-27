using System.Diagnostics;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Generator;

[Generator]
public class TypeAliasGenerator : ISourceGenerator
{
    public void Initialize(GeneratorInitializationContext context)
    {
        context.RegisterForSyntaxNotifications(() => new TypeSynonymContext());
        if (!Debugger.IsAttached)
        {
            //Debugger.Launch();
        }
    }

    public void Execute(GeneratorExecutionContext context)
    {
        TypeSynonymContext syntaxReceiver = (TypeSynonymContext)context.SyntaxReceiver!;
        foreach (var record in syntaxReceiver.Records)
        {
            try
            {
                AugmentRecord(context, record);
            }
            catch (Exception e)
            {
                //TODO add diagnostic on exception
                Console.WriteLine(e);
            }
        }
    }

    static void AugmentRecord(GeneratorExecutionContext context, RecordDeclarationSyntax record)
    {
        var semanticModel = context.Compilation.GetSemanticModel(record.SyntaxTree);
        var recordModel = semanticModel?.GetDeclaredSymbol(record) as ITypeSymbol;
        if (semanticModel is null || recordModel is null)
        {
            return;
        }

        var parameters = record.ParameterList?.DescendantNodes().OfType<ParameterSyntax>().ToList();
        if (parameters is { Count: not 1})
        {
            // type is not a simple type alias as it contains multiple parameters, skip type
            return;
        }

        var parameter = parameters!.Single();
        var parameterName = parameter.Identifier.ValueText;
        var predef = parameter.Type as PredefinedTypeSyntax;
        var operations = new List<string>();

        switch (predef?.Keyword.Text)
        {
            // == and != already work by default
            case "int":
            case "float":
            case "double":
            case "decimal":
                operations.Add($"public static {record.Identifier} operator +({record.Identifier} a) => new(+a.{parameterName});");
                operations.Add($"public static {record.Identifier} operator -({record.Identifier} a) => new(-a.{parameterName});");
                operations.Add(AdditionOperator(record.Identifier.ToString(), parameterName));
                operations.Add($"public static {record.Identifier} operator -({record.Identifier} a, {record.Identifier} b) => new(a.{parameterName} - b.{parameterName});");
                operations.Add($"public static {record.Identifier} operator *({record.Identifier} a, {record.Identifier} b) => new(a.{parameterName} * b.{parameterName});");
                operations.Add($"public static {record.Identifier} operator /({record.Identifier} a, {record.Identifier} b) => new(a.{parameterName} / b.{parameterName});");
                operations.Add($"public static bool operator >({record.Identifier} a, {record.Identifier} b) => a.{parameterName} > b.{parameterName};");
                operations.Add($"public static bool operator >=({record.Identifier} a, {record.Identifier} b) => a.{parameterName} >= b.{parameterName};");
                operations.Add($"public static bool operator <({record.Identifier} a, {record.Identifier} b) => a.{parameterName} < b.{parameterName};");
                operations.Add($"public static bool operator <=({record.Identifier} a, {record.Identifier} b) => a.{parameterName} <= b.{parameterName};");
                break;
            case "string":
                operations.Add(AdditionOperator(record.Identifier.ToString(), parameterName));
                break;
        }

        if (!recordModel.IsReadOnly)
        {
            switch (predef?.Keyword.Text)
            {
                case "int":
                case "float":
                case "double":
                case "decimal":
                    operations.Add($"public static {record.Identifier} operator ++({record.Identifier} a) => new(a.{parameterName}+1);");
                    // this might appear like the correct implementation when using the prefix operator but it doesn't seem to be necessary.
                    //operations.Add($"public static {record.Identifier} operator ++({record.Identifier} a) {{ a.{parameterName}++; return a; }}");
                    operations.Add($"public static {record.Identifier} operator --({record.Identifier} a) => new(a.{parameterName}-1);");
                    break;
            }
        }

        var namespaceText = recordModel.ContainingNamespace.IsGlobalNamespace
            ? string.Empty
            : $"namespace {recordModel.ContainingNamespace};";


        var sourceText = $$"""
            {{namespaceText}}
            {{recordModel.DeclaredAccessibility.ToString("G").ToLower()}} partial record struct {{record.Identifier}}
            {
                {{string.Join($"\r\n\t", operations)}}
            }
            """;

        context.AddSource($"{record.Identifier}.g.cs", sourceText);
    }

    static string AdditionOperator(string typeName, string valueName) =>
        $"public static {typeName} operator +({typeName} a, {typeName} b) => new(a.{valueName} + b.{valueName});";

    class TypeSynonymContext : ISyntaxReceiver
    {
        static readonly string attributeName = nameof(TypeAliasAttribute).Replace("Attribute", string.Empty);

        public List<RecordDeclarationSyntax> Records { get; set; } = new();

        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            if (syntaxNode is RecordDeclarationSyntax recordDeclarationSyntax
                && recordDeclarationSyntax.AttributeLists.Any(list => list.Attributes.Any(a => a.Name.ToString() == attributeName)))
            {
                //TODO check for partial
                Records.Add(recordDeclarationSyntax);
            }
        }
    }
}