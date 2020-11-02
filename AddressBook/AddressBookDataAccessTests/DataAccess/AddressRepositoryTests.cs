using AddressBookDataAccess.DataAccess;
using AddressBookDataAccess.Models.People;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Xunit;

namespace AddressBookDataAccessTests.DataAccess
{
    public class AddressRepositoryTests
    {
        private string conStr = "conStrMoq";
        private Mock<ISqliteDataAccess> db;
        private AddressRepository repo;

        public AddressRepositoryTests()
        {
            db = new Mock<ISqliteDataAccess>();
            repo = new AddressRepository(conStr, db.Object);
        }

        [Fact]
        public void GetPeople_ReturnsListOfPerson()
        {
            // arrange
            var people = GetSamplePeople();
            string sql = "SELECT * FROM People";
            db.Setup(x => x.LoadData<Person, dynamic>(sql, It.IsAny<object>(), conStr))
              .Returns(people);

            // result
            var expected = GetSamplePeople();
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


        private List<Person> GetSamplePeople()
        {
            return new List<Person>
            {
                new Person
                {
                    FirstName = "Ayva",
                    LastName = "Miranda"
                },
                new Person
                {
                    FirstName = "Killian",
                    LastName = "Laing"
                },
                new Person
                {
                    FirstName = "Olivier",
                    LastName = "Kline"
                },
            };
        }
    }
}
