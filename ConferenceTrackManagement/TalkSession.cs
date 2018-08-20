using System;
using System.Collections.Generic;

namespace ConferenceTrackManagement
{
    public class TalkSession : Session
    {
        public TalkSession(DateTime startTime, DateTime endTime) : base(startTime, endTime)
        {
        }

        public List<Talk> Talks { get; private set; }

        public TalkSessionType SessionType =>
            StartTime?.ToString("tt") == "AM" ? TalkSessionType.Morning : TalkSessionType.Evening;

        public void AddATalk(Talk talk)
        {
            if (Talks == null)
                Talks = new List<Talk>();

            Talks.Add(talk);
        }
    }
}