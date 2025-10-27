using System.Threading;
using System.Threading.Tasks;
using TypeScriptParserBackend.Domain.Interfaces;
using TypeScriptParserBackend.Domain.Models;

namespace TypeScriptParserBackend.Application
{
    /// <summary>
    /// Minimal stub parser to satisfy DI and API integration. 
    /// Real parsing to be implemented in subsequent steps.
    /// </summary>
    public class TypeScriptParser : ITypeScriptParser
    {
        // PUBLIC_INTERFACE
        /// <inheritdoc />
        public Task<TypeScriptClassInfo> ParseAsync(string source, CancellationToken cancellationToken = default)
        {
            // Stubbed result for now. We'll populate minimal values to demonstrate the flow.
            var result = new TypeScriptClassInfo
            {
                ClassName = "UnknownClass",
                OriginalSource = source
            };

            return Task.FromResult(result);
        }
    }
}
