using Microsoft.AspNetCore.Http.HttpResults;
using TypeScriptParserBackend.Application;
using TypeScriptParserBackend.Contracts.Requests;
using TypeScriptParserBackend.Contracts.Responses;
using TypeScriptParserBackend.Domain.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(cfg =>
{
    cfg.Title = "TypeScript Parser Backend";
    cfg.Version = "v1";
    cfg.Description = "Parses TypeScript class source to extract structural info and reconstruct code.";
});

// Register application services (DI)
builder.Services.AddScoped<ITypeScriptParser, TypeScriptParserBackend.Application.TypeScriptParser>();
builder.Services.AddScoped<TypeScriptReconstructor>();

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.SetIsOriginAllowed(_ => true)
              .AllowCredentials()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Use CORS
app.UseCors("AllowAll");

// Configure OpenAPI/Swagger
app.UseOpenApi();
app.UseSwaggerUi(config =>
{
    config.Path = "/docs";
});

// Health check endpoint
app.MapGet("/", () => new { message = "Healthy" });

/// <summary>
/// Parses a TypeScript class source and returns a placeholder summary.
/// </summary>
/// <remarks>
/// This is a stub implementation for step 1. It validates input and returns minimal data.
/// </remarks>
app.MapPost("/api/parse", async Task<Results<Ok<ParseResponse>, BadRequest<ErrorResponse>>> (ParseRequest request, ITypeScriptParser parser, TypeScriptReconstructor reconstructor, CancellationToken ct) =>
{
    if (request is null || string.IsNullOrWhiteSpace(request.Source))
    {
        return TypedResults.BadRequest(new ErrorResponse
        {
            Error = "ValidationError",
            Message = "Source is required."
        });
    }

    var classInfo = await parser.ParseAsync(request.Source, ct);

    // Map domain to DTO intentionally minimal for now
    var response = new ParseResponse
    {
        ClassName = classInfo.ClassName,
        BaseClass = classInfo.BaseClass,
        ImplementedInterfaces = classInfo.ImplementedInterfaces,
        ReconstructedSource = reconstructor.ToSource(classInfo)
    };

    // Light, human-readable summaries for now
    foreach (var imp in classInfo.Imports)
    {
        var parts = new List<string>();
        if (!string.IsNullOrWhiteSpace(imp.DefaultImport))
            parts.Add(imp.DefaultImport!);
        if (imp.NamedImports.Count > 0)
            parts.Add("{ " + string.Join(", ", imp.NamedImports) + " }");
        if (!string.IsNullOrWhiteSpace(imp.NamespaceImport))
            parts.Add("* as " + imp.NamespaceImport);

        var left = parts.Count > 0 ? string.Join(", ", parts) + " " : string.Empty;
        response.Imports.Add($"import {(imp.IsTypeOnly ? "type " : string.Empty)}{left}from \"{imp.Module}\";");
    }

    foreach (var v in classInfo.Variables)
    {
        var mods = string.Join(" ", new[]
        {
            v.AccessModifier,
            v.IsStatic ? "static" : string.Empty,
            v.IsReadonly ? "readonly" : string.Empty
        }).Trim();

        var text = string.IsNullOrWhiteSpace(mods) ? v.Name : $"{mods} {v.Name}";
        if (!string.IsNullOrWhiteSpace(v.Type)) text += $": {v.Type}";
        if (!string.IsNullOrWhiteSpace(v.Initializer)) text += $" = {v.Initializer}";
        response.Variables.Add(text);
    }

    foreach (var m in classInfo.Methods)
    {
        var mods = string.Join(" ", new[]
        {
            m.AccessModifier,
            m.IsStatic ? "static" : string.Empty,
            m.IsAsync ? "async" : string.Empty
        }).Trim();

        var text = string.IsNullOrWhiteSpace(mods) ? m.Name : $"{mods} {m.Name}";
        text += $"({string.Join(", ", m.Parameters)})";
        if (!string.IsNullOrWhiteSpace(m.ReturnType)) text += $": {m.ReturnType}";
        response.Methods.Add(text);
    }

    return TypedResults.Ok(response);
})
.WithName("ParseTypeScript")
.WithSummary("Parses TypeScript class source")
.WithDescription("Accepts raw TypeScript class source text, parses it, and returns a placeholder structured response.")
.Produces<ParseResponse>(StatusCodes.Status200OK)
.Produces<ErrorResponse>(StatusCodes.Status400BadRequest);

app.Run();