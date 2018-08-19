using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
