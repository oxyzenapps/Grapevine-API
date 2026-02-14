using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Generic;

namespace grapevineCommon.Model.Homes
{
    public class UI_ACTION_RESULT
    {
        public List<object> Result { get; set; } = new List<object>();
    }

    public class HomeServiceResponse
    {
        public UI_ACTION_RESULT ActionResult { get; set; } = new UI_ACTION_RESULT();
    }
    

    public class HomeActionResultResponse
    {
        public UI_ACTION_RESULT ActionResult { get; set; } = new UI_ACTION_RESULT();
    }

    public class HomeResponse
    {
        public UI_ACTION_RESULT ActionResult { get; set; } = new UI_ACTION_RESULT();
    }

    
        public class ActionResponse
        {
            public List<string> Result { get; set; } = new List<string>();
        }
    public class BuildingAvailabilityResponse
    {
        public string Unit_ID { get; set; }
        public string Unit_No { get; set; }
        public string Status { get; set; }
        public string Floor_No { get; set; }
        // Add other fields returned by your SP (e.g., ColorCode, Area, etc.)
    }
    
        public class ApiResponse<T>
        {
            public T Result { get; set; }
            public string Message { get; set; }
            public bool Success { get; set; }
    }

   
        

        public class WrappedResponse
        {
            public UI_ACTION_RESULT ActionResult { get; set; }
        }

    public class TopLocalitiesResponse
    {
        public bool Success { get; set; }
        public string Data { get; set; }
    }

    public class HomesResponse
    {
        public bool Success { get; set; }
        public bool IsSuccess { get; set; }
        public object Data { get; set; }
    }
    public class APIResponse<T>
    {
        public T Result { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}





