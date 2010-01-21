/*
 * This file is part of the Twitterizer library <http://code.google.com/p/twitterizer/>
 *
 * Copyright (c) 2010, Patrick "Ricky" Smith <ricky@digitally-born.com>
 * All rights reserved.
 * 
 * Redistribution and use in source and binary forms, with or without modification, are 
 * permitted provided that the following conditions are met:
 *
 * - Redistributions of source code must retain the above copyright notice, this list 
 *   of conditions and the following disclaimer.
 * - Redistributions in binary form must reproduce the above copyright notice, this list 
 *   of conditions and the following disclaimer in the documentation and/or other 
 *   materials provided with the distribution.
 * - Neither the name of the Twitterizer nor the names of its contributors may be 
 *   used to endorse or promote products derived from this software without specific 
 *   prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND 
 * ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
 * IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, 
 * INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT 
 * NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR 
 * PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
 * WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
 * ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE 
 * POSSIBILITY OF SUCH DAMAGE.
 */
namespace Twitterizer.Framework
{
    using System;
    using System.Drawing;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Xml;

    internal class TwitterRequest
    {
        private string proxyUri = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="TwitterRequest"/> class.
        /// </summary>
        /// <param name="ProxyUri">The proxy URI.</param>
        public TwitterRequest(string proxyUri)
        {
            this.proxyUri = proxyUri;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TwitterRequest"/> class.
        /// </summary>
        public TwitterRequest()
        {
        }

        /// <summary>
        /// Performs the web request.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public TwitterRequestData PerformWebRequest(TwitterRequestData data)
        {
            this.PerformWebRequest(data, "POST");

            return data;
        }

        /// <summary>
        /// Performs the web request.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="method">The method.</param>
        /// <returns></returns>
        public TwitterRequestData PerformWebRequest(TwitterRequestData data, string method)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(data.ActionUri);

            // Check if a proxy address was given, if so, we need to parse it and give it to the HttpWebRequest object.
            if (!string.IsNullOrEmpty(this.proxyUri))
            {
                UriBuilder proxyUriBuilder = new UriBuilder(this.proxyUri);
                request.Proxy = new WebProxy(proxyUriBuilder.Host, proxyUriBuilder.Port);

                // Add the proxy credentials if they are supplied.
                if (!string.IsNullOrEmpty(proxyUriBuilder.UserName))
                {
                    request.Proxy.Credentials = new NetworkCredential(proxyUriBuilder.UserName, proxyUriBuilder.Password);
                }
            }

            request.Method = method;

            // Some limitations
            request.MaximumAutomaticRedirections = 4;
            request.MaximumResponseHeadersLength = 4;
            request.ContentLength = 0;

            // Set our credentials
            request.Credentials = new NetworkCredential(data.UserName, data.Password);

            HttpWebResponse response = null;

            // Get the response
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException wex)
            {
                HandleWebException(data, wex);

                if (System.Configuration.ConfigurationManager.AppSettings["Twitterizer.EnableRequestHistory"] == "true")
                {
                    TwitterRequestHistory.History.Enqueue(data);
                }

                // If it gets this far without throwing an exception, 
                // we should return the Data object as it is.
                return data;
            }
            catch (Exception ex)
            {
                if (System.Configuration.ConfigurationManager.AppSettings["Twitterizer.EnableRequestHistory"] == "true")
                {
                    TwitterRequestHistory.History.Enqueue(data);
                }

                throw new TwitterizerException(ex.Message, data, ex);
            }

