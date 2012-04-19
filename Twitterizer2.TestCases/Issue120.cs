using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System.IO;
using Twitterizer;
using Twitterizer.Core;

namespace Twitterizer2.TestCases
{
    [TestFixture]
    class Issue120
    {
        [Test]
        public static void Test()
        {
            byte[] responseBody = File.ReadAllBytes("Issue120.txt");

            TwitterUserCollection results = SerializationHelper<TwitterUserCollection>.Deserialize(responseBody, DeserializeWrapper);

            Assert.IsNotNull(results);
            Assert.IsNotEmpty(results);
        }

        /// <summary>
        /// This method is copied out of the TwitterUserCollection class.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static TwitterUserCollection DeserializeWrapper(JObject value)
        {
            if (value == null || value.SelectToken("users") == null)
                return null;

            TwitterUserCollection result = JsonConvert.DeserializeObject<TwitterUserCollection>(value.SelectToken("users").ToString());
            //result.NextCursor = value.SelectToken("next_cursor").Value<long>();
            //result.PreviousCursor = value.SelectToken("previous_cursor").Value<long>();

            return result;
        }
    }
}
