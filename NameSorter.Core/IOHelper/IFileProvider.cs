using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameSorter.Core.IOHelper
{
    public interface IFileProvider
    {
        /// <summary>
        /// Reads all lines from a file asynchronously.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<IList<string>> ReadAllLinesAsync(string path, CancellationToken ct = default);

        /// <summary>
        /// Writes all lines to a file asynchronously.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="lines"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task WriteAllLinesAsync(string path, IEnumerable<string> lines, CancellationToken ct = default);

        /// <summary>
        /// Check if a file exists at the given path.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        bool Exists(string path);
    }
}
