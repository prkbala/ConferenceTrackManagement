using System;

namespace ConferenceTrackManagement
{
    public class Talk
    {
        public Talk(string title, Duration duration)
        {
            Title = title;
            Duration = duration;
        }

        public string Title { get; }
        public Duration Duration { get; }
        public DateTime? StartTime { get; set; }
    }
}