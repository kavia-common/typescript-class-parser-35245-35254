using System.Collections.Generic;

namespace TypeScriptParserBackend.Domain.Models
{
    /// <summary>
    /// Represents a parsed TypeScript class and its related top-level artifacts.
    /// </summary>
    public class TypeScriptClassInfo
    {
        /// <summary>
        /// Class name.
        /// </summary>
        public string ClassName { get; set; } = string.Empty;

        /// <summary>
        /// Optional base class name (extends).
        /// </summary>
        public string? BaseClass { get; set; }

        /// <summary>
        /// Implemented interfaces (names only).
        /// </summary>
        public List<string> ImplementedInterfaces { get; set; } = new();

        /// <summary>
        /// Collected class variables/fields.
        /// </summary>
        public List<TypeScriptVariable> Variables { get; set; } = new();

        /// <summary>
        /// Collected class method summaries.
        /// </summary>
        public List<TypeScriptMethodSummary> Methods { get; set; } = new();

        /// <summary>
        /// Top-level imports found in the source before the class declaration.
        /// </summary>
        public List<TypeScriptImport> Imports { get; set; } = new();

        /// <summary>
        /// Original source as provided (optional).
        /// </summary>
        public string? OriginalSource { get; set; }
    }
}
