using AddressBookDataAccess.DataAccess;
using AddressBookDataAccess.Models.Contact;
using AddressBookDataAccess.Models.People;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Xunit;
using Xunit.Sdk;

namespace AddressBookDataAccessTests.DataAccess
{
    public class AddressRepositoryTests
    {
        private string conStr = "conStrMoq";
        private Mock<ISqliteDataAccess> db;
        private AddressRepository repo;

        // const sql queries
        const string sqlPerson = "insert into People (FirstName, LastName) values (@FirstName, @LastName);"
                + "select last_insert_rowid();";
        const string sqlPhone = "insert into PhoneNumbers (PersonId, Number) values (@PersonId, @Number);";
        const string sqlAddress = "insert into Addresses (PersonId, StreetAddress, City, Suburb, State, PostCode, IsMailAddress) values " +
                "(@PersonId, @StreetAddress, @City, @Suburb, @State, @PostCode, @IsMailAddress);";
        const string sqlEmail = "insert into EmailAddresses (PersonId, EmailAddress) values (@PersonId, @EmailAddress);";
                
        public AddressRepositoryTests()
        {
            db = new Mock<ISqliteDataAccess>();
            repo = new AddressRepository(conStr, db.Object);
        }
        
        #region Get methods

        [Fact]
        public void GetPeople_ReturnsListOfPerson()
        {
            // arrange
            var people = GetListOfSamplePeople();
            string sql = "SELECT * FROM People";
            db.Setup(x => x.LoadData<Person, dynamic>(sql, It.IsAny<object>(), conStr))
              .Returns(people);

            // result
            var expected = GetListOfSamplePeople();
            var actual = repo.GetPeople();

            // assert
            Assert.True(actual != null);
            Assert.Equal(expected.Count, actual.Count);
            for (int i = 0; i < actual.Count; i++)
            {
                Assert.Equal(expected[i].FullName, actual[i].FullName);
            }
        }

        [Theory]
        [InlineData(2)]
        public void GetPersonById_ReturnsPerson(int id)
        {
            var p = new List<Person>
            {
              new Person
              {
                Id = 2,
                FirstName = "Shaun",
                LastName = "Hobbs"
              }
            };

            string sql = "SELECT * FROM People WHERE Id = @id";

            db.Setup(x =>
            x.LoadData<Person, dynamic>(sql, It.IsAny<object>(), conStr)
            ).Returns(p);

            // result
            var expected = p.FirstOrDefault();
            var actual = repo.GetPersonById(id);

            // assert
            Assert.True(actual != null);
            Assert.Equal(expected, actual);
        }
        #endregion

        #region CreatePerson tests
        
        [Theory]
        [ClassData(typeof(TestData_PeopleWithInvalidNames))]
        // Tests person with pure whitespace, and one that is null, and one that is empty
        public void CreatePerson_PersonLackingNonNullablePropCausesRollback(Person p)
        {            
            var list = new List<int> { p.Id };

            db.Setup(x => x.LoadDataInTransaction<int, dynamic>
                (sqlPerson, It.IsAny<object>()))
                .Returns(list);
            
            Assert.Throws<ArgumentException>(() => repo.CreatePerson(p));

            db.Verify(x => x.StartTransaction(conStr), Times.Once());            
            db.Verify(x => x.RollbackTransaction(), Times.Once());

            db.Verify(x => x.LoadDataInTransaction<int, dynamic>(sqlPerson, p), Times.Never());
            // It.IsAny used for Save since all 3 calls should not be called
            db.Verify(x => x.SaveDataInTransaction<dynamic>(It.IsAny<string>(), p), Times.Never());
            db.Verify(x => x.CommitTransaction(), Times.Never());
        }

        [Fact]
        public void CreatePerson_SavePersonWithoutContactLists()
        {
            var p = SamplePeople()["PersonWithNoContactList"];
            var list = new List<int> { p.Id };
           
            db.Setup(x => x.LoadDataInTransaction<int, dynamic>
                (sqlPerson, It.IsAny<object>()))
                .Returns(list);
            
            repo.CreatePerson(p);

            db.Verify(x => x.StartTransaction(conStr), Times.Once());            
            db.Verify(x => x.LoadDataInTransaction<int, dynamic>(sqlPerson, It.IsAny<object>()), Times.Once());            
            db.Verify(x => x.CommitTransaction(), Times.Once());

            db.Verify(x => x.SaveDataInTransaction<dynamic>(It.IsAny<string>(), p), Times.Never());
            db.Verify(x => x.RollbackTransaction(), Times.Never());
        }

