using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingProcessing.Entities.IeParking
{
    /// <summary>
    /// Predix Ie Parking Search Result Embedded.
    /// </summary>
    public class PredixIeParkingSearchResultEmbedded
    {
        /// <summary>
        /// Gets or sets the assets.
        /// </summary>
        /// <value>
        /// The assets.
        /// </value>
        public List<PredixIeParkingAsset> Assets { get; set; }
    }
}
