using System.Collections.Generic;

namespace AddressBookDataAccess.DataAccess
{
    public interface ISqliteDataAccess
    {
        List<T> LoadData<T, U>(string sqlStatement, U parameters, string connectionString);
        void SaveData<T>(string sqlStatement, T parameters, string connectionString);
        T LoadResultSets<T, U>(string sqlStatements, U parameters, string connectionString);

        void RollbackTransaction();
        List<T> LoadDataInTransaction<T, U>(string sqlStatement, U parameters);
        void SaveDataInTransaction<T>(string sqlStatement, T parameters);
        void StartTransaction(string connectionString);
        void CommitTransaction();
        void Dispose();
    }
}