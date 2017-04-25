using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Joe's PseudoLoggingService, because CF + .Net = A Disaster
/// Call one of the Log functions to log an event
/// Send EventLog straight to the browser if you want.
/// </summary>
namespace ParkingProcessing
{
    public class PseudoLoggingService
    {
        private IEnumerable<string> _events = new List<string>();
        private static readonly PseudoLoggingService Instance = new PseudoLoggingService();

        private PseudoLoggingService()
        {
        }

        /// <summary>
        /// Logs the string.
        /// </summary>
        /// <param name="eventString">The event string.</param>
        public static void Log(string eventString)
        {
            eventString = DateTime.Now + " : " + eventString;
            Instance._events = Instance._events.Append(eventString);
        }

        /// <summary>
        /// Logs the array of strings.
        /// </summary>
        /// <param name="eventStrings"></param>
        public static void Log(string[] eventStrings)
        {
            foreach (string s in eventStrings)
            {
                Log(s);
            }
        }

        /// <summary>
        /// Logs the dictionary.
        /// </summary>
        /// <param name="dictionary"></param>
        public static void Log(System.Collections.IDictionary dictionary)
        {
            foreach (object key in dictionary.Keys)
            {
                Log(key.ToString() + " : " + dictionary[key].ToString());
            }

        }

        /// <summary>
        /// Logs the exception.
        /// </summary>
        /// <param name="e"></param>
        public static void Log(Exception e)
        {
            Log("EXCEPTION: Message is " + e.Message + ", Source is " + e.Source);
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
