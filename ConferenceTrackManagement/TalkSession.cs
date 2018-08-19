using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceTrackManagement
{
    public class TalkSession : Session
    {
        public List<Talk> Talks { get; private set; }
        public SessionType SessionType
        {
            get
            {
                if (StartTime.Value.ToString("tt") == "AM")
                    return SessionType.Morning;
                else
                    return SessionType.Evening;
            }
        }

        public TalkSession(DateTime startTime, DateTime endTime) : base(startTime, endTime)
        {}

        public void AddATalk(Talk talk)
        {
            if (Talks == null)
                Talks = new List<Talk>();

            Talks.Add(talk);
        }        
    }
}
