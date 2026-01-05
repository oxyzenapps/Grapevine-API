using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grapevineData.Interfaces
{
    public interface ISqlExecutor
    {
        Task<List<T>> ExecuteAsync<T>(
            string spName,
            List<IDbDataParameter>? parameters = null
        ) where T : new();

        Task<(List<T1>, List<T2>)> ExecuteMultipleAsync<T1, T2>(
            string spName,
            List<IDbDataParameter>? parameters = null
        ) where T1 : new() where T2 : new();

        Task<int> ExecuteNonQueryAsync(
            string spName,
            List<IDbDataParameter>? parameters = null
        );

        Task<TOut?> ExecuteWithOutputAsync<TOut>(
            string spName,
            List<IDbDataParameter> parameters,
            string outputParamName
        );
    }
}
