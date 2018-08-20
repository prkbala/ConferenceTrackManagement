using System.Collections.Generic;
using System.Text;

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
            if (conference?.Tracks == null)
                return null;

            var scheduleList = new List<string>();
            for (var index = 0; index < conference.Tracks.Count; index++)
            {
                scheduleList.Add($"Track {index + 1}:");
                var track = conference.Tracks[index];
                GeneratePrintableTrack(track, scheduleList);
            }
            return scheduleList;
        }

        private static void GeneratePrintableTrack(Track track, ICollection<string> scheduleList)
        {
            foreach (var talkSession in track.TalkSessions)
            {
                foreach (var talk in talkSession.Talks)
                    GetPrintableTalk(scheduleList, talk);

                scheduleList.Add(talkSession.SessionType == TalkSessionType.Morning
                    ? string.Format($"{track.LunchSession.StartTime?.ToString(TimeFormat)} Lunch")
                    : string.Format($"{track.NetworkingSession.StartTime?.ToString(TimeFormat)} Networking Event"));
            }
        }

        private static void GetPrintableTalk(ICollection<string> scheduleList, Talk talk)
        {
            var talkString = new StringBuilder();
            talkString.Append(talk.StartTime?.ToString(TimeFormat));
            talkString.Append(Space);
            talkString.Append(talk.Title);
            talkString.Append(Space);
            talkString.Append(talk.Duration.DurationPortionFromTitle);
            scheduleList.Add(talkString.ToString());
        }
    }
}