using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Twitterizer.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class PlaceAttributes
    {
        /// <summary>
        /// Gets or sets the address of the street.
        /// </summary>
        /// <value>The address of the street.</value>
        [JsonProperty(PropertyName = "street_address")]
        public string StreetAddress { get; set; }

        /// <summary>
        /// Gets or sets the locality.
        /// </summary>
        /// <value>The locality.</value>
        /// <remarks></remarks>
        [JsonProperty(PropertyName = "locality")]
        public string Locality { get; set; }

        /// <summary>
        /// Gets or sets the region.
        /// </summary>
        /// <value>The region.</value>
        /// <remarks></remarks>
        [JsonProperty(PropertyName = "region")]
        public string Region { get; set; }

        /// <summary>
        /// Gets or sets the iso3 country code.
        /// </summary>
        /// <value>The iso3 country code.</value>
        /// <remarks></remarks>
        [JsonProperty(PropertyName = "iso3")]
        public string Iso3CountryCode { get; set; }

        /// <summary>
        /// Gets or sets the postal code.
        /// </summary>
        /// <value>The postal code.</value>
        /// <remarks></remarks>
        [JsonProperty(PropertyName = "postal_code")]
        public string PostalCode { get; set; }

        /// <summary>
        /// Gets or sets the phone number in the preferred local format for the place, include long distance code.
        /// </summary>
        /// <value>The phone number.</value>
        /// <remarks></remarks>
        [JsonProperty(PropertyName = "phone")]
        public string Phone { get; set; }

        /// <summary>
        /// twitter screen-name, without @.
        /// </summary>
        /// <value>The phone number.</value>
        /// <remarks></remarks>
        [JsonProperty(PropertyName = "twitter")]
        public string TwitterScreenName { get; set; }

        /// <summary>
        /// Gets or sets the address of the data.
        /// </summary>
        /// <value>The address of the data.</value>
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets an ID or comma separated list of IDs representing the place in the applications place database.
        /// </summary>
        /// <value>The app ids.</value>
        [JsonProperty(PropertyName = "app:id")]
        public string AppIds { get; set; }
    }
}
