using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grapevineData.Interfaces
{
    public interface IDapperExecutor
    {
        Task<IEnumerable<T>> ExecuteAsync<T>(StoredProcedureRequest request);
        Task ExecuteAsync(StoredProcedureRequest request);
        Task<T> ExecuteScalarFunctionAsync<T>(
            string functionName,
            object parameters = null);
        Task<IEnumerable<T>> ExecuteTableFunctionAsync<T>(
           string functionName,
           object parameters = null);
        Task<T> ExecuteTableFunctionSingleAsync<T>(
            string functionName,
            object parameters = null);
    }
}
