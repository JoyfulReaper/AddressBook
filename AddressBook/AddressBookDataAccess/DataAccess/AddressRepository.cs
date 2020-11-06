﻿using AddressBookDataAccess.Models.Contact;
using AddressBookDataAccess.Models.People;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AddressBookDataAccess.DataAccess
{
    public class AddressRepository : IAddressRepository
    {
        private readonly string connectionString;
        private readonly ISqliteDataAccess db;

        public AddressRepository(string connectionString, ISqliteDataAccess db)
        {
            this.connectionString = connectionString;
            this.db = db;
        }

        public void CreatePerson(Person person)
        {
            try
            {
                db.StartTransaction(connectionString);

                string sql = "insert into People (FirstName, LastName) values (@FirstName, @LastName);"
                + "select last_insert_rowid();";

                if (string.IsNullOrWhiteSpace(person.FirstName) || string.IsNullOrWhiteSpace(person.LastName))
                {
                    throw new ArgumentException("Non nullable property of person was not properly set.");
                }

                int id = db.LoadDataInTransaction<int, dynamic>(
                    sql,
                    new { FirstName = person.FirstName, LastName = person.LastName })
                    .FirstOrDefault();

                if (person.EmailAddresses.Count > 0)
                {
                    person.EmailAddresses.ForEach(e => e.PersonId = id);

                    sql = "insert into EmailAddresses (PersonId, EmailAddress) values (@PersonId, @EmailAddress);";

                    db.SaveDataInTransaction(
                        sql,
                        person.EmailAddresses);
                    // passing list causes dapper to iterate over it automatically
                    // however @params must match db cols for mapping purposes
                }

                if (person.Addresses.Count > 0)
                {
                    person.Addresses.ForEach(a => a.PersonId = id);

                    sql = "insert into Addresses (PersonId, StreetAddress, City, Suburb, State, PostCode, IsMailAddress) values " +
                    "(@PersonId, @StreetAddress, @City, @Suburb, @State, @PostCode, @IsMailAddress);";

                    db.SaveDataInTransaction(
                        sql,
                        person.Addresses);
                }

                if (person.PhoneNumbers.Count > 0)
                {
                    person.PhoneNumbers.ForEach(p => p.PersonId = id);

                    sql = "insert into PhoneNumbers (PersonId, Number) values (@PersonId, @Number);";

                    db.SaveDataInTransaction(
                        sql,
                        person.PhoneNumbers);
                }


                db.CommitTransaction();
            }
            catch (Exception e)
            {
                db.RollbackTransaction();
                throw e;
                // throw? See error handling in demo
            }
        }

        public List<Person> GetPeople()
        {
            // temporarily disabled for interfering with tests
            //if (!System.IO.File.Exists("./AddressBook.db")) return null;
            string sql = "SELECT * FROM People";

            var people = db.LoadData<Person, dynamic>(
                sql,
                new { },
                connectionString);

            // Call test method. Remove in the future.
            TestDb(people: people);

            return people;
        }

        public Person GetPersonById(int id)
        {
            //if (!System.IO.File.Exists("./AddressBook.db")) return null;
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
