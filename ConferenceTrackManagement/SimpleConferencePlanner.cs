using System;
using System.Collections.Generic;

namespace ConferenceTrackManagement
{
    public interface IConferencePlanner
    {
        Conference PlanAConference(DateTime conferenceStartDate, List<string> inputTalkList);
    }

    public class SimpleConferencePlanner : IConferencePlanner
    {
        private readonly ITitleDurationSplitter _titleDurationSplitter;
        private readonly Conference _conference;

        public SimpleConferencePlanner(ITitleDurationSplitter titleDurationSplitter)
        {
            _titleDurationSplitter = titleDurationSplitter;
            _conference = new Conference();
        }

        public virtual Conference PlanAConference(DateTime conferenceDate, List<string> inputTalkList)
        {
            var talkList = _titleDurationSplitter.GetTalkList(inputTalkList);

            var currentSession = CreateANewTalkSession(conferenceDate);
            var currentTrack = new Track(conferenceDate);
            currentTrack.AddASession(currentSession);

            _conference.AddATrack(currentTrack);

            var currentSessionTime = currentSession.StartTime;

            foreach (var talk in talkList)
            {
                if (FitTalkIntoTheSession(talk, currentSession, ref currentSessionTime))
                    continue;

                //couldn't fit into the current session, so create necessary session and track
                var currentTrackAndSession = AddNecessaryTrackAndSession(currentSession, currentTrack);
                currentTrack = currentTrackAndSession.Item1;
                currentSession = currentTrackAndSession.Item2;
                currentSessionTime = currentSession.StartTime;
                FitTalkIntoTheSession(talk, currentSession, ref currentSessionTime);
            }
            return _conference;
        }

        private Tuple<Track, TalkSession> AddNecessaryTrackAndSession(TalkSession currentSession, Track currentTrack)
        {
            if (currentSession.SessionType == TalkSessionType.Morning)
            {
                //if current session is morning, create an evening session and add that to the current track
                currentSession = CreateANewTalkSession(currentSession.StartTime.Value.Date, true);
                currentTrack.AddASession(currentSession);
            }
            else
            {
                //if current session is evening, create a new track and a morning session and add that session to the new track
                currentSession = CreateANewTalkSession(currentSession.StartTime.Value.Date);
                currentTrack = new Track(currentSession.StartTime.Value.Date);
                currentTrack.AddASession(currentSession);
                _conference.AddATrack(currentTrack);
            }
            
            return new Tuple<Track, TalkSession>(currentTrack, currentSession);
        }

        private static bool FitTalkIntoTheSession(Talk talk, TalkSession session, ref DateTime? currentSessionTime)
        {
            if (talk.Duration.DurationInTimeSpan + currentSessionTime?.TimeOfDay >
                session.EndTime?.TimeOfDay) return false;

            talk.StartTime = currentSessionTime;
            currentSessionTime = currentSessionTime + talk.Duration.DurationInTimeSpan;
            session.AddATalk(talk);
            return true;
        }

        private static TalkSession CreateANewTalkSession(DateTime sessionDay, bool isEveningSession = false)
        {
            TimeSpan startTimeSpan;
            TimeSpan endTimeSpan;
            if (isEveningSession)
            {
                startTimeSpan = ConferenceSettings.EveningSessionStart;
                endTimeSpan = ConferenceSettings.EveningSessionEnd;
            }
            else
            {
                startTimeSpan = ConferenceSettings.MorningSessionStart;
                endTimeSpan = ConferenceSettings.MorningSessionEnd;
            }

            var startTime = sessionDay.Date + startTimeSpan;
            var endTime = sessionDay.Date + endTimeSpan;

            return new TalkSession(startTime, endTime);
        }
    }
}