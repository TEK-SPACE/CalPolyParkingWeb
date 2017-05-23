using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parkix.Process.Entities.Parking
{
    /// <summary>
    /// Describes a parking lot in a one day interval.
    /// </summary>
    public class ParkingLotFrame
    {
        private DayTimeSeries _spotsTaken;

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
                return _spotsTaken.StartTimeStamp;
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
                return _spotsTaken.EndTimeStamp;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ParkingLotFrame"/> class.
        /// </summary>
        /// <param name="start">The start.</param>
        ParkingLotFrame(DateTime start)
        {
            _spotsTaken = new DayTimeSeries(start);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ParkingLotFrame"/> class.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="spotsTaken">The spots taken.</param>
        ParkingLotFrame(DateTime start, int spotsTaken)
        {
            _spotsTaken = new DayTimeSeries(start);
            _spotsTaken.AddDataPoint(spotsTaken, start, false);
        }

        /// <summary>
        /// Updates the frame with a snapshot.
        /// </summary>
        /// <param name="snapshot">The snapshot.</param>
        public void UpdateWithSnapshot(ParkingLotSnapshot snapshot)
        {
            _spotsTaken.AddDataPoint(snapshot.SpotsTaken, snapshot.Timestamp, true);
        }
    }
}
