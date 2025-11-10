using NameSorter.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameSorter.Core.ParseHelper
{
    public class NameParser : INameParser
    {
        public bool TryParse(string line, out Name? name, out string? error)
        {
            throw new NotImplementedException();
        }
    }
}
