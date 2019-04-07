using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnixusTracking.Helper.Unixus.Models
{
    public class EventModel
    {
        public DateTime TransactionDate { get; set; }
        public string StrDate { get; set; }
        public string StrTime { get; set; }
        public string StationCode { get; set; }
        public string StationDescription { get; set; }
        public string CountryCode { get; set; }
        public string CountryDescription { get; set; }
        public string EventCode { get; set; }
        public string EventDescription { get; set; }
        public string ReasonCode { get; set; }
        public string ReasonDescription { get; set; }
        public string CompanyName { get; set; }
        public string StationName { get; set; }
        public string Remarks { get; set; }

        public bool MapVisibility
        {
            get
            {
                return !string.IsNullOrEmpty(StationName);
            }
        }
    }
}
