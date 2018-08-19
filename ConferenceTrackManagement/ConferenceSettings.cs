using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceTrackManagement
{
    public static class ConferenceSettings
    {
        public static int LightningTalkDuration = 5;
        public static TimeSpan MorningSessionStart = new TimeSpan(9, 0, 0);
        public static TimeSpan MorningSessionEnd = new TimeSpan(12, 0, 0);
        public static TimeSpan EveningSessionStart = new TimeSpan(13, 0, 0);
        public static TimeSpan EveningSessionEnd = new TimeSpan(17, 0, 0);
        public static TimeSpan LunchSessionStart = new TimeSpan(12, 0, 0);
        public static TimeSpan NetworkingSessionStart = new TimeSpan(17, 0, 0);
    }       
}
