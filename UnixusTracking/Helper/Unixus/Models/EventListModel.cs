using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnixusTracking.Helper.Unixus.Models
{
    public class EventListModel
    {
        public string HawbNo { get; set; }
        public IEnumerable<EventModel> Events { get; set; }
    }
}
