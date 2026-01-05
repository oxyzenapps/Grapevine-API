using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grapevineData
{
    public class StoredProcedureRequest
    {
        public string ProcedureName { get; set; }
        public DynamicParameters Parameters { get; set; }
    }
}
