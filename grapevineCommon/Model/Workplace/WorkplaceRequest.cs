using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace grapevineCommon.Model.Workplace
{
    public class UpdateFilterRequest
    {
        public int[] FilterParameter { get; set; }
        public string[] Parameter1 { get; set; }
        public string[] Parameter2 { get; set; }
        public int SearchForId { get; set; }
        public string SearchID { get; set; } = "0";
        public string CityID { get; set; } = "0";
        public string LocalityID { get; set; } = "0";
        public string DistanceInKm { get; set; } = "0";
    }
}