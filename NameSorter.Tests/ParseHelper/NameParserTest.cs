using NameSorter.Core.Application;
using NameSorter.Core.ParseHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameSorter.Tests.ParseHelper
{
    public class NameParserTest
    {

        private readonly NameParser _parser = new();

        [Theory]
        [InlineData("Janet Parsons", "Parsons", 1)]
        [InlineData("Adonis Julius Archer", "Archer", 2)]
        [InlineData("Hunter Uriah Mathew Clarke", "Clarke", 3)]
        public void TryParse_ValidInputs_ParsesLastNameAndGivenNames(string input, string expectedLast, int expectedGivenCount)
        {
            var ok = _parser.TryParse(input, out var name, out var error);

            Assert.True(ok);
            Assert.Null(error);
            Assert.NotNull(name);
            Assert.Equal(expectedLast, name!.LastName);
            Assert.Equal(expectedGivenCount, name.GivenNames.Count);
            Assert.Equal(input.Trim().Replace("\t", " "), name.OriginalName);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("SingleToken")]
        public void TryParse_InvalidInputs_ReturnsFalse(string input)
        {
            var ok = _parser.TryParse(input, out var name, out var error);

            Assert.False(ok);
            Assert.Null(name);
            Assert.False(string.IsNullOrWhiteSpace(error));
        }

        [Fact]
        public void TryParse_TooManyTokens_ReturnsFalse()
        {
            var input = "a b c d e";
            var ok = _parser.TryParse(input, out var name, out var error);

            Assert.False(ok);
            Assert.Null(name);
            Assert.Contains("up to three given names", error ?? string.Empty, StringComparison.OrdinalIgnoreCase);
        }

    }
}
