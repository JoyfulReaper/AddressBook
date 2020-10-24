using AddressBookDataAccess.Models.People;
using System;
using System.Collections.Generic;
using System.Text;

namespace AddressBookDataAccess.DataAccess
{
    public class AddressRepository : IAddressRepository
    {
        private readonly string connectionString;

        public AddressRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void CreatePerson(Person person)
        {
            throw new NotImplementedException();
        }

        public List<Person> GetPeople()
        {
            throw new NotImplementedException();
        }

        public Person GetPersonById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
