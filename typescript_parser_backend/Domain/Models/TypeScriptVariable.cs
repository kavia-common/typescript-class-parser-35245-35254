namespace TypeScriptParserBackend.Domain.Models
{
    /// <summary>
    /// Represents a field/variable within a TypeScript class.
    /// </summary>
    public class TypeScriptVariable
    {
        /// <summary>
        /// Variable name.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Optional TypeScript type annotation (raw string).
        /// </summary>
        public string? Type { get; set; }

        /// <summary>
        /// Optional initializer (raw string without trailing semicolon).
        /// </summary>
        public string? Initializer { get; set; }

        /// <summary>
        /// Is this variable declared as readonly.
        /// </summary>
        public bool IsReadonly { get; set; }

        /// <summary>
        /// Access modifier: public, private, protected (empty string means omitted/default).
        /// </summary>
        public string AccessModifier { get; set; } = string.Empty;

        /// <summary>
        /// Is marked static.
        /// </summary>
        public bool IsStatic { get; set; }
    }
}
