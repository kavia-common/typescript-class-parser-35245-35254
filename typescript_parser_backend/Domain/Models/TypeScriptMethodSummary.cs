using System.Collections.Generic;

namespace TypeScriptParserBackend.Domain.Models
{
    /// <summary>
    /// Represents a lightweight summary of a TypeScript class method.
    /// </summary>
    public class TypeScriptMethodSummary
    {
        /// <summary>
        /// Method name.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Access modifier: public, private, protected (empty means omitted).
        /// </summary>
        public string AccessModifier { get; set; } = string.Empty;

        /// <summary>
        /// Whether the method is static.
        /// </summary>
        public bool IsStatic { get; set; }

        /// <summary>
        /// Whether the method is async.
        /// </summary>
        public bool IsAsync { get; set; }

        /// <summary>
        /// Optional list of parameter names (no types for now).
        /// </summary>
        public List<string> Parameters { get; set; } = new();

        /// <summary>
        /// Optional return type string.
        /// </summary>
        public string? ReturnType { get; set; }
    }
}
