using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnixusTracking.Helper.Unixus
{
    public delegate void OnServiceCallback<T>(T result, string error);
}
