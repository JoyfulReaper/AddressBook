using AddressBookDataAccess.Models.People;
using System;
using System.Collections.Generic;
using System.Text;

namespace AddressBookDataAccess.DataAccess
{
    public interface IAddressRepository 
    {
        List<Person> GetPeople();
        Person GetPersonById(int id);
        void CreatePerson(Person person); // Issue: use a person DTO to create person along with contacts?
    }
}
