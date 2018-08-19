using ConferenceTrackManagement;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceTrackManagement.Tests
{
    [TestFixture]
    public class IntegrationTests
    {
        [Test]
        public void IntegrationTestWithSample1()
        {
            //Arrange
            var inputTalkList = new List<string>
            {
                "Writing Fast Tests Against Enterprise Rails 60min",
                "Overdoing it in Python 45min",
                "Lua for the Masses 30min",
                "Ruby Errors from Mismatched Gem Versions 45min",
                "Common Ruby Errors 45min",
                "Rails for Python Developers lightning",
                "Communicating Over Distance 60min",
                "Accounting-Driven Development 45min",
                "Woah 30min",
                "Sit Down and Write 30min",
                "Pair Programming vs Noise 45min",
                "Rails Magic 60min",
                "Ruby on Rails: Why We Should Move On 60min",
                "Clojure Ate Scala (on my project) 45min",
                "Programming in the Boondocks of Seattle 30min",
                "Ruby vs. Clojure for Back-End Development 30min",
                "Ruby on Rails Legacy App Maintenance 60min",
                "A World Without HackerNews 30min",
                "User Interface CSS in Rails Apps 30min"
            };
            var conferenceStartDate = new DateTime(2018, 8, 24);
            var expectedOutputList = new List<string>
            {
                "Track 1:",
                "09:00AM Writing Fast Tests Against Enterprise Rails 60min",
                "10:00AM Overdoing it in Python 45min",
                "10:45AM Lua for the Masses 30min",
                "11:15AM Ruby Errors from Mismatched Gem Versions 45min",
                "12:00PM Lunch",
                "01:00PM Common Ruby Errors 45min",
                "01:45PM Rails for Python Developers lightning",
                "01:50PM Communicating Over Distance 60min",
                "02:50PM Accounting-Driven Development 45min",
                "03:35PM Woah 30min",
                "04:05PM Sit Down and Write 30min",
                "05:00PM Networking Event",
                "Track 2:",
                "09:00AM Pair Programming vs Noise 45min",
                "09:45AM Rails Magic 60min",
                "10:45AM Ruby on Rails: Why We Should Move On 60min",
                "12:00PM Lunch",
                "01:00PM Clojure Ate Scala (on my project) 45min",
                "01:45PM Programming in the Boondocks of Seattle 30min",
                "02:15PM Ruby vs. Clojure for Back-End Development 30min",
                "02:45PM Ruby on Rails Legacy App Maintenance 60min",
                "03:45PM A World Without HackerNews 30min",
                "04:15PM User Interface CSS in Rails Apps 30min",
                "05:00PM Networking Event"
            };

            //Action
            var conferencePlanner = new SimpleConferencePlanner(new TitleDurationSplitter());
            var conference = conferencePlanner.PlanAConference(conferenceStartDate, inputTalkList);
            var actualOutputList = new SimplePrintFormatter().GetPrintablePlan(conference);           

            //Assert
            Assert.AreEqual(expectedOutputList, actualOutputList);
        }

        [Test]
        public void IntegrationTestWithSample2()
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
            var expectedOutputList = new List<string>
            {
                "Track 1:",
                "09:00AM Writing Fast Tests Against Enterprise Rails 60min",
                "10:00AM Lua for the Masses 30min",
                "10:30AM Ruby Errors from Mismatched Gem Versions 45min",
                "11:15AM Accounting-Driven Development 45min",
                "12:00PM Lunch",
                "01:00PM Rails for Python Developers lightning",
                "01:05PM Sit Down and Write 30min",
                "01:35PM Rails Magic 60min",
                "02:35PM Clojure Ate Scala (on my project) 45min",
                "03:20PM Ruby vs. Clojure for Back-End Development 30min",
                "03:50PM A World Without HackerNews 30min",
                "05:00PM Networking Event"
            };

            //Action
            var conferencePlanner = new SimpleConferencePlanner(new TitleDurationSplitter());
            var conference = conferencePlanner.PlanAConference(conferenceStartDate, inputTalkList);
            var actualOutputList = new SimplePrintFormatter().GetPrintablePlan(conference);           

            //Assert
            Assert.AreEqual(expectedOutputList, actualOutputList);
        }
    }

}
