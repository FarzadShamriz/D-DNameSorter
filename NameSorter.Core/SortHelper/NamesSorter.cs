using NameSorter.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameSorter.Core.SortHelper
{
    public class NamesSorter : INamesSorter
    {
        public IEnumerable<Name> Sort(IEnumerable<Name> names)
        {
            if (names is null) throw new ArgumentNullException(nameof(names));

            return names
                .OrderBy(n => n.LastName)
                .ThenBy(n => n.GivenNames)
                .ToList();
        }
    }
}