            try
            {
                // Get information about out usage rate
                if (!string.IsNullOrEmpty(response.Headers.Get("X-RateLimit-Limit")))
                {
                    data.RateLimit = int.Parse(response.Headers.Get("X-RateLimit-Limit"));
                }

                if (!string.IsNullOrEmpty(response.Headers.Get("X-RateLimit-Remaining")))
                {
                    data.RateLimitRemaining = int.Parse(response.Headers.Get("X-RateLimit-Remaining"));
                }

                // The date string is in Unix (aka epoch) time, which is the number of seconds since January 1st 1970 00:00
                if (!string.IsNullOrEmpty(response.Headers.Get("X-RateLimit-Reset")))
                {
                    data.RateLimitReset = (new DateTime(1970, 1, 1, 0, 0, 0, 0)).AddSeconds(double.Parse(response.Headers.Get("X-RateLimit-Reset")));
                }

                // Get the stream associated with the response.
                using (Stream receiveStream = response.GetResponseStream())
                {
                    using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                    {
                        // Pipes the stream to a higher level stream reader with the required encoding format. 
                        data.Response = readStream.ReadToEnd();

                        readStream.Close();
                    }
                }

                data = ParseResponseData(data);
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }

                if (System.Configuration.ConfigurationManager.AppSettings["Twitterizer.EnableRequestHistory"] == "true")
                {
                    TwitterRequestHistory.History.Enqueue(data);
                }
            }

