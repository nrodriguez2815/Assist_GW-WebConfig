using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
namespace Assist_WebConfig.Data
{
    public static class DapperORM
    {
        private static string _connectionString = Properties.Settings.Default.MainDB;
        private static string _clientFConnectionString = Properties.Settings.Default.ClientFDB;

        public static async Task ExecuteWithoutReturnAsync(string procedureName, DynamicParameters param = null)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                await sqlConnection.ExecuteAsync(procedureName, param, commandType: CommandType.StoredProcedure);
            }
        }
        public static IEnumerable<T> ReturnList<T>(string procedureName, DynamicParameters param = null)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                return sqlConnection.Query<T>(procedureName, param, commandType: CommandType.StoredProcedure);
            }
        }
        public static IEnumerable<T> ReturnList<T>(string db, string query)
        {
            string conn = _clientFConnectionString;

            if (db.Equals("MainDB"))
                conn = _connectionString;

            using (SqlConnection sqlConnection = new SqlConnection(conn))
            {
                return sqlConnection.Query<T>(query);
            }
        }
    }
}