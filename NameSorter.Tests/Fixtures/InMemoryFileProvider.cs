using NameSorter.Core.IOHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameSorter.Tests.Fixtures
{
    //Used AI to generate this test
    public class InMemoryFileProvider : IFileProvider
    {
        private readonly Dictionary<string, List<string>> _files = new(StringComparer.OrdinalIgnoreCase);

        public void AddFile(string path, IEnumerable<string> lines)
        {
            _files[path] = lines.Select(l => l ?? string.Empty).ToList();
        }

        public Task<IList<string>> ReadAllLinesAsync(string path, CancellationToken ct = default)
        {
            if (!_files.TryGetValue(path, out var lines))
                throw new System.IO.FileNotFoundException($"File not found: {path}", path);

            return Task.FromResult<IList<string>>(lines);
        }

        public Task WriteAllLinesAsync(string path, IEnumerable<string> lines, CancellationToken ct = default)
        {
            _files[path] = lines.Select(l => l ?? string.Empty).ToList();
            return Task.CompletedTask;
        }

        public bool Exists(string path) => _files.ContainsKey(path);

        // Helper to retrieve last written file (for assertions)
        public IReadOnlyList<string>? GetFile(string path) => _files.TryGetValue(path, out var v) ? v : null;
    }
}
