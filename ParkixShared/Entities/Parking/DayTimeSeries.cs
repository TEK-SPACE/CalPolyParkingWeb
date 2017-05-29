using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parkix.Shared.Entities.Parking
{
    /// <summary>
    /// Stores data in five minute increments for a given data type.
    /// </summary>
    public class DayTimeSeries
    {
        //<5 minute increment from midnight, value> 
        public IDictionary<int, int> SeriesData;

        /// <summary>
        /// The starting timestamp of the timeseries.
        /// </summary>
        public DateTime StartTimeStamp { get; set; }

        /// <summary>
        /// The ending timestamp of the timeseries.
        /// </summary>
        public DateTime EndTimeStamp
        {
            get
            {
                return StartTimeStamp.AddDays(1);
            }
        }

        /// <summary>
        /// Generates a new daytimeseries starting at the base time stamp.
        /// </summary>
        /// <param name="basetimestamp"></param>
        public DayTimeSeries(DateTime basetimestamp)
        {
            StartTimeStamp = basetimestamp.Date;
            SeriesData = new Dictionary<int, int>();
        }

        /// <summary>
        /// Adds a data point to the timeseries.
        /// </summary>
        /// <param name="datapoint"></param>
        /// <param name="timestamp"></param>
        /// <param name="interpolate">Indicate whether we should fill in skipped time values.</param>
        /// <returns></returns>
        public void AddDataPoint(int datapoint, DateTime timestamp)
        {
            var timeIndex = (int)(timestamp - StartTimeStamp).TotalMinutes / 5;

            if (timeIndex < 0 || timeIndex > 288)
            {
                throw new InvalidOperationException("datapoint is out of daterange for this series.");
            }

            SeriesData[timeIndex] = datapoint;
        }

        /// <summary>
        /// Summarize the data based on the sample rate.
        /// </summary>
        /// <param name="samplerate"></param>
        /// <returns></returns>
        public List<Tuple<int, int>> Summarize(TimeSpan samplerate, bool interpolate)
        {
            var samplecount = samplerate.TotalMinutes / 5;

            var result = new List<Tuple<int, int>>();
            int iterator = 0;
            int value;

            while (iterator < 288) //288 5-minute increments in a day
            {
                int samples = 0;
                int total = 0;
                int i;

                for (i = 0; i < samplecount && iterator < 288; i++, iterator++)
                {
                    if (SeriesData.TryGetValue(iterator, out value))
                    {
                        samples++;
                        total += value;
                    }
                }

                result.Add(new Tuple<int, int>(iterator - i, samples == 0 ? -1 : total / samples));
            }

            if (interpolate)
            {
                int index = 0;
                int interpolationValue = result.First((val) => val.Item2 != -1).Item2;
                while (index < result.Count)
                {
                    if (result[index].Item2 == -1)
                    {
                        result[index] = new Tuple<int, int>(result[index].Item1, interpolationValue);
                    }
                    else
                    {
                        interpolationValue = result[index].Item2;
                    }
                }
            }

            return result;
        }
    }
}
