using NameSorter.Core.Application;
using NameSorter.Core.ParseHelper;
using NameSorter.Core.SortHelper;
using NameSorter.Tests.Fixtures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameSorter.Tests.Application
{
    public class NameSorterAppTest
    {

        [Fact]
        public async Task RunAsync_ParsesSortsAndWritesOutputFileAndReturnsZero()
        {
            var fileProvider = new InMemoryFileProvider();
            var inputPath = "input.txt";
            fileProvider.AddFile(inputPath, new[]
            {
            "Janet Parsons",
            "Vaughn Lewis",
            "Adonis Julius Archer",
            "Hunter Uriah Mathew Clarke",
            "Mikayla Lopez",
            "Frankie Conner Ritter"
        });

            var parser = new NameParser();
            var sorter = new NamesSorter();
            var useCase = new NameSorterApp(fileProvider, parser, sorter);

            using var stdout = new StringWriter();
            using var stderr = new StringWriter();

            var exit = await useCase.RunAsync(inputPath, stdout, stderr);

            Assert.Equal(0, exit);

            var outputLines = stdout.ToString().Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            Assert.NotEmpty(outputLines);

            // Verify alphabetical by last name
            var byLast = outputLines.Select(l => l.Split(' ', StringSplitOptions.RemoveEmptyEntries).Last()).ToList();
            var sortedByLast = byLast.OrderBy(s => s, System.StringComparer.OrdinalIgnoreCase).ToList();
            Assert.Equal(sortedByLast, byLast);

            // Verify file was written to expected filename
            var written = fileProvider.GetFile("sorted-names-list.txt");
            Assert.NotNull(written);
            Assert.Equal(outputLines.Length, written!.Count);
        }

        [Fact]
        public async Task RunAsync_InvalidLines_WriteWarningsToStderr()
        {
            var fileProvider = new InMemoryFileProvider();
            var inputPath = "input2.txt";
            fileProvider.AddFile(inputPath, new[]
            {
            "SingleToken",
            "  ",
            "Valid Person"
        });

            var parser = new NameParser();
            var sorter = new NamesSorter();
            var useCase = new NameSorterApp(fileProvider, parser, sorter);

            using var stdout = new StringWriter();
            using var stderr = new StringWriter();

            var exit = await useCase.RunAsync(inputPath, stdout, stderr);

            Assert.Equal(0, exit);

            var err = stderr.ToString();
            Assert.Contains("Warning", err, StringComparison.OrdinalIgnoreCase);
            Assert.Contains("SingleToken", err);
        }

    }
}
