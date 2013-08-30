/* Geolocation Class Library
 * Author: Scott Schluer (scott.schluer@gmail.com)
 * May 29, 2012
 * https://github.com/scottschluer/Geolocation
 */

namespace Twitterizer2.Streaming
{
    /// <summary>
    /// Utility class for coordinating the validators
    /// </summary>
    public static class CoordinateValidator
    {
        /// <summary>
        /// Validates the coordinate.
        /// </summary>
        /// <param name="latitude">The latitude.</param>
        /// <param name="longitude">The longitude.</param>
        /// <returns>True, if the coordinate is valid, false otherwise.</returns>
        public static bool Validate(double latitude, double longitude)
        {
            if (latitude < -90 || latitude > 90) return false;
            if (longitude < -180 || longitude > 180) return false;

            return true;
        }
    }
}
