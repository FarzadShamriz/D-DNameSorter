using NameSorter.Core.Domain;
using NameSorter.Core.SortHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameSorter.Tests.SortHelper
{
    public class NamesSorterTest
    {
        private readonly NamesSorter _sorter = new();

        [Fact]
        public void Sort_AllLastNamesUnique_SucceedsAndPreservesOrderByLastName()
        {
            var list = new[]
            {
                new Name("Adams", new[] { "Zoe" }, "Zoe Adams"),
                new Name("Brown", new[] { "Alice" }, "Alice Brown"),
                new Name("Clark", new[] { "Bob" }, "Bob Clark")
            };

            var sorted = _sorter.Sort(list).ToList();

            Assert.Equal(3, sorted.Count);
            Assert.Equal("Zoe Adams", sorted[0].OriginalName);
            Assert.Equal("Alice Brown", sorted[1].OriginalName);
            Assert.Equal("Bob Clark", sorted[2].OriginalName);
        }

        [Fact]
        public void Sort_TwoSameLastNames_ThrowsInvalidOperationExceptionDueToMissingGivenNamesComparer()
        {
            var list = new[]
            {
                new Name("Smith", new[] { "John" }, "John Smith"),
                new Name("Smith", new[] { "Adam" }, "Adam Smith")
            };

            Assert.Throws<InvalidOperationException>(() => _sorter.Sort(list).ToList());
        }
    }
}
