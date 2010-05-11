//-----------------------------------------------------------------------
// <copyright file="ReverseGeocodeCommand.cs" company="Patrick 'Ricky' Smith">
//  This file is part of the Twitterizer library (http://code.google.com/p/twitterizer/)
// 
//  Copyright (c) 2010, Patrick "Ricky" Smith (ricky@digitally-born.com)
//  All rights reserved.
//  
//  Redistribution and use in source and binary forms, with or without modification, are 
//  permitted provided that the following conditions are met:
// 
//  - Redistributions of source code must retain the above copyright notice, this list 
//    of conditions and the following disclaimer.
//  - Redistributions in binary form must reproduce the above copyright notice, this list 
//    of conditions and the following disclaimer in the documentation and/or other 
//    materials provided with the distribution.
//  - Neither the name of the Twitterizer nor the names of its contributors may be 
//    used to endorse or promote products derived from this software without specific 
//    prior written permission.
// 
//  THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND 
//  ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
//  WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
//  IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, 
//  INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT 
//  NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR 
//  PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
//  WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
//  ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE 
//  POSSIBILITY OF SUCH DAMAGE.
// </copyright>
// <author>Ricky Smith</author>
// <summary>The reverse geocode command class.</summary>
//-----------------------------------------------------------------------

namespace Twitterizer.Commands
{
    using Twitterizer.Core;
    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System.Collections.ObjectModel;
    using System;

    /// <summary>
    /// The reverse geocode command class. Performs a reverse geocode lookup.
    /// </summary>
    internal class ReverseGeocodeCommand : TwitterCommand<TwitterPlaceCollection>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReverseGeocodeCommand"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public ReverseGeocodeCommand(double latitude, double longitude, OptionalProperties options)
            : base("GET", "geo/reverse_geocode.json", null, options)
        {
            this.Latitude = latitude;
            this.Longitude = longitude;
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
        /// Initializes the command.
        /// </summary>
        public override void Init()
        {
            this.DeserializationHandler = Deserialize;
            this.RequestParameters.Add("lat", this.Latitude.ToString(CultureInfo.InvariantCulture));
            this.RequestParameters.Add("long", this.Longitude.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Validates this instance.
        /// </summary>
        public override void Validate()
        {
            this.IsValid = this.Latitude != 0 && this.Longitude != 0;
        }

        private static TwitterPlaceCollection Deserialize(JObject value)
        {
            TwitterPlaceCollection result = new TwitterPlaceCollection();

            foreach (JObject item in (JArray)value.SelectToken("result.places"))
            {
                TwitterPlace place = JsonConvert.DeserializeObject<TwitterPlace>(item.ToString());
                JToken boundingBox = item.SelectToken("bounding_box");

                if (boundingBox == null || boundingBox.Type == JTokenType.Null)
                    continue;

                place.BoundingBox = new TwitterBoundingBox();
                place.BoundingBox.Type = (string)((JValue)boundingBox.SelectToken("type")).Value;
                place.BoundingBox.Coordinates = new double[0, 2];

                foreach (JToken coordinate in (JArray)boundingBox.SelectToken("coordinates").First)
                {
                    int rowNumber = place.BoundingBox.Coordinates.GetUpperBound(0) + 1;

                    double[,] coordinateArray = new double[rowNumber + 1, 2];

                    Array.Copy(place.BoundingBox.Coordinates, coordinateArray, place.BoundingBox.Coordinates.Length);

                    coordinateArray[rowNumber, 0] = (double)((JValue)coordinate.First).Value;
                    coordinateArray[rowNumber, 1] = (double)((JValue)coordinate.Last).Value;

                    place.BoundingBox.Coordinates = coordinateArray;
                }

                result.Add(place);
            }

            return result;
        }
    }
}
