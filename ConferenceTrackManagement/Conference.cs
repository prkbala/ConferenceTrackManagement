using System.Collections.Generic;

namespace ConferenceTrackManagement
{
    public class Conference
    {
        public List<Track> Tracks { get; private set; }

        public void AddATrack(Track track)
        {
            if (Tracks == null)
                Tracks = new List<Track>();

            Tracks.Add(track);
        }
    }
}