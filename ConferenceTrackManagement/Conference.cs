using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceTrackManagement
{
    public class Conference
    {
        private IPrintFormatter _printFormatter;
        public List<Track> Tracks { get; private set; }

        public Conference(IPrintFormatter printFormatter)
        {
            _printFormatter = printFormatter;
        }

        public void AddATrack(Track track)
        {
            if (Tracks == null)
                Tracks = new List<Track>();

            Tracks.Add(track);
        }

        public List<string> GetPrintablePlan()
        {
           return _printFormatter.GetPrintablePlan(this);
        }
    }
}
