using System.Collections.Generic;

namespace TypeScriptParserBackend.Contracts.Responses
{
    /// <summary>
    /// Response payload for parsed TypeScript class information.
    /// Note: DTO is intentionally decoupled from domain models.
    /// </summary>
    public class ParseResponse
    {
        public string ClassName { get; set; } = string.Empty;
        public string? BaseClass { get; set; }
        public List<string> ImplementedInterfaces { get; set; } = new();
        public List<string> Variables { get; set; } = new();
        public List<string> Methods { get; set; } = new();
        public List<string> Imports { get; set; } = new();
        public string? ReconstructedSource { get; set; }
    }
}
