using NameSorter.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameSorter.Core.ParseHelper
{
    public interface INameParser
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="line"></param>
        /// <param name="name"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        bool TryParse(string line, out Name? name, out string? error);

    }
}
