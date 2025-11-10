using NameSorter.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NameSorter.Core.ParseHelper
{
    public class NameParser : INameParser
    {
        //Regex to use for name normalization (removing extra spaces)
        private static readonly Regex _multiSpace = new(@"\s+", RegexOptions.Compiled);

        public bool TryParse(string line, out Name? name, out string? error)
        {
            name = null;
            error = null;

            if (string.IsNullOrWhiteSpace(line))
            {
                error = "Line is empty or whitespace.";
                return false;
            }

            var normalized = _multiSpace.Replace(line.Trim(), " ");
            var parts = normalized.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            // Expecting 2 to 4 tokens, 1 to 3 given names + 1 last name
            if (parts.Length < 2)
            {
                error = "A name must contain at least one given name and a last name.";
                return false;
            }

            if (parts.Length > 4)
            {
                error = "A name may have up to three given names and one last name.";
                return false;
            }

            var lastName = parts.Last();
            var given = parts.Take(parts.Length - 1).ToArray();

            name = new Name(lastName, given, normalized);
            return true;
        }
    }
}
