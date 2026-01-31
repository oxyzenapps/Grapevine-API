
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using grapevineData.Interfaces;

namespace grapevineData
{
    public class DapperSqlExecutor : IDapperExecutor
    {
        private readonly string _connStr;

        public DapperSqlExecutor(IConfiguration config)
        {
            _connStr = config.GetConnectionString("DefaultConnection")
                      ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        }

        public IDbConnection GetConnection()
        {
            return new SqlConnection(_connStr);
        }

        public async Task<IEnumerable<T>> ExecuteAsync<T>(StoredProcedureRequest request)
        {
            using var conn = new SqlConnection(_connStr);
            return await conn.QueryAsync<T>(
                request.ProcedureName,
                request.Parameters,
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task ExecuteAsync(StoredProcedureRequest request)
        {
            using var conn = new SqlConnection(_connStr);
            await conn.ExecuteAsync(
                request.ProcedureName,
                request.Parameters,
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<T> ExecuteScalarFunctionAsync<T>(string functionName, object? parameters = null)
        {
            using var conn = new SqlConnection(_connStr);
            var sql = $"SELECT {functionName}";
            return await conn.ExecuteScalarAsync<T>(sql, parameters);
        }

        public async Task<IEnumerable<T>> ExecuteTableFunctionAsync<T>(string functionName, object? parameters = null)
        {
            using var conn = new SqlConnection(_connStr);
            var sql = $"SELECT * FROM {functionName}";
            return await conn.QueryAsync<T>(sql, parameters);
        }

        public async Task<T> ExecuteTableFunctionSingleAsync<T>(string functionName, object? parameters = null)
        {
            using var conn = new SqlConnection(_connStr);
            var sql = $"SELECT * FROM {functionName}";
            return await conn.QueryFirstOrDefaultAsync<T>(sql, parameters);
        }

        public async Task<(IEnumerable<T1>, IEnumerable<T2>)> QueryMultipleAsync<T1, T2>(StoredProcedureRequest request)
        {
            using var connection = new SqlConnection(_connStr);
            using var multi = await connection.QueryMultipleAsync(
                request.ProcedureName,
                request.Parameters,
                commandType: CommandType.StoredProcedure
            );

            var first = await multi.ReadAsync<T1>();
            var second = await multi.ReadAsync<T2>();
            return (first, second);
        }

        public async Task<List<IEnumerable<dynamic>>> QueryMultipleDynamicAsync(StoredProcedureRequest request)
        {
            using var connection = new SqlConnection(_connStr);
            using var grid = await connection.QueryMultipleAsync(
                request.ProcedureName,
                request.Parameters,
                commandType: CommandType.StoredProcedure
            );

            var resultSets = new List<IEnumerable<dynamic>>();

            while (!grid.IsConsumed)
            {
                var rows = await grid.ReadAsync();
                resultSets.Add(rows);
            }

            return resultSets;
        }
    }
}