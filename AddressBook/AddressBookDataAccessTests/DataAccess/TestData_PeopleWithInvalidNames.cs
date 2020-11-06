using AddressBookDataAccess.Models.People;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AddressBookDataAccessTests.DataAccess
{
    public class TestData_PeopleWithInvalidNames : IEnumerable<object[]> 
    {
        private readonly List<object[]> People = new List<object[]>
        {
            new object[] { new Person {FirstName = "Bob", LastName = null} },
            new object[] { new Person { FirstName = "    ", LastName = "" }}                
        };

        public IEnumerator<object[]> GetEnumerator() => People.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
