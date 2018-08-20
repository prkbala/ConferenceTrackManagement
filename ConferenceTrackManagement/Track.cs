using System;
using System.Collections.Generic;

namespace ConferenceTrackManagement
{
    public class Track
    {
        public Track(DateTime trackDay)
        {
            LunchSession = new Session(trackDay.Date + ConferenceSettings.LunchSessionStart);
            NetworkingSession = new Session(trackDay.Date + ConferenceSettings.NetworkingSessionStart);
        }

        public List<TalkSession> TalkSessions { get; private set; }
        public Session LunchSession { get; }
        public Session NetworkingSession { get; }

        public void AddASession(TalkSession session)
        {
            if (TalkSessions == null)
                TalkSessions = new List<TalkSession>();

            TalkSessions.Add(session);
        }
    }
}