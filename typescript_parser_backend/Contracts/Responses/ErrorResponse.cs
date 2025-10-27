namespace TypeScriptParserBackend.Contracts.Responses
{
    /// <summary>
    /// Standard error response envelope.
    /// </summary>
    public class ErrorResponse
    {
        public string Error { get; set; } = "Bad Request";
        public string Message { get; set; } = string.Empty;
    }
}
