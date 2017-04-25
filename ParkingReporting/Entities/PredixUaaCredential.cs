﻿using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ParkingReporting.Entities
{
    public class PredixUaaCredential
    {
        /// <summary>
        /// Gets or sets the issuer identifier.
        /// </summary>
        /// <value>
        /// The issuer identifier.
        /// </value>
        public string IssuerId { get; set; }

        /// <summary>
        /// Gets or sets the subdomain.
        /// </summary>
        /// <value>
        /// The subdomain.
        /// </value>
        public string Subdomain { get; set; }

        /// <summary>
        /// Gets or sets the dashboard URL.
        /// </summary>
        /// <value>
        /// The dashboard URL.
        /// </value>
        public string DashboardUrl { get; set; }

        /// <summary>
        /// Gets or sets the zone.
        /// </summary>
        /// <value>
        /// The zone.
        /// </value>
        public PredixUaaZone Zone { get; set; }

        
        public string Uri { get; set; }
    }
}