        [Fact]
        public void CreatePerson_SavePersonWithOneContactList()
        {
            var p = SamplePeople()["PersonWithPhoneList"];
            var list = new List<int> { p.Id };

            db.Setup(x => x.LoadDataInTransaction<int, dynamic>
                (sqlPerson, It.IsAny<object>()))
                .Returns(list);
            
            repo.CreatePerson(p);

            db.Verify(x => x.StartTransaction(conStr), Times.Once());
            db.Verify(x => x.LoadDataInTransaction<int, dynamic>(sqlPerson, It.IsAny<object>()), Times.Once());
            db.Verify(x => x.SaveDataInTransaction<dynamic>(sqlPhone, It.IsAny<object>()), Times.Once());            
            db.Verify(x => x.CommitTransaction(), Times.Once());

            db.Verify(x => x.SaveDataInTransaction<dynamic>(sqlAddress, It.IsAny<object>()), Times.Never());
            db.Verify(x => x.SaveDataInTransaction<dynamic>(sqlEmail, It.IsAny<object>()), Times.Never());
            db.Verify(x => x.RollbackTransaction(), Times.Never());
        }

        [Fact]
        public void CreatePerson_SavePersonWithTwoContactLists()
        {
            var p = SamplePeople()["PersonWithPhoneEmailList"];
            var list = new List<int> { p.Id };

            db.Setup(x => x.LoadDataInTransaction<int, dynamic>
                (sqlPerson, It.IsAny<object>()))
                .Returns(list);
            
            repo.CreatePerson(p);

            db.Verify(x => x.StartTransaction(conStr), Times.Once());
            db.Verify(x => x.LoadDataInTransaction<int, dynamic>(sqlPerson, It.IsAny<object>()), Times.Once());
            db.Verify(x => x.SaveDataInTransaction<dynamic>(sqlEmail, It.IsAny<object>()), Times.Once());
            db.Verify(x => x.SaveDataInTransaction<dynamic>(sqlPhone, It.IsAny<object>()), Times.Once());
            db.Verify(x => x.CommitTransaction(), Times.Once());

            db.Verify(x => x.RollbackTransaction(), Times.Never());
            db.Verify(x => x.SaveDataInTransaction<dynamic>(sqlAddress, It.IsAny<object>()), Times.Never());
        }

        [Fact]
        public void CreatePerson_SavePersonWithAllContactLists()
        {
            var p = SamplePeople()["PersonWithAddressEmailPhoneNumList"];
            var list = new List<int> { p.Id };

            db.Setup(x => x.LoadDataInTransaction<int, dynamic>
                (sqlPerson, It.IsAny<object>()))
                .Returns(list);

            repo.CreatePerson(p);

            db.Verify(x => x.StartTransaction(conStr), Times.Once());
            db.Verify(x => x.LoadDataInTransaction<int, dynamic>(sqlPerson, It.IsAny<object>()), Times.Once());
            db.Verify(x => x.SaveDataInTransaction<dynamic>(sqlEmail, It.IsAny<object>()), Times.Once());
            db.Verify(x => x.SaveDataInTransaction<dynamic>(sqlPhone, It.IsAny<object>()), Times.Once());
            db.Verify(x => x.SaveDataInTransaction<dynamic>(sqlAddress, It.IsAny<object>()), Times.Once());
            db.Verify(x => x.CommitTransaction(), Times.Once());

            db.Verify(x => x.RollbackTransaction(), Times.Never());
        }
        #endregion

        #region Dummy test data
        private List<Person> GetListOfSamplePeople()
        {
            return new List<Person>
            {
                new Person
                {
                    FirstName = "Ayva",
                    LastName = "Miranda",
                    EmailAddresses = new List<Email> { new Email { EmailAddress = "email@email.com" } },
                    Addresses = new List<Address> { new Address { City = "Hong Kong", State = "Tai Po", Suburb = "v123", StreetAddress = "test", PostCode = "999077" } },
                    PhoneNumbers = new List<PhoneNum> {new PhoneNum { Number = 123456789 } }
                },
                new Person
                {
                    FirstName = "Killian",
                    LastName = "Laing",
                    PhoneNumbers = new List<PhoneNum> { new PhoneNum { Number = 789456123} },
                    EmailAddresses = new List<Email> { new Email { EmailAddress = "zzz123" } }
                },
                new Person
                {
                    FirstName = "Olivier",
                    LastName = "Kline",
                    PhoneNumbers = new List<PhoneNum> { new PhoneNum { Number = 456123789 } }
                },
                new Person
                {
                    FirstName = "Van",
                    LastName = "Williams"
                }
            };
        }

        public Dictionary<string, Person> SamplePeople()
        {
            return new Dictionary<string, Person>
            {
                // person with full contact lists
                { "PersonWithAddressEmailPhoneNumList", GetListOfSamplePeople()[0] },
                // person with 2 contact lists
                { "PersonWithPhoneEmailList", GetListOfSamplePeople()[1] },
                // person with 1 contact list
                { "PersonWithPhoneList", GetListOfSamplePeople()[2] },
                // person with no contact lists
                { "PersonWithNoContactList", GetListOfSamplePeople()[3] }                
            };
        }
        #endregion
    }
}
