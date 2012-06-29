using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using Twitterizer;
using Twitterizer.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using System.Dynamic;

namespace Twitterizer2.MSTestCases
{
    [TestClass]
    public class Issue120
    {
        [TestMethod]
        public void Issue120Test()
        {
            String responseBody;

            using(var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Twitterizer2.MSTestCases.Issue120.txt"))
            using(var streamReader = new StreamReader(stream)) {
                responseBody = streamReader.ReadToEnd();
            }            

            TwitterUserCollection results = SerializationHelper<TwitterUserCollection>.Deserialize(responseBody, DeserializeWrapper);

            Assert.IsNotNull(results);
            Assert.AreNotEqual(0, results.Count);
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
