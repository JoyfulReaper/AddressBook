using AddressBookDataAccess.Models.People;
using System;
using System.Collections.Generic;
using System.Text;

namespace AddressBookDataAccess.DataAccess
{
    public class AddressRepository : IAddressRepository
    {
        private readonly string connectionString;
        private SqliteDataAccess db = new SqliteDataAccess();

        public AddressRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void CreatePerson(Person person)
        {
            string sql = "insert into People (FirstName, LastName) values (@FirstName, @LastName);";

            db.SaveData<dynamic>(
                sql, 
                new { FirstName =  person.FirstName, LastName = person.LastName },
                connectionString);
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
