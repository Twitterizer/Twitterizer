
using Twitterizer2.Streaming;
namespace Twitterizer.Streaming
{
    public class Location
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Location"/> class.
        /// </summary>
        /// <remarks></remarks>
        public Location()
        {
            UseCoordinates = true;
        }
        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        /// <value>The latitude.</value>
        public double Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        /// <value>The longitude.</value>
        public double Longitude { get; set; }
        /// <summary>
        /// Gets or sets the min latitude.
        /// </summary>
        /// <value>The min latitude.</value>
        /// <remarks></remarks>
        public double MinLatitude { get; set; }
        /// <summary>
        /// Gets or sets the max latitude.
        /// </summary>
        /// <value>The max latitude.</value>
        /// <remarks></remarks>
        public double MaxLatitude { get; set; }
        /// <summary>
        /// Gets or sets the min longitude.
        /// </summary>
        /// <value>The min longitude.</value>
        /// <remarks></remarks>
        public double MinLongitude { get; set; }
        /// <summary>
        /// Gets or sets the max longitude.
        /// </summary>
        /// <value>The max longitude.</value>
        /// <remarks></remarks>
        public double MaxLongitude { get; set; }
        public bool UseCoordinates { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            var retVal = string.Format("{0},{1},{2},{3}", MinLongitude, MinLatitude, MaxLongitude, MaxLatitude);
            if (UseCoordinates)
            {
                var boundaries = new CoordinateBoundaries(Latitude, Longitude, 0.25);
                // -122.75,36.8,-121.75,37.8
                double minLatitude = boundaries.MinLatitude;
                double maxLatitude = boundaries.MaxLatitude;
                double minLongitude = boundaries.MinLongitude;
                double maxLongitude = boundaries.MaxLongitude;
                //var retVal = string.Format("{0},{1},{2},{3}", minLatitude, maxLongitude, maxLatitude, minLongitude);
                //var retVal = string.Format("{0},{1},{2},{3}", minLatitude, minLongitude, maxLatitude, maxLongitude);
                retVal = string.Format("{0},{1},{2},{3}", minLongitude, minLatitude, maxLongitude, maxLatitude);
            }

            return retVal;
        }
    }
}
