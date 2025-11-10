using NameSorter.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameSorter.Tests.Domain
{
    public class NameTest
    {


        [Fact]
        public void Name_Properties_AreSetCorrectly()
        {
            // Arrange
            var lastName = "Doe";
            var givenNames = new[] { "John", "Michael" };
            var originalName = "John Michael Doe";

            // Act
            var name = new Name(lastName, givenNames, originalName);

            // Assert
            Assert.Equal(lastName, name.LastName);
            Assert.Equal(givenNames, name.GivenNames);
            Assert.Equal(originalName, name.OriginalName);
        }

    }
}
