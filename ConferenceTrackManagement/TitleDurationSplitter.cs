using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConferenceTrackManagement
{
    internal interface ITitleDurationSplitter
    {
        List<Talk> GetTalkList(List<string> talkList);
    }

    class TitleDurationSplitter : ITitleDurationSplitter
    {
        private const string DurationMatchRegexString = @"(\b-?\d+\S*min\b)$";
        private const string DurationValueRegexString = @"-?\d+";
        private const string LightningString = @"lightning$";

        public virtual List<Talk> GetTalkList(List<string> inputTalkList)
        {
            var talkList = new List<Talk>();
            foreach (var talk in inputTalkList)
            {
                var duration = GetTalkDurationFromTalkString(talk);
                var titleStr = GetTalkTitleFromTalkString(talk);

                //if (durationStringAndValue.Duration <= ZeroTimeSpan || string.IsNullOrEmpty(titleStr))
                //{
                //    InValidTalks.Add(title);
                //    continue;
                //}
                talkList.Add(new Talk(titleStr, duration));
            }
            return talkList;
        }

        private static Duration GetTalkDurationFromTalkString(string talk)
        {
            var durationValue = 0;
            var durationPortion = Regex.Match(talk.ToLower(), DurationMatchRegexString);

            if (!string.IsNullOrEmpty(durationPortion.Value))
            {
                var durationStr = Regex.Match(durationPortion.Value, DurationValueRegexString);
                durationValue = Convert.ToInt32(durationStr.Value);
            }
            else
            {
                durationPortion = Regex.Match(talk.ToLower(), LightningString);
                if (!string.IsNullOrEmpty(durationPortion.Value))
                    durationValue = ConferenceSettings.LightningTalkDuration;
            }

            return new Duration
            {
                DurationPortionFromTitle = durationPortion.Value,
                DurationValue = durationValue,
                DurationInTimeSpan = new TimeSpan(0, durationValue, 0)
            };
        }

        private static string GetTalkTitleFromTalkString(string title)
        {
            var titleStr = Regex.Split(title, DurationMatchRegexString);
            if (new List<string>(titleStr).Count < 2)
            {
                titleStr = Regex.Split(title, LightningString);
            }
            return titleStr[0];
        }       
    }
}
