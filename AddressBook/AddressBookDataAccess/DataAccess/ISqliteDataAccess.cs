using System.Collections.Generic;

namespace AddressBookDataAccess.DataAccess
{
    public interface ISqliteDataAccess
    {
        List<T> LoadData<T, U>(string sqlStatement, U parameters, string connectionString);
        void SaveData<T>(string sqlStatement, T parameters, string connectionString);
    }
}