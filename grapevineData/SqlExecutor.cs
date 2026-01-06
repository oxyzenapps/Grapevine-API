using grapevineData.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grapevineData
{
    public class SqlExecutor:ISqlExecutor
    {
        private readonly string _connStr;

        public SqlExecutor(IConfiguration config)
        {
            _connStr = config.GetConnectionString("DefaultConnection");
        }

        public async Task<List<T>> ExecuteAsync<T>(
            string spName,
            List<IDbDataParameter>? parameters = null
        ) where T : new()
        {
            using var conn = new SqlConnection(_connStr);
            using var cmd = new SqlCommand(spName, conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            if (parameters != null)
                cmd.Parameters.AddRange(parameters.ToArray());

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            return reader.MapToList<T>();
        }

        public async Task<(List<T1>, List<T2>)> ExecuteMultipleAsync<T1, T2>(
            string spName,
            List<IDbDataParameter>? parameters = null
        ) where T1 : new() where T2 : new()
        {
            using var conn = new SqlConnection(_connStr);
            using var cmd = new SqlCommand(spName, conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            if (parameters != null)
                cmd.Parameters.AddRange(parameters.ToArray());

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            var list1 = reader.MapToList<T1>();
            await reader.NextResultAsync();
            var list2 = reader.MapToList<T2>();

            return (list1, list2);
        }

        public async Task<int> ExecuteNonQueryAsync(
            string spName,
            List<IDbDataParameter>? parameters = null
        )
        {
            using var conn = new SqlConnection(_connStr);
            using var cmd = new SqlCommand(spName, conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            if (parameters != null)
                cmd.Parameters.AddRange(parameters.ToArray());

            await conn.OpenAsync();
            return await cmd.ExecuteNonQueryAsync();
        }

        public async Task<TOut?> ExecuteWithOutputAsync<TOut>(
            string spName,
            List<IDbDataParameter> parameters,
            string outputParamName
        )
        {
            using var conn = new SqlConnection(_connStr);
            using var cmd = new SqlCommand(spName, conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddRange(parameters.ToArray());

            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            return (TOut?)cmd.Parameters[outputParamName].Value;
        }
    }
}
