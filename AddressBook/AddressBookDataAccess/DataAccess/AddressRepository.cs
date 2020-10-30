using AddressBookDataAccess.Models.Contact;
using AddressBookDataAccess.Models.People;
using System;
using System.Collections.Generic;
using System.Linq;
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
            string sql = "insert into People (FirstName, LastName) values (@FirstName, @LastName)";

            db.SaveData<dynamic>(
                sql,
                new { FirstName = person.FirstName, LastName = person.LastName },
                connectionString);

        }

        public List<Person> GetPeople()
        {
            if (!System.IO.File.Exists("./AddressBook.db")) return null;
            string sql = "SELECT * FROM People";

            var people = db.LoadData<Person, dynamic>(
                sql,
                new { },
                connectionString).ToList();

            // Call test method. Remove in the future.
            TestDb(people: people);

            return people;
        }

        public Person GetPersonById(int id)
        {
            if (!System.IO.File.Exists("./AddressBook.db")) return null;
            string sql = "SELECT * FROM People WHERE Id = @id";

            var person = db.LoadData<Person, dynamic>(
                sql,
                new { Id = @id },
                connectionString).FirstOrDefault();

            // Call test method. Remove in the future.
            TestDb(person: person);

            return person;
        }

        public void TestDb(List<Person> people = null, Person person = null)
        {
            // Multiple People
            if (people != null)
            {
                foreach (var result in people)
                {
                    Console.WriteLine($"{ result.Id } : { result.FullName }");
                } 
            }

            // Single Person
            if (person != null)
            {
                Console.WriteLine($"{ person.Id } : { person.FullName }"); 
            }
        }
    }
}
