using NameSorter.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameSorter.Core.SortHelper
{
    public interface INameSorter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        IEnumerable<Name> Sort(IEnumerable<Name> names);
    }
}
