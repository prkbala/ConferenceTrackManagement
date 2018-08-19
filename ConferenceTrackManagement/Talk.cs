using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceTrackManagement
{
    public class Talk
    {
        public string Title { get; private set; }
        public Duration Duration { get; private set; }
        public DateTime? StartTime { get; set; }

        public Talk(string title, Duration duration)
        {
            Title = title;
            Duration = duration;
        }
    }
}
