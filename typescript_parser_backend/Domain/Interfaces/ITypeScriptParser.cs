using System.Threading;
using System.Threading.Tasks;
using TypeScriptParserBackend.Domain.Models;

namespace TypeScriptParserBackend.Domain.Interfaces
{
    /// <summary>
    /// Defines the contract for parsing TypeScript class source into structured information.
    /// </summary>
    public interface ITypeScriptParser
    {
        // PUBLIC_INTERFACE
        /// <summary>
        /// Parses a TypeScript class source string and returns summarized information.
        /// Implementations may use regex for simple constructs and external tooling for complex scenarios.
        /// </summary>
        /// <param name="source">The TypeScript source code to parse.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Structured class info extracted from the source.</returns>
        Task<TypeScriptClassInfo> ParseAsync(string source, CancellationToken cancellationToken = default);
    }
}
