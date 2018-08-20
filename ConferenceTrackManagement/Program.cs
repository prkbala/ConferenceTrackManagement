using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConferenceTrackManagement
{
    internal class Program
    {
        private static readonly List<string> SampleInput = new List<string>
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

        private static void Main(string[] args)
        {
            List<string> inputTalkList;

            try
            {
                inputTalkList = File.ReadAllLines(args[0]).ToList();
            }
            catch (Exception)
            {
                Console.WriteLine("An error occured with the actual input (please check the path and content of the input file), for now running the app with a sample input \n\n");
                inputTalkList = SampleInput;
            }

            var conferenceDate = new DateTime(2018, 8, 24);
            var conferencePlanner = new SimpleConferencePlanner(new TitleDurationSplitter());
            var conference = conferencePlanner.PlanAConference(conferenceDate, inputTalkList);
            var printableList = new SimplePrintFormatter().GetPrintablePlan(conference);
            foreach (var entry in printableList)
                Console.WriteLine(entry);

            Console.ReadKey();
        }
    }
}