            return data;
        }

        /// <summary>
        /// Handles a web exception.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="wex">The WebException.</param>
        private static void HandleWebException(TwitterRequestData data, WebException wex)
        {
            // If this was a 'real' exception (connection error, etc) throw the exception.
            if (wex.Status != WebExceptionStatus.ProtocolError || wex.Response.ContentLength == 0)
            {
                throw new TwitterizerException(wex.Message, data, wex);
            }

            HttpWebResponse errorResponse = (HttpWebResponse)wex.Response;

            // If we have any content in the response, read it into the request data object.
            if (errorResponse.ContentLength > 0)
            {
                StreamReader readStream = new StreamReader(errorResponse.GetResponseStream(), Encoding.UTF8);
                data.Response = readStream.ReadToEnd();
            }

            data.ResponseException = wex;

            // Determine what the protocol error was and throw the exception accordingly.
            switch (errorResponse.StatusCode)
            {
                case HttpStatusCode.NotModified:
                case HttpStatusCode.NotFound:
                    // There was no data to return
                    break;

                case HttpStatusCode.BadRequest:
                case HttpStatusCode.Forbidden:
                    // There is an error message returned as the response. We should get it and return it in the exception.
                    throw new TwitterizerException(TwitterizerException.ParseErrorMessage(data.Response), data, wex);
                case HttpStatusCode.Unauthorized:
                    throw new TwitterizerException("Authorization Failed", data);

                case HttpStatusCode.BadGateway:
                case HttpStatusCode.InternalServerError:
                    throw new TwitterizerException("Twitter is currently unavailable.", data);

                case HttpStatusCode.ServiceUnavailable:
                    throw new TwitterizerException("Twitter is overloaded or you are being rate limited.", data);

                default:
                    throw new TwitterizerException(wex.Message, data, wex);
            }
        }

        /// <summary>
        /// Propigates the rate limit details into all data objects.
        /// </summary>
        /// <param name="data">The data.</param>
        private static void PropigateRateLimitDetails(TwitterRequestData data)
        {
            if (data.RateLimit == null && data.RateLimitRemaining == null && data.RateLimitReset == null)
            {
                return;
            }

            if (data.Statuses != null)
            {
                data.Statuses.RateLimit = data.RateLimit;
                data.Statuses.RateLimitRemaining = data.RateLimitRemaining;
                data.Statuses.RateLimitReset = data.RateLimitReset;

                foreach (TwitterStatus item in data.Statuses)
                {
                    item.RateLimit = data.RateLimit;
                    item.RateLimitRemaining = data.RateLimitRemaining;
                    item.RateLimitReset = data.RateLimitReset;
                }
            }

            if (data.Users != null)
            {
                data.Users.RateLimit = data.RateLimit;
                data.Users.RateLimitRemaining = data.RateLimitRemaining;
                data.Users.RateLimitReset = data.RateLimitReset;

                foreach (TwitterUser item in data.Users)
                {
                    item.RateLimit = data.RateLimit;
                    item.RateLimitRemaining = data.RateLimitRemaining;
                    item.RateLimitReset = data.RateLimitReset;
                }
            }
        }

        /// <summary>
        /// Parses the response data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        private static TwitterRequestData ParseResponseData(TwitterRequestData data)
        {
            if (data == null || data.Response == string.Empty)
            {
                return null;
            }

            XmlDocument resultXmlDocument = new XmlDocument();
            resultXmlDocument.LoadXml(data.Response);

            if (resultXmlDocument.DocumentElement != null)
            {
                switch (resultXmlDocument.DocumentElement.Name.ToLower())
                {
                    case "status":
                        data.Statuses = new TwitterStatusCollection();
                        data.Statuses.Add(ParseStatusNode(resultXmlDocument.DocumentElement));
                        break;
                    case "statuses":
                        data.Statuses = ParseStatuses(resultXmlDocument.DocumentElement);
                        break;
                    case "users":
                        data.Users = ParseUsers(resultXmlDocument.DocumentElement);
                        break;
                    case "users_list":
                        data.Users = ParseUsers(resultXmlDocument.DocumentElement["users"]);
                        data.Users.NextCursor = long.Parse(resultXmlDocument.DocumentElement["next_cursor"].InnerText);
                        data.Users.PreviousCursor = long.Parse(resultXmlDocument.DocumentElement["previous_cursor"].InnerText);
                        break;
                    case "user":
                        data.Users = new TwitterUserCollection();
                        data.Users.Add(ParseUserNode(resultXmlDocument.DocumentElement));
                        break;
                    case "direct_message":
                        data.Statuses = new TwitterStatusCollection();
                        data.Statuses.Add(ParseDirectMessageNode(resultXmlDocument.DocumentElement));
                        break;
                    case "direct-messages":
                        data.Statuses = ParseDirectMessages(resultXmlDocument.DocumentElement);
                        break;
                    case "nilclasses":
                    case "nil-classes":
                        // do nothing, this seems to be a null response i.e. no messages since
                        break;
                    case "error":
                        throw new Exception("Error response from Twitter: " + resultXmlDocument.DocumentElement.InnerText);
                    default:
                        throw new Exception("Invalid response from Twitter");
                }
            }

            // Copy our rate limit values to the properties that are actually returned
            PropigateRateLimitDetails(data);

            return data;
        }

        #region Parse Statuses
        /// <summary>
        /// Parses multiple status nodes and returns a collection of status objects.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns></returns>
        private static TwitterStatusCollection ParseStatuses(XmlElement element)
        {
            TwitterStatusCollection collection = new TwitterStatusCollection();
            foreach (XmlElement child in element.GetElementsByTagName("status"))
            {
                collection.Add(ParseStatusNode(child));
            }

            // Get the cursor values
            int nextCursor;
            if (element["next_cursor"] != null && int.TryParse(element["next_cursor"].InnerText, out nextCursor))
            {
                collection.NextCursor = nextCursor;
            }

            int prevCursor;
            if (element["prev_cursor"] != null && int.TryParse(element["prev_cursor"].InnerText, out prevCursor))
            {
                collection.PreviousCursor = prevCursor;
            }

            return collection;
        }

        /// <summary>
        /// Parses a single status node and returns a status object.
        /// </summary>
        /// <param name="Element">The element.</param>
        /// <returns></returns>
        private static TwitterStatus ParseStatusNode(XmlNode element)
        {
            if (element == null)
            {
                return null;
            }

            TwitterStatus status = new TwitterStatus()
            {
                IsDirectMessage = false,
                ID = Int64.Parse(element["id"].InnerText),
                Created = ParseDateString(element["created_at"].InnerText),
                Text = element["text"].InnerText,
                Source = element["source"].InnerText,
                IsTruncated = bool.Parse(element["truncated"].InnerText),
                TwitterUser = ParseUserNode(element["user"])
            };

            if (element["in_reply_to_status_id"].InnerText != string.Empty)
            {
                status.InReplyToStatusID = Int64.Parse(element["in_reply_to_status_id"].InnerText);
            }

            if (element["in_reply_to_user_id"].InnerText != string.Empty)
            {
                status.InReplyToUserID = int.Parse(element["in_reply_to_user_id"].InnerText);
            }

            bool isFavorited;
            bool.TryParse(element["favorited"].InnerText, out isFavorited);
            status.IsFavorited = isFavorited;

            if (element["sender"] != null)
            {
                status.TwitterUser = ParseUserNode(element["sender"]);
            }

            if (element["recipient"] != null)
            {
                status.Recipient = ParseUserNode(element["recipient"]);
            }

            return status;
        }
        #endregion

        #region Parse DirectMessages
        /// <summary>
        /// Parses multiple direct messages and returns a collection of statuses.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns></returns>
        private static TwitterStatusCollection ParseDirectMessages(XmlElement element)
        {
            TwitterStatusCollection collection = new TwitterStatusCollection();
            foreach (XmlElement child in element.GetElementsByTagName("direct_message"))
            {
                collection.Add(ParseDirectMessageNode(child));
            }

            return collection;
        }

        /// <summary>
        /// Parses a single direct message node and returns a status object.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns></returns>
        private static TwitterStatus ParseDirectMessageNode(XmlNode element)
        {
            if (element == null)
            {
                return null;
            }

            TwitterStatus status = new TwitterStatus()
            {
                IsDirectMessage = true,
                ID = Int64.Parse(element["id"].InnerText),
                Created = ParseDateString(element["created_at"].InnerText),
                Text = element["text"].InnerText,
                TwitterUser = ParseUserNode(element["sender"]),
                RecipientID = int.Parse(element["recipient_id"].InnerText),
                Recipient = ParseUserNode(element["recipient"])
            };

            if (element["favorited"] != null && (element["in_reply_to_status_id"].InnerText != string.Empty))
            {
                status.IsFavorited = bool.Parse(element["favorited"].InnerText);
            }

            return status;
        }
        #endregion

        #region Parse Users
        /// <summary>
        /// Parses multiple users nodes and returns a collection of user objects.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns></returns>
        private static TwitterUserCollection ParseUsers(XmlElement element)
        {
            if (element == null)
            {
                return null;
            }

            TwitterUserCollection collection = new TwitterUserCollection();
            foreach (XmlElement child in element.GetElementsByTagName("user"))
            {
                collection.Add(ParseUserNode(child));
            }

            // Get the cursor values
            int nextCursor;
            if (element["next_cursor"] != null && int.TryParse(element["next_cursor"].InnerText, out nextCursor))
            {
                collection.NextCursor = nextCursor;
            }

            int prevCursor;
            if (element["prev_cursor"] != null && int.TryParse(element["prev_cursor"].InnerText, out prevCursor))
            {
                collection.PreviousCursor = prevCursor;
            }

            return collection;
        }

        /// <summary>
        /// Parses a single user node and returns a user object.
        /// </summary>
        /// <param name="Element">The element.</param>
        /// <returns></returns>
        private static TwitterUser ParseUserNode(XmlNode element)
        {
            if (element == null)
            {
                return null;
            }

            TwitterUser user = new TwitterUser()
            {
                ID = int.Parse(element["id"].InnerText),
                UserName = element["name"].InnerText,
                ScreenName = element["screen_name"].InnerText,
                Location = element["location"].InnerText,
                Uri = element["url"].InnerText,
                Description = element["description"].InnerText,
                CreatedAt = ParseDateString(element["created_at"].InnerText),
                IsVerified = bool.Parse(element["verified"].InnerText),
                IsProtected = bool.Parse(element["protected"].InnerText),

                // Profile information
                ProfileImageUri = element["profile_image_url"].InnerText,
                ProfileUri = element["url"].InnerText,
                ProfileBackgroundColor = ColorTranslator.FromHtml(
                    String.Concat("#", element["profile_background_color"].InnerText)),
                ProfileTextColor = ColorTranslator.FromHtml(
                            String.Concat("#", element["profile_text_color"].InnerText)),
                ProfileLinkColor = ColorTranslator.FromHtml(
                            String.Concat("#", element["profile_link_color"].InnerText)),
                ProfileSidebarFillColor = ColorTranslator.FromHtml(
                            String.Concat("#", element["profile_sidebar_fill_color"].InnerText)),
                ProfileSidebarBorderColor = ColorTranslator.FromHtml(
                            String.Concat("#", element["profile_sidebar_border_color"].InnerText)),
                ProfileBackgroundImageUri = element["profile_background_image_url"].InnerText,
                ProfileBackgroundTile = bool.Parse(element["profile_background_tile"].InnerText)
            };

            int utcOffset;
            if (int.TryParse(element["utc_offset"].InnerText, out utcOffset))
            {
                user.UTCOffset = utcOffset;
            }

            user.TimeZone = element["time_zone"].InnerText;

            if (!string.IsNullOrEmpty(element["notifications"].InnerText))
            {
                user.Notifications = bool.Parse(element["notifications"].InnerText);
            }

            if (!string.IsNullOrEmpty(element["following"].InnerText))
            {
                user.Following = bool.Parse(element["following"].InnerText);
            }

            // Get the number of followers
            if (element["followers_count"] != null)
            {
                user.NumberOfFollowers = int.Parse(element["followers_count"].InnerText);
            }
            else
            {
                user.NumberOfFollowers = -1;
            }

            // Get the number of friends
            if (element["friends_count"] != null)
            {
                user.NumberOfFriends = int.Parse(element["friends_count"].InnerText);
            }
            else
            {
                user.NumberOfFriends = -1;
            }

            // Get the number of statuses
            if (element["statuses_count"] != null)
            {
                user.NumberOfStatuses = int.Parse(element["statuses_count"].InnerText);
            }
            else
            {
                user.NumberOfStatuses = -1;
            }

            // If there is a status, parse it
            if (element["status"] != null)
            {
                user.Status = ParseStatusNode(element["status"]);
            }

            return user;
        }
        #endregion

        /// <summary>
        /// Returns the value of the element within the provided XmlNode, if the element exists. Returns String.Empty if not.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="elementName">Name of the element.</param>
        /// <returns></returns>
        private static string ElementValueIfExists(XmlNode node, string elementName)
        {
            if (!node.HasChildNodes || node[elementName] == null)
            {
                return string.Empty;
            }

            return node[elementName].Value;
        }

        /// <summary>
        /// Parses a date string into a strongly typed DateTime object.
        /// </summary>
        /// <param name="DateString">The date string.</param>
        /// <returns></returns>
        /// <remarks>Format example: Wed Apr 08 20:30:04 +0000 2009</remarks>
        private static DateTime ParseDateString(string dateString)
        {
            Regex re = new Regex(@"(?<DayName>[^ ]+) (?<MonthName>[^ ]+) (?<Day>[^ ]{1,2}) (?<Hour>[0-9]{1,2}):(?<Minute>[0-9]{1,2}):(?<Second>[0-9]{1,2}) (?<TimeZone>[+-][0-9]{4}) (?<Year>[0-9]{4})");
            Match createdAtMatch = re.Match(dateString);
            DateTime parsedDate = DateTime.Parse(
            string.Format(
            "{0} {1} {2} {3}:{4}:{5}",
            createdAtMatch.Groups["MonthName"].Value,
            createdAtMatch.Groups["Day"].Value,
            createdAtMatch.Groups["Year"].Value,
            createdAtMatch.Groups["Hour"].Value,
            createdAtMatch.Groups["Minute"].Value,
            createdAtMatch.Groups["Second"].Value));

            return parsedDate;
        }
    }
}
