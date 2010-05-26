using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Twitterizer
{
    /// <summary>
    /// Provides a payload for optional parameters for the <see cref="Twitterizer.TwitterPlace.Lookup(double, double, TwitterPlaceLookupOptions)"/> method.
    /// </summary>
    public class TwitterPlaceLookupOptions : OptionalProperties
    {
        /// <summary>
        /// A hint on the "region" in which to search. If a number, then this is a radius in meters, but it can also take a string that is suffixed with ft to specify feet. If this is not passed in, then it is assumed to be 0m. If coming from a device, in practice, this value is whatever accuracy the device has measuring its location (whether it be coming from a GPS, WiFi triangulation, etc.).
        /// </summary>
        public string Accuracy { get; set; }

        /// <summary>
        /// The minimal granularity of data to return. If this is not passed in, then neighborhood is assumed. city can also be passed.
        /// </summary>
        public string Granularity { get; set; }

        /// <summary>
        /// A hint as to the number of results to return. This does not guarantee that the number of results returned will equal max_results, but instead informs how many "nearby" results to return. Ideally, only pass in the number of places you intend to display to the user here.
        /// </summary>
        public int? MaxResults { get; set; }
    }
}
