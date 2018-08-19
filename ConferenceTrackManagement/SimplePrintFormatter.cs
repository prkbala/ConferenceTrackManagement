using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceTrackManagement
{
    public interface IPrintFormatter
    {
        List<string> GetPrintablePlan(Conference conference);
    }

    public class SimplePrintFormatter : IPrintFormatter
    {
        private const string TimeFormat = "hh:mmtt";
        private const string Space = " ";

        public virtual List<string> GetPrintablePlan(Conference conference)
        {
            if (conference == null || conference.Tracks == null)
                return null;

            var scheduleList = new List<string>();
            for (var index = 0; index < conference.Tracks.Count; index++)
            {
                var track = conference.Tracks[index];
                scheduleList.Add(string.Format("Track {0}:", index + 1));
                GeneratePrintableTrack(track, scheduleList);
            }
            return scheduleList;
        }

        private static void GeneratePrintableTrack(Track track, List<string> scheduleList)
        {
            foreach (var talkSession in track.TalkSessions)
            {
                if (talkSession.SessionType == TalkSessionType.Morning)
                {
                    foreach (var talk in talkSession.Talks)
                    {
                        GetPrintableTalk(scheduleList, talk);
                    }
                    scheduleList.Add(string.Format($"{track.LunchSession.StartTime.Value.ToString(TimeFormat)} Lunch"));
                }
                else
                {
                    foreach (var talk in talkSession.Talks)
                    {
                        GetPrintableTalk(scheduleList, talk);
                    }
                    scheduleList.Add(string.Format($"{track.NetworkingSession.StartTime.Value.ToString(TimeFormat)} Networking Event"));
                }
            }
        }

        private static void GetPrintableTalk(List<string> scheduleList, Talk talk)
        {
            var talkString = new StringBuilder();
            talkString.Append(talk.StartTime.Value.ToString(TimeFormat));
            talkString.Append(Space);
            talkString.Append(talk.Title);
            talkString.Append(Space);
            talkString.Append(talk.Duration.DurationPortionFromTitle);
            scheduleList.Add(talkString.ToString());
        }
    }
}
