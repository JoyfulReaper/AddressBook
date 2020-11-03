using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace AddressBookDataAccess.DataAccess
{
    public class SqliteDataAccess : ISqliteDataAccess
    {
        public List<T> LoadData<T, U>(string sqlStatement, U parameters, string connectionString)
        {
            using (IDbConnection connection = new SQLiteConnection(connectionString))
            {
                // U parameters are used by dapper as the args to the query, if they exist
                List<T> rows = connection.Query<T>(sqlStatement, parameters).ToList();
                return rows;
            }
        }

        public void SaveData<T>(string sqlStatement, T parameters, string connectionString)
        {
            using (IDbConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Execute(sqlStatement, parameters);
            }
        }

        // Transaction logic

        private IDbConnection connection;
        private IDbTransaction transaction;
        private bool isClosed = false;

        public void StartTransaction(string connectionString)
        {
            connection = new SQLiteConnection(connectionString);
            connection.Open();
            transaction = connection.BeginTransaction();

            // keep track of open transaction
            isClosed = false;
        }

        public void SaveDataInTransaction<T>(string sql, T parameters)
        {
            connection.Execute(sql, parameters, transaction: transaction);
        }

        public List<T> LoadDataInTransaction<T, U>(string sql, U parameters)
        {
            List<T> rows = connection.Query<T>(sql, parameters, transaction: transaction)
                .ToList();
                
            return rows;
        }

        public void CommitTransaction()
        {
            transaction?.Commit();
            connection?.Close();

            isClosed = true;
        }

        public void RollbackTransaction()
        {
            transaction?.Rollback();
            connection?.Close();

            isClosed = true;
        }

        // this is called at the end of a 'using' statement involving this class
        public void Dispose()
        {
            if (isClosed == false)
            {
                try
                {
                    CommitTransaction();
                }
                catch (Exception ex)
                {
                    // TODO: Log this                    
                }
            }

            transaction = null;
            connection = null;
        }
    }
}
