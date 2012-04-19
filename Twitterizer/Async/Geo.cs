using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitterizer.Models;

namespace Twitterizer
{
    /// <summary>
    /// Users tweet from all over the world. These methods allow you to attach location data to tweets and discover tweets & locations.
    /// NOT YET IMPLEMENTED: GET/id:placeid, GET/search, GET/similar_places, GET/place
    /// </summary>
    public static class Geo
    {
        /// <summary>
        /// Retrieves a place based on the specified coordinates.
        /// </summary>
        /// <param name="latitude">The latitude.</param>
        /// <param name="longitude">The longitude.</param>
        /// <param name="options">The options. Leave null for defaults.</param>
        /// <returns>A collection of matched <see cref="Twitterizer.TwitterPlace"/> items.</returns>
        public static async Task<TwitterResponse<TwitterPlaceCollection>> ReverseGeocodeAsync(double latitude, double longitude, TwitterPlaceLookupOptions options = null)
        {
            return await Core.CommandPerformer.PerformAction(new Twitterizer.Commands.ReverseGeocodeCommand(latitude, longitude, options));
        }        
    }
}
