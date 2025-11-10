using NameSorter.Core.Domain;
using NameSorter.Core.IOHelper;
using NameSorter.Core.ParseHelper;
using NameSorter.Core.SortHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameSorter.Core.Application
{
    public class NameSorterApp
    {

        private readonly IFileProvider _files;
        private readonly INameParser _parser;
        private readonly INamesSorter _sorter;
        private const string OutputFileName = "sorted-names-list.txt";

        public NameSorterApp(IFileProvider files, INameParser parser, INamesSorter sorter)
        {
            _files = files ?? 
                throw new ArgumentNullException(nameof(files));
            _parser = parser ?? 
                throw new ArgumentNullException(nameof(parser));
            _sorter = sorter ?? 
                throw new ArgumentNullException(nameof(sorter));
        }

        /// <summary>
        /// Returns 0 on success; non-zero for errors.
        /// Writes warnings to errorWriter; writes sorted lines to outputWriter and OutputFileName.
        /// </summary>
        public async Task<int> RunAsync(string inputPath, TextWriter? outputWriter = null, TextWriter? errorWriter = null, CancellationToken ct = default)
        {
            outputWriter ??= Console.Out;
            errorWriter ??= Console.Error;

            if (string.IsNullOrWhiteSpace(inputPath))
            {
                errorWriter.WriteLine("Input path is required.");
                return 1;
            }

            if (!_files.Exists(inputPath))
            {
                errorWriter.WriteLine($"Input file not found: {inputPath}");
                return 2;
            }

            IList<string> rawLines = await _files.ReadAllLinesAsync(inputPath, ct).ConfigureAwait(false);

            var valid = new List<Name>();
            int lineNumber = 0;

            foreach (var raw in rawLines)
            {
                lineNumber++;
                if (_parser.TryParse(raw, out var name, out var error))
                {
                    valid.Add(name!);
                }
                else
                {
                    errorWriter.WriteLine($"Warning (line {lineNumber}): {error} Line: \"{raw}\"");
                }
            }

            var sorted = _sorter.Sort(valid).ToList();
            var outputLines = sorted.Select(n => n.OriginalName).ToList();

            foreach (var line in outputLines)
                outputWriter.WriteLine(line);

            await _files.WriteAllLinesAsync(OutputFileName, outputLines, ct).ConfigureAwait(false);

            return 0;
        }

    }
}
