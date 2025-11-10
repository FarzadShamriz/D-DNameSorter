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
            return names;
        }
    }
}
