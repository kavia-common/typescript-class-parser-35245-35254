using System.Text;
using TypeScriptParserBackend.Domain.Models;

namespace TypeScriptParserBackend.Application
{
    /// <summary>
    /// Responsible for reconstructing TypeScript source from parsed information.
    /// Stub implementation for initial wiring and compilation.
    /// </summary>
    public class TypeScriptReconstructor
    {
        // PUBLIC_INTERFACE
        /// <summary>
        /// Reconstructs a simple source string from class info.
        /// </summary>
        /// <param name="info">Parsed class info.</param>
        /// <returns>Placeholder TypeScript code.</returns>
        public string ToSource(TypeScriptClassInfo info)
        {
            var sb = new StringBuilder();

            // Emit imports (very naive formatting for now)
            foreach (var imp in info.Imports)
            {
                sb.Append("import ");
                if (imp.IsTypeOnly) sb.Append("type ");
                if (!string.IsNullOrWhiteSpace(imp.DefaultImport))
                {
                    sb.Append(imp.DefaultImport);
                    if (imp.NamedImports.Count > 0) sb.Append(", ");
                }
                if (imp.NamedImports.Count > 0)
                {
                    sb.Append("{ ").Append(string.Join(", ", imp.NamedImports)).Append(" }");
                }
                if (!string.IsNullOrWhiteSpace(imp.NamespaceImport))
                {
                    sb.Append("* as ").Append(imp.NamespaceImport);
                }
                sb.Append(" from \"").Append(imp.Module).AppendLine("\";");
            }

            // Emit class header
            sb.Append("export class ").Append(info.ClassName);
            if (!string.IsNullOrWhiteSpace(info.BaseClass))
            {
                sb.Append(" extends ").Append(info.BaseClass);
            }
            if (info.ImplementedInterfaces.Count > 0)
            {
                sb.Append(" implements ").Append(string.Join(", ", info.ImplementedInterfaces));
            }
            sb.AppendLine(" {");

            // Emit variables (no types/initializers formatting refinements yet)
            foreach (var v in info.Variables)
            {
                var mods = string.Join(" ", new[]
                {
                    v.AccessModifier,
                    v.IsStatic ? "static" : string.Empty,
                    v.IsReadonly ? "readonly" : string.Empty
                }).Trim();

                if (!string.IsNullOrWhiteSpace(mods))
                {
                    sb.Append("  ").Append(mods).Append(" ");
                }
                else
                {
                    sb.Append("  ");
                }

                sb.Append(v.Name);

                if (!string.IsNullOrWhiteSpace(v.Type))
                {
                    sb.Append(": ").Append(v.Type);
                }

                if (!string.IsNullOrWhiteSpace(v.Initializer))
                {
                    sb.Append(" = ").Append(v.Initializer);
                }

                sb.AppendLine(";");
            }

            // Emit method signatures placeholder
            foreach (var m in info.Methods)
            {
                var mods = string.Join(" ", new[]
                {
                    m.AccessModifier,
                    m.IsStatic ? "static" : string.Empty,
                    m.IsAsync ? "async" : string.Empty
                }).Trim();

                if (!string.IsNullOrWhiteSpace(mods))
                {
                    sb.Append("  ").Append(mods).Append(" ");
                }
                else
                {
                    sb.Append("  ");
                }

                sb.Append(m.Name).Append("(").Append(string.Join(", ", m.Parameters)).Append(")");
                if (!string.IsNullOrWhiteSpace(m.ReturnType))
                {
                    sb.Append(": ").Append(m.ReturnType);
                }
                sb.AppendLine(" { /* ... */ }");
            }

            sb.AppendLine("}");

            return sb.ToString();
        }
    }
}
