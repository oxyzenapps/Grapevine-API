//using Dapper;
//using System.Collections.Generic;
//using System.Data;
//using System.Threading.Tasks;

//namespace grapevineData.Interfaces
//{
//    public interface IDapperExecutor
//    {
//        Task<IEnumerable<T>> ExecuteAsync<T>(StoredProcedureRequest request);
//        Task ExecuteAsync(StoredProcedureRequest request);
//        Task<T> ExecuteScalarFunctionAsync<T>(string functionName, object parameters = null);
//        Task<IEnumerable<T>> ExecuteTableFunctionAsync<T>(string functionName, object parameters = null);
//        Task<T> ExecuteTableFunctionSingleAsync<T>(string functionName, object parameters = null);

//        // Fixed: Added proper generic QueryMultipleAsync overload for 5 result sets
//        Task<(IEnumerable<MediaTypeItem> MediaTypes,
//              IEnumerable<CampaignItem> Campaigns,
//              IEnumerable<SourceItem> Sources,
//              IEnumerable<StageItem> Stages,
//              IEnumerable<StatusItem> Statuses)>
//        QueryMultipleAsync<MediaTypeItem, CampaignItem, SourceItem, StageItem, StatusItem>(StoredProcedureRequest request);

//        // Generic version for flexibility
//        Task<(IEnumerable<T1> Item1, IEnumerable<T2> Item2)> QueryMultipleAsync<T1, T2>(StoredProcedureRequest request);
//    }
//}

using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace grapevineData.Interfaces
{
    public interface IDapperExecutor
    {
        Task<IEnumerable<T>> ExecuteAsync<T>(StoredProcedureRequest request);
        Task ExecuteAsync(StoredProcedureRequest request);
        Task<T> ExecuteScalarFunctionAsync<T>(string functionName, object parameters = null);
        Task<IEnumerable<T>> ExecuteTableFunctionAsync<T>(string functionName, object parameters = null);
        Task<T> ExecuteTableFunctionSingleAsync<T>(string functionName, object parameters = null);

        // Fixed: Return type is now generic and scannable
        Task<(IEnumerable<T1>, IEnumerable<T2>)> QueryMultipleAsync<T1, T2>(StoredProcedureRequest request);

        // Added: Required for interface implementation
        IDbConnection GetConnection();
    }
}