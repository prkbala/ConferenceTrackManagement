using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceTrackManagement
{
    public class Session
    {
        public Session(DateTime? startTime, DateTime? endTime = null)
        {
            StartTime = startTime;
            EndTime = endTime;
        }

        public DateTime? StartTime { get; private set; }
        public DateTime? EndTime { get; private set; }
    }
}
