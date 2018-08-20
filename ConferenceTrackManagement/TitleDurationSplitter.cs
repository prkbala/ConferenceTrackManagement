using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ConferenceTrackManagement
{
    public interface ITitleDurationSplitter
    {
        List<Talk> GetTalkList(List<string> talkList);
    }

    public class TitleDurationSplitter : ITitleDurationSplitter
    {
        private const string DurationMatchRegexString = @" (\b-?\d+\S*min\b)$";
        private const string DurationValueRegexString = @"-?\d+";
        private const string LightningString = @" lightning$";

        public virtual List<Talk> GetTalkList(List<string> inputTalkList)
        {
            return (from talk in inputTalkList
                let duration = GetTalkDurationFromTalkString(talk)
                let titleStr = GetTalkTitleFromTalkString(talk)
                select new Talk(titleStr, duration)).ToList();
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
                DurationPortionFromTitle = durationPortion.Value.TrimStart(' '),
                DurationInTimeSpan = new TimeSpan(0, durationValue, 0)
            };
        }

        private static string GetTalkTitleFromTalkString(string title)
        {
            var titleStr = Regex.Split(title, DurationMatchRegexString);
            if(titleStr.Length < 2)
                titleStr = Regex.Split(title, LightningString);
            return titleStr[0];
        }
    }
}