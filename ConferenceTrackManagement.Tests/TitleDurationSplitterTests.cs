using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace ConferenceTrackManagement.Tests
{
    [TestFixture]
    internal class TitleDurationSplitterTests
    {
        [Test]
        public void GetTalkListTest()
        {
            //Arrange
            var inputTalkList = new List<string>
            {
                "Writing Fast Tests Against Enterprise Rails 60min",
                "Lua for the Masses lightning"
            };
            var expectedTalkList = new List<Talk>
            {
                new Talk(
                    "Writing Fast Tests Against Enterprise Rails",
                    new Duration
                    {
                        DurationInTimeSpan = new TimeSpan(1, 0, 0),
                        DurationPortionFromTitle = "60min"
                    }),
                new Talk(
                    "Lua for the Masses",
                    new Duration
                    {
                        DurationInTimeSpan = new TimeSpan(0, 5, 0),
                        DurationPortionFromTitle = "lightning"
                    })
            };

            //Action
            var actualTalkList = new TitleDurationSplitter().GetTalkList(inputTalkList);

            //Assert
            Assert.AreEqual(expectedTalkList.Count, actualTalkList.Count);
            Assert.AreEqual(expectedTalkList[0].Title, actualTalkList[0].Title);
            Assert.AreEqual(expectedTalkList[0].Duration.DurationInTimeSpan,
                actualTalkList[0].Duration.DurationInTimeSpan);
            Assert.AreEqual(expectedTalkList[0].Duration.DurationPortionFromTitle,
                actualTalkList[0].Duration.DurationPortionFromTitle);

            Assert.AreEqual(expectedTalkList[1].Title, actualTalkList[1].Title);
            Assert.AreEqual(expectedTalkList[1].Duration.DurationInTimeSpan,
                actualTalkList[1].Duration.DurationInTimeSpan);
            Assert.AreEqual(expectedTalkList[1].Duration.DurationPortionFromTitle,
                actualTalkList[1].Duration.DurationPortionFromTitle);
        }
    }
}