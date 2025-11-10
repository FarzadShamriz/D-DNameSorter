using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameSorter.Core.IOHelper
{
    public class FileProvider : IFileProvider
    {
        public Task<IList<string>> ReadAllLinesAsync(string path, CancellationToken ct = default)
        {
            var lines = File.ReadAllLines(path);
            return Task.FromResult<IList<string>>(lines);
        }

        public Task WriteAllLinesAsync(string path, IEnumerable<string> lines, CancellationToken ct = default)
        {
            return File.WriteAllLinesAsync(path, lines, ct);
        }

        public bool Exists(string path) => File.Exists(path);
    }
}
