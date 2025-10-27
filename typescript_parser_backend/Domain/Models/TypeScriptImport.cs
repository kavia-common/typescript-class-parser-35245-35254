using System.Collections.Generic;

namespace TypeScriptParserBackend.Domain.Models
{
    /// <summary>
    /// Represents a single TypeScript import statement.
    /// </summary>
    public class TypeScriptImport
    {
        /// <summary>
        /// The module specifier, e.g., "react" or "./utils".
        /// </summary>
        public string Module { get; set; } = string.Empty;

        /// <summary>
        /// The default import name, e.g., React in: import React from "react";
        /// </summary>
        public string? DefaultImport { get; set; }

        /// <summary>
        /// Named imports list, e.g., useState, useEffect in: import { useState, useEffect } from "react";
        /// </summary>
        public List<string> NamedImports { get; set; } = new();

        /// <summary>
        /// Namespace import, e.g., * as Utils in: import * as Utils from "./utils";
        /// </summary>
        public string? NamespaceImport { get; set; }

        /// <summary>
        /// Whether this import is a type-only import: import type { Foo } from "bar";
        /// </summary>
        public bool IsTypeOnly { get; set; }
    }
}
