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
        private int[] _seriesData;
        private int _lastPoint;
        private DateTime _baseTimeStamp;

        /// <summary>
        /// The starting timestamp of the timeseries.
        /// </summary>
        public DateTime StartTimeStamp
        {
            get
            {
                return _baseTimeStamp;
            }
        }

        /// <summary>
        /// The ending timestamp of the timeseries.
        /// </summary>
        public DateTime EndTimeStamp
        {
            get
            {
                return _baseTimeStamp.AddDays(1);
            }
        }

        /// <summary>
        /// Generates a new daytimeseries starting at the base time stamp.
        /// </summary>
        /// <param name="basetimestamp"></param>
        public DayTimeSeries(DateTime basetimestamp)
        {
            _baseTimeStamp = basetimestamp.Date;
            _seriesData = new int[288];
            _lastPoint = 0;
        }

        /// <summary>
        /// Adds a data point to the timeseries.
        /// </summary>
        /// <param name="datapoint"></param>
        /// <param name="timestamp"></param>
        /// <param name="interpolate">Indicate whether we should fill in skipped time values.</param>
        /// <returns></returns>
        public void AddDataPoint(int datapoint, DateTime timestamp, bool interpolate)
        {
            var timeIndex = (int)(timestamp - _baseTimeStamp).TotalMinutes / 5;

            if (timeIndex > _lastPoint)
            {
                while (_lastPoint < 287)
                {
                    _seriesData[_lastPoint + 1] = _seriesData[_lastPoint];
                    _lastPoint++;
                }
                return;
            }
            else
            {
                _seriesData[timeIndex] = datapoint;
            }
            

            if (interpolate)
            {
                InterpolateDataPoints(timeIndex, datapoint);
            }

            _lastPoint = timeIndex;
        }

        /// <summary>
        /// Summarize the data based on the sample rate.
        /// </summary>
        /// <param name="samplerate"></param>
        /// <returns></returns>
        public int[] Summarize(TimeSpan samplerate)
        {
            if (samplerate > TimeSpan.FromDays(1) || samplerate < TimeSpan.FromMinutes(5))
            {
                return null;
            }

            var numberOfPoints = (int)(288 / (samplerate.TotalMinutes / 5));
            var pointsPerSummaryPoint = 288 / numberOfPoints;

            var result = new int[numberOfPoints];

            for (int i = 0; i < numberOfPoints; i++)
            {
                for (int j = 0; j < pointsPerSummaryPoint; j++)
                {
                    result[i] += _seriesData[i*pointsPerSummaryPoint + j];
                }

                result[i] /= pointsPerSummaryPoint;
            }

            return result;
        }

        /// <summary>
        /// Interpolates data for when data was not collected.
        /// Holds the previous state of the data.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="datapoint"></param>
        private void InterpolateDataPoints(int index, int datapoint)
        {
            var i = _lastPoint + 1;
            while (i < index)
            {
                _seriesData[i] = datapoint;
                i++;
            }
        }
    }
}
