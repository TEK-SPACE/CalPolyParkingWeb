using System;
using System.Collections.Generic;
using System.Linq;


namespace ParkingProcessing
{
    /// <summary>
    /// Makeshift logging service
    /// </summary>
    public class PseudoLoggingService
    {
        private IEnumerable<string> _events = new List<string>();
        private static readonly PseudoLoggingService Instance = new PseudoLoggingService();

        private PseudoLoggingService()
        {
        }

        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="eventString">The event string.</param>
        public static void Log(string source, string eventString)
        {
            if (Instance._events.Count() > 500)
            {
                Instance._events = new List<string>();
            }

            eventString = DateTime.Now.AddHours(-7) + " - " + source + " : " + eventString;
            Instance._events = Instance._events.Append(eventString);
        }

        /// <summary>
        /// Logs the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="eventStrings">The event strings.</param>
        public static void Log(string source, string[] eventStrings)
        {
            foreach (string s in eventStrings)
            {
                Log(source, s);
            }
        }

        /// <summary>
        /// Logs the specified dictionary.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="dictionary">The dictionary.</param>
        public static void Log(string source, System.Collections.IDictionary dictionary)
        {
            foreach (object key in dictionary.Keys)
            {
                Log(source, key.ToString() + " : " + dictionary[key].ToString());
            }

        }

        /// <summary>
        /// Logs the specified exception.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="e">The e.</param>
        public static void Log(string source, Exception e)
        {
            Log(source, "EXCEPTION: Message is " + e.Message + ", Source is " + e.Source);
        }

        /// <summary>
        /// Gets the event log.
        /// </summary>
        /// <value>
        /// The event log.
        /// </value>
        public static IEnumerable<string> EventLog => Instance._events;

        /// <summary>
        /// Gets the event log string.
        /// </summary>
        /// <value>
        /// The event log string.
        /// </value>
        public static string EventLogString
        {
            get
            {
                var output = "Event Log:\n";
                foreach (string s in Instance._events)
                {
                    output += s + "\n";
                }

                return output;
            }
        }
    }
}
