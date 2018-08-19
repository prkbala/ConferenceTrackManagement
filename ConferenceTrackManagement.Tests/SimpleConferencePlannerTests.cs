using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceTrackManagement.Tests
{
    [TestFixture]
    class SimpleConferencePlannerTests
    {
        [Test]
        public void PlanAConferenceTest()
        {
            //Arrange
            var inputTalkList = new List<string>
            {
                "Writing Fast Tests Against Enterprise Rails 60min",
                "Lua for the Masses 30min",
                "Ruby Errors from Mismatched Gem Versions 45min",
                "Accounting-Driven Development 45min",
                "Rails for Python Developers lightning",
                "Sit Down and Write 30min",
                "Rails Magic 60min",
                "Clojure Ate Scala (on my project) 45min",
                "Ruby vs. Clojure for Back-End Development 30min",
                "A World Without HackerNews 30min"
            };
            var conferenceStartDate = new DateTime(2018, 8, 24);
            var talk1 = new Talk(
                "Writing Fast Tests Against Enterprise Rails",
                new Duration
                {
                    DurationInTimeSpan = new TimeSpan(1, 0, 0),
                    DurationPortionFromTitle = "60min"
                });
            var talk2 = new Talk(
                "Lua for the Masses",
                new Duration
                {
                    DurationInTimeSpan = new TimeSpan(0, 30, 0),
                    DurationPortionFromTitle = "30min"
                });
            var talk3 = new Talk(
                "Ruby Errors from Mismatched Gem Versions",
                new Duration
                {
                    DurationInTimeSpan = new TimeSpan(0, 45, 0),
                    DurationPortionFromTitle = "45min"
                });
            var talk4 = new Talk(
                "Accounting-Driven Development",
                new Duration
                {
                    DurationInTimeSpan = new TimeSpan(0, 45, 0),
                    DurationPortionFromTitle = "45min"
                });
            var talk5 = new Talk(
                "Rails for Python Developers",
                new Duration
                {
                    DurationInTimeSpan = new TimeSpan(0, 5, 0),
                    DurationPortionFromTitle = "lightning"
                });
            var talk6 = new Talk(
                "Sit Down and Write",
                new Duration
                {
                    DurationInTimeSpan = new TimeSpan(0, 30, 0),
                    DurationPortionFromTitle = "30min"
                });
            var talk7 = new Talk(
                "Rails Magic",
                new Duration
                {
                    DurationInTimeSpan = new TimeSpan(0, 60, 0),
                    DurationPortionFromTitle = "60min"
                });
            var talk8 = new Talk(
                "Clojure Ate Scala (on my project)",
                new Duration
                {
                    DurationInTimeSpan = new TimeSpan(0, 45, 0),
                    DurationPortionFromTitle = "45min"
                });
            var talk9 = new Talk(
                "Ruby vs. Clojure for Back-End Development",
                new Duration
                {
                    DurationInTimeSpan = new TimeSpan(0, 30, 0),
                    DurationPortionFromTitle = "30min"
                });
            var talk10 = new Talk(
                "A World Without HackerNews",
                new Duration
                {
                    DurationInTimeSpan = new TimeSpan(0, 30, 0),
                    DurationPortionFromTitle = "30min"
                });

            var morningTalkSession = new TalkSession(
                conferenceStartDate.Date + ConferenceSettings.MorningSessionStart,
                conferenceStartDate.Date + ConferenceSettings.MorningSessionEnd);
            var eveningTalkSession = new TalkSession(
                conferenceStartDate.Date + ConferenceSettings.EveningSessionStart,
                conferenceStartDate.Date + ConferenceSettings.EveningSessionEnd);
            talk1.StartTime = morningTalkSession.StartTime;
            morningTalkSession.AddATalk(talk1);
            talk2.StartTime = talk1.StartTime + talk1.Duration.DurationInTimeSpan;
            morningTalkSession.AddATalk(talk2);
            talk3.StartTime = talk2.StartTime + talk2.Duration.DurationInTimeSpan;
            morningTalkSession.AddATalk(talk3);
            talk4.StartTime = talk3.StartTime + talk3.Duration.DurationInTimeSpan;
            morningTalkSession.AddATalk(talk4);
            talk5.StartTime = eveningTalkSession.StartTime;
            eveningTalkSession.AddATalk(talk5);
            talk6.StartTime = talk5.StartTime + talk5.Duration.DurationInTimeSpan;
            eveningTalkSession.AddATalk(talk6);
            talk7.StartTime = talk6.StartTime + talk6.Duration.DurationInTimeSpan;
            eveningTalkSession.AddATalk(talk7);
            talk8.StartTime = talk7.StartTime + talk7.Duration.DurationInTimeSpan;
            eveningTalkSession.AddATalk(talk8);
            talk9.StartTime = talk8.StartTime + talk8.Duration.DurationInTimeSpan;
            eveningTalkSession.AddATalk(talk9);
            talk10.StartTime = talk9.StartTime + talk9.Duration.DurationInTimeSpan;
            eveningTalkSession.AddATalk(talk10);

            var track = new Track(conferenceStartDate);
            track.AddASession(morningTalkSession);
            track.AddASession(eveningTalkSession);

            var expectedConference = new Conference();
            expectedConference.AddATrack(track);

            //Action
            var conferencePlanner = new SimpleConferencePlanner(new TitleDurationSplitter());
            var actualConference = conferencePlanner.PlanAConference(conferenceStartDate, inputTalkList);

            //Assert
            Assert.AreEqual(expectedConference.Tracks.Count, actualConference.Tracks.Count);
            Assert.AreEqual(expectedConference.Tracks[0].TalkSessions.Count,
                actualConference.Tracks[0].TalkSessions.Count);

            Assert.AreEqual(expectedConference.Tracks[0].TalkSessions[0].Talks.Count,
                actualConference.Tracks[0].TalkSessions[0].Talks.Count);
            Assert.AreEqual(expectedConference.Tracks[0].TalkSessions[1].Talks.Count,
                actualConference.Tracks[0].TalkSessions[1].Talks.Count);

            Assert.AreEqual(expectedConference.Tracks[0].TalkSessions[0].Talks[0].Title,
                actualConference.Tracks[0].TalkSessions[0].Talks[0].Title);
            Assert.AreEqual(expectedConference.Tracks[0].TalkSessions[0].Talks[0].Duration.DurationPortionFromTitle,
                actualConference.Tracks[0].TalkSessions[0].Talks[0].Duration.DurationPortionFromTitle);
            Assert.AreEqual(expectedConference.Tracks[0].TalkSessions[0].Talks[0].StartTime,
                actualConference.Tracks[0].TalkSessions[0].Talks[0].StartTime);

            Assert.AreEqual(expectedConference.Tracks[0].TalkSessions[0].Talks[3].Title,
                actualConference.Tracks[0].TalkSessions[0].Talks[3].Title);
            Assert.AreEqual(expectedConference.Tracks[0].TalkSessions[0].Talks[3].Duration.DurationPortionFromTitle,
                actualConference.Tracks[0].TalkSessions[0].Talks[3].Duration.DurationPortionFromTitle);
            Assert.AreEqual(expectedConference.Tracks[0].TalkSessions[0].Talks[3].StartTime,
                actualConference.Tracks[0].TalkSessions[0].Talks[3].StartTime);

            Assert.AreEqual(expectedConference.Tracks[0].TalkSessions[1].Talks[0].Title,
                actualConference.Tracks[0].TalkSessions[1].Talks[0].Title);
            Assert.AreEqual(expectedConference.Tracks[0].TalkSessions[1].Talks[0].Duration.DurationPortionFromTitle,
                actualConference.Tracks[0].TalkSessions[1].Talks[0].Duration.DurationPortionFromTitle);
            Assert.AreEqual(expectedConference.Tracks[0].TalkSessions[1].Talks[0].StartTime,
                actualConference.Tracks[0].TalkSessions[1].Talks[0].StartTime);

            Assert.AreEqual(expectedConference.Tracks[0].TalkSessions[1].Talks[5].Title,
                actualConference.Tracks[0].TalkSessions[1].Talks[5].Title);
            Assert.AreEqual(expectedConference.Tracks[0].TalkSessions[1].Talks[5].Duration.DurationPortionFromTitle,
                actualConference.Tracks[0].TalkSessions[1].Talks[5].Duration.DurationPortionFromTitle);
            Assert.AreEqual(expectedConference.Tracks[0].TalkSessions[1].Talks[5].StartTime,
                actualConference.Tracks[0].TalkSessions[1].Talks[5].StartTime);
        }
    }
}
