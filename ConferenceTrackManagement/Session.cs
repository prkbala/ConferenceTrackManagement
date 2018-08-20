using System;

namespace ConferenceTrackManagement
{
    public class Session
    {
        public Session(DateTime? startTime, DateTime? endTime = null)
        {
            StartTime = startTime;
            EndTime = endTime;
        }

        public DateTime? StartTime { get; }
        public DateTime? EndTime { get; }
    }
}