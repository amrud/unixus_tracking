using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnixusTracking.Helper.Unixus.Models
{
    class TrackDetailsResponseModel
    {
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public IEnumerable<EventListModel> EventList { get; set; }
    }
}
