/* Geolocation Class Library
 * Author: Scott Schluer (scott.schluer@gmail.com)
 * May 29, 2012
 * https://github.com/scottschluer/Geolocation
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Twitterizer2.Streaming
{
    public class CoordinateBoundaries
    {
        private double _latitude;

        /// <summary>
        /// The origin point latitude in decimal notation
        /// </summary>
        public double Latitude
        {
            get { return _latitude; }

            set
            {
                _latitude = value;
                Calculate();
            }
        }

        private double _longitude;

        /// <summary>
        /// The origin point longitude in decimal notation
        /// </summary>
        public double Longitude
        {
            get { return _longitude; }
            set
            {
                _longitude = value;
                Calculate();
            }
        }

        private double _distance;

        /// <summary>
        /// The distance in statue miles from the origin point
        /// </summary>
        public double Distance
        {
            get { return _distance; }
            set
            {
                _distance = value;
                Calculate();
            }
        }

        /// <summary>
        /// The lower boundary latitude point in decimal notation
        /// </summary>
        public double MaxLatitude { get; private set; }

        /// <summary>
        /// The upper boundary latitude point in decimal notation
        /// </summary>
        public double MinLatitude { get; private set; }

        /// <summary>
        /// The right boundary longitude point in decimal notation
        /// </summary>
        public double MaxLongitude { get; private set; }

        /// <summary>
        /// The left boundary longitude point in decimal notation
        /// </summary>
        public double MinLongitude { get; private set; }

        /// <summary>
        /// Creates a new CoordinateBoundary object
        /// </summary>
        public CoordinateBoundaries()
        {
        }

        /// <summary>
        /// Creates a new CoordinateBoundary object
        /// </summary>
        /// <param name="latitude">The origin point latitude in decimal notation</param>
        /// <param name="longitude">The origin point longitude in decimal notation</param>
        /// <param name="distance">The distance from the origin point in statute miles</param>
        public CoordinateBoundaries(double latitude, double longitude, double distance)
        {
            if (!CoordinateValidator.Validate(latitude, longitude))
                throw new ArgumentException("Invalid coordinates supplied.");

            _latitude = latitude;
            _longitude = longitude;
            _distance = distance;

            Calculate();
        }

        private void Calculate()
        {
            if (!CoordinateValidator.Validate(Latitude, Longitude))
                throw new ArgumentException("Invalid coordinates supplied.");

            double latitudeConversionFactor = Distance / 69;
            double longitudeConversionFactor = Distance / 69 / Math.Abs(Math.Cos(Latitude.ToRadian()));

            MinLatitude = Latitude - latitudeConversionFactor;
            MaxLatitude = Latitude + latitudeConversionFactor;

            MinLongitude = Longitude - longitudeConversionFactor;
            MaxLongitude = Longitude + longitudeConversionFactor;

            // Adjust for passing over coordinate boundaries
            if (MinLatitude < -90) MinLatitude = 90 - (-90 - MinLatitude);
            if (MaxLatitude > 90) MaxLatitude = -90 + (MaxLatitude - 90);

            if (MinLongitude < -180) MinLongitude = 180 - (-180 - MinLongitude);
            if (MaxLongitude > 180) MaxLongitude = -180 + (MaxLongitude - 180);
        }
    }
}
