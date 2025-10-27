using System.ComponentModel.DataAnnotations;

namespace TypeScriptParserBackend.Contracts.Requests
{
    /// <summary>
    /// Request payload for parsing TypeScript class source.
    /// </summary>
    public class ParseRequest
    {
        /// <summary>
        /// Raw TypeScript class source text to parse.
        /// </summary>
        [Required]
        public string Source { get; set; } = string.Empty;
    }
}
