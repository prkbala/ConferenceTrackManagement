using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceTrackManagement
{
    public class Track
    {
        public List<TalkSession> TalkSessions { get; private set; }
        public Session LunchSession { get; private set; }
        public Session NetworkingSession { get; private set; }
      
        public Track(DateTime trackDay)
        {
            LunchSession = new Session(trackDay.Date + ConferenceSettings.LunchSessionStart);
            NetworkingSession = new Session(trackDay.Date + ConferenceSettings.NetworkingSessionStart);
        }

        public void AddASession(TalkSession session)
        {
            if (TalkSessions == null)
                TalkSessions = new List<TalkSession>();

            TalkSessions.Add(session);
        }
    }
}
