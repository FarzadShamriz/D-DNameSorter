using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameSorter.Core.Domain
{
    public class Name
    {
        //Gets the last name of the individual.
        public string LastName { get; }

        //Gets the given names of the individual.
        public IReadOnlyList<string> GivenNames { get; }

        //Gets the original full name as a single string.
        public string OriginalName { get; }

        /// <summary>
        /// Constructor: Initializes a new instance of the Name class.
        /// if any of the arguments are null or invalid, appropriate exceptions are thrown.
        /// </summary>
        /// <param name="lastName">Last Name</param>
        /// <param name="givenNames">GIven Names</param>
        /// <param name="originalName">Original Name Read From The File.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public Name(string lastName, IReadOnlyList<string> givenNames, string originalName)
        {
            LastName = lastName ?? 
                throw new ArgumentNullException(nameof(lastName));
            GivenNames = givenNames ?? 
                throw new ArgumentNullException(nameof(givenNames));
            if (givenNames.Count == 0) 
                throw new ArgumentException("At least one given name is required", nameof(givenNames));
            OriginalName = originalName ?? 
                throw new ArgumentNullException(nameof(originalName));
        }

    }
}
