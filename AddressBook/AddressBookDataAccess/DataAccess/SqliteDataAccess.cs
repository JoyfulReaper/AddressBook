using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Text;
using static Dapper.SqlMapper;

namespace AddressBookDataAccess.DataAccess
{
    public class SqliteDataAccess : ISqliteDataAccess, IDisposable
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

        public T LoadResultSets<T, U>(string sqlStatements, U parameters, string connectionString)
        {
            using (IDbConnection connection = new SQLiteConnection(connectionString))
            {
                var resultSet = connection.QueryMultiple(
                    sqlStatements, parameters);

                var baseObject = resultSet.ReadSingle<T>(); // object to be populated

                Dictionary<string, Type> propTypeLists = new Dictionary<string, Type>();

                // get all list properties from type T
                foreach (var prop in typeof(T).GetProperties())
                {
                    var type = prop.PropertyType;

                    if (type.IsGenericType && 
                        type.GetGenericTypeDefinition() == typeof(List<>))
                    {
                        propTypeLists.Add(prop.Name, type.GetGenericArguments()[0]);
                    }
                }

                foreach (var type in propTypeLists)
                {

                    var reader = typeof(GridReader).GetMethods()
                        .Where(x => x.Name == "Read")
                        .FirstOrDefault(x => x.IsGenericMethod);
                    reader = reader.MakeGenericMethod(type.Value);

                    var items = reader.Invoke(resultSet, new object[] { true });
                    var objectProperty = baseObject.GetType().GetProperty(type.Key);
                    objectProperty.SetValue(baseObject, items);
                }

                return baseObject;
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

        public void SaveDataInTransaction<T>(string sqlStatement, T parameters)
        {
            connection.Execute(sqlStatement, parameters, transaction: transaction);
        }

        public List<T> LoadDataInTransaction<T, U>(string sqlStatement, U parameters)
        {
            List<T> rows = connection.Query<T>(sqlStatement, parameters, transaction: transaction)
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
