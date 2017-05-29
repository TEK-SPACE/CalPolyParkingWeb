using System;

namespace Parkix.Shared.Helpers
{
    /// <summary>
    /// Data helpers for conversion and manipulation of data
    /// </summary>
    public static class DateHelpers
    {

        /// <summary>
        /// Datetimes to epoch ms.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns></returns>
        public static long DatetimeToEpochMs(DateTime dateTime)
        {
            return (long)(dateTime - new DateTime(1970, 1, 1)).TotalMilliseconds;
        }

        /// <summary>
        /// The unix epoch date time.
        /// </summary>
        private static DateTime unixEpochDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);

        /// <summary>
        /// Converts a Unix Epoch timestamp (1970) to datetime.
        /// </summary>
        /// <param name="unixTimeStamp">The unix time stamp.</param>
        /// <returns></returns>
        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            var result = unixEpochDateTime.AddMilliseconds(unixTimeStamp).ToLocalTime();
            return result;
        }
    }
}
