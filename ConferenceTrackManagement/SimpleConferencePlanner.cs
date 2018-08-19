using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceTrackManagement
{
    public interface IConferencePlanner
    {
        Conference PlanAConference(DateTime conferenceStartDate, List<string> inputTalkList);
    }

    public class SimpleConferencePlanner : IConferencePlanner
    {
        private ITitleDurationSplitter _titleDurationSplitter;

        public SimpleConferencePlanner(ITitleDurationSplitter titleDurationSplitter)
        { 
            _titleDurationSplitter = titleDurationSplitter;
        }

        public virtual Conference PlanAConference(DateTime conferenceStartDate, List<string> inputTalkList)
        {
            var talkList = _titleDurationSplitter.GetTalkList(inputTalkList);

            var currentSession = CreateANewTalkSession(conferenceStartDate);
            var track = new Track(conferenceStartDate);
            track.AddASession(currentSession);

            var conference = new Conference();
            conference.AddATrack(track);

            var currentSessionTime = currentSession.StartTime;

            foreach (var talk in talkList)
            {
                if (FitATalkIntoASession(talk, currentSession, ref currentSessionTime))
                    continue;

                if (currentSession.SessionType == TalkSessionType.Morning)
                {
                    currentSession = CreateANewTalkSession(currentSession.StartTime.Value.Date, true);
                    track.AddASession(currentSession);
                }
                else
                {
                    currentSession = CreateANewTalkSession(currentSession.StartTime.Value.Date.AddDays(1));
                    track = new Track(currentSession.StartTime.Value.Date);
                    track.AddASession(currentSession);
                    conference.AddATrack(track);
                }

                currentSessionTime = currentSession.StartTime;
                FitATalkIntoASession(talk, currentSession, ref currentSessionTime);
            }
            return conference;
        }

        private bool FitATalkIntoASession(Talk talk, TalkSession session, ref DateTime? currentSessionTime)
        {
            if ((talk.Duration.DurationInTimeSpan + currentSessionTime.Value.TimeOfDay) <= session.EndTime.Value.TimeOfDay)
            {
                talk.StartTime = currentSessionTime;
                currentSessionTime = currentSessionTime + talk.Duration.DurationInTimeSpan;
                session.AddATalk(talk);
                return true;
            }
            return false;
        }

        private TalkSession CreateANewTalkSession(DateTime sessionDay, bool isEveningSession = false)
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
