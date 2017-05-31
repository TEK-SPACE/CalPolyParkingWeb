using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parkix.Shared.Entities.Parking
{
    /// <summary>
    /// Describes a parking lot in a one day interval.
    /// </summary>
    public class ParkingLotFrame
    {
        public DayTimeSeries SpotsTaken;

        /// <summary>
        /// Gets the starting timestamp of the parking lot frame.
        /// </summary>
        /// <value>
        /// The start date time.
        /// </value>
        public DateTime StartDateStamp
        {
            get
            {
                return SpotsTaken.StartTimeStamp;
            }
        }

        /// <summary>
        /// Gets the ending timestamp of the parking lot frame.
        /// </summary>
        /// <value>
        /// The end date time.
        /// </value>
        public DateTime EndDateStamp
        {
            get
            {
                return SpotsTaken.EndTimeStamp;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ParkingLotFrame"/> class.
        /// </summary>
        /// <param name="start">The start.</param>
        public ParkingLotFrame(DateTime start)
        {
            SpotsTaken = new DayTimeSeries(start);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ParkingLotFrame"/> class.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="spotsTaken">The spots taken.</param>
        ParkingLotFrame(DateTime start, int spotsTaken)
        {
            SpotsTaken = new DayTimeSeries(start);
            SpotsTaken.AddDataPoint(spotsTaken, start);
        }

        /// <summary>
        /// Updates the frame with a snapshot.
        /// </summary>
        /// <param name="snapshot">The snapshot.</param>
        public string UpdateWithSnapshot(ParkingLotSnapshot snapshot)
        {
            return SpotsTaken.AddDataPoint(snapshot.SpotsTaken, snapshot.Timestamp);
        }
    }
}
