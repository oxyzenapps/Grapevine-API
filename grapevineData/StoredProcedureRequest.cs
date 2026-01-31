

namespace grapevineData.Interfaces
{
    public interface IStoredProcedureRequest
    {
        string ProcedureName { get; set; }
        object Parameters { get; set; }
    }
}
