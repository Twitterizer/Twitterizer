/*
 * This file is part of the Twitterizer library <http://code.google.com/p/twitterizer/>
 *
 * Copyright (c) 2008, Patrick "Ricky" Smith <ricky@digitally-born.com>
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
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Net;
    using System.Xml;
    using System.IO;

	internal class TwitterRequest
	{
        private string proxyUri = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="TwitterRequest"/> class.
        /// </summary>
        /// <param name="ProxyUri">The proxy URI.</param>
        public TwitterRequest(string ProxyUri)
        {
            proxyUri = ProxyUri;
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
        /// <param name="Data">The data.</param>
        /// <returns></returns>
		public TwitterRequestData PerformWebRequest(TwitterRequestData Data)
		{
			PerformWebRequest(Data, "POST");

			return Data;

		}

        /// <summary>
        /// Performs the web request.
        /// </summary>
        /// <param name="Data">The data.</param>
        /// <param name="HTTPMethod">The HTTP method.</param>
        /// <returns></returns>
		public TwitterRequestData PerformWebRequest(TwitterRequestData Data, string HTTPMethod)
		{
			HttpWebRequest Request = (HttpWebRequest)WebRequest.Create(Data.ActionUri);

            // Check if a proxy address was given, if so, we need to parse it and give it to the HttpWebRequest object.
            if (!string.IsNullOrEmpty(proxyUri))
            {
                UriBuilder proxyUriBuilder = new UriBuilder(proxyUri);
                Request.Proxy = new WebProxy(proxyUriBuilder.Host, proxyUriBuilder.Port);

                // Add the proxy credentials if they are supplied.
                if (!string.IsNullOrEmpty(proxyUriBuilder.UserName))
                    Request.Proxy.Credentials = new NetworkCredential(proxyUriBuilder.UserName, proxyUriBuilder.Password);
            }
            
			Request.Method = HTTPMethod;

			StreamReader readStream;

			// Some limitations
			Request.MaximumAutomaticRedirections = 4;
			Request.MaximumResponseHeadersLength = 4;
			Request.ContentLength = 0;

			// Set our credentials
			Request.Credentials = new NetworkCredential(Data.UserName, Data.Password);

			HttpWebResponse Response = null;

			// Get the response
            try
            {
                Response = (HttpWebResponse)Request.GetResponse();
            }
            catch (WebException wex)
            {
                HandleWebException(Data, wex);

                if (System.Configuration.ConfigurationManager.AppSettings["Twitterizer.EnableRequestHistory"] == "true")
                    TwitterRequestHistory.History.Enqueue(Data);

                // If it gets this far without throwing an exception, 
                // we should return the Data object as it is.
                return Data;
            }
            catch (Exception ex)
            {
                if (System.Configuration.ConfigurationManager.AppSettings["Twitterizer.EnableRequestHistory"] == "true")
                    TwitterRequestHistory.History.Enqueue(Data);

                throw new TwitterizerException(ex.Message, Data, ex);
            }

            try
            {
                // Get information about out usage rate
                if (!string.IsNullOrEmpty(Response.Headers.Get("X-RateLimit-Limit")))
                    Data.RateLimit = int.Parse(Response.Headers.Get("X-RateLimit-Limit"));

                if (!string.IsNullOrEmpty(Response.Headers.Get("X-RateLimit-Remaining")))
                    Data.RateLimitRemaining = int.Parse(Response.Headers.Get("X-RateLimit-Remaining"));

                // The date string is in Unix (aka epoch) time, which is the number of seconds since January 1st 1970 00:00
                if (!string.IsNullOrEmpty(Response.Headers.Get("X-RateLimit-Reset")))
                    Data.RateLimitReset = (new DateTime(1970, 1, 1, 0, 0, 0, 0)).AddSeconds(double.Parse(Response.Headers.Get("X-RateLimit-Reset")));

                // Get the stream associated with the response.
                using (Stream receiveStream = Response.GetResponseStream())
                {
                    // Pipes the stream to a higher level stream reader with the required encoding format. 
                    readStream = new StreamReader(receiveStream, Encoding.UTF8);

                    Data.Response = readStream.ReadToEnd();

                    readStream.Close();
                }
                
                Data = ParseResponseData(Data);
            }
            finally
            {
                if (Response != null)
                    Response.Close();

                if (System.Configuration.ConfigurationManager.AppSettings["Twitterizer.EnableRequestHistory"] == "true")
                    TwitterRequestHistory.History.Enqueue(Data);
            }
            
			return Data;
		}

        /// <summary>
        /// Handles a web exception.
        /// </summary>
        /// <param name="Data">The RequestData object.</param>
        /// <param name="wex">The WebException.</param>
        /// <returns></returns>
        private static void HandleWebException(TwitterRequestData Data, WebException wex)
        {
            // If this was a 'real' exception (connection error, etc) throw the exception.
            if (wex.Status != WebExceptionStatus.ProtocolError || wex.Response.ContentLength == 0)
                throw new TwitterizerException(wex.Message, Data, wex);

            HttpWebResponse ErrorResponse = (HttpWebResponse)wex.Response;
            
            // If we have any content in the response, read it into the request data object.
            if (ErrorResponse.ContentLength > 0)
            {
                StreamReader readStream = new StreamReader(ErrorResponse.GetResponseStream(), Encoding.UTF8);
                Data.Response = readStream.ReadToEnd();
            }

            Data.ResponseException = wex;

            // Determine what the protocol error was and throw the exception accordingly.
            switch (ErrorResponse.StatusCode)
            {
                case HttpStatusCode.NotModified:
                case HttpStatusCode.NotFound:
                    // There was no data to return
                    break;

                case HttpStatusCode.BadRequest:
                case HttpStatusCode.Forbidden:
                    // There is an error message returned as the response. We should get it and return it in the exception.
                    throw new TwitterizerException(TwitterizerException.ParseErrorMessage(Data.Response), Data, wex);
                case HttpStatusCode.Unauthorized:
                    throw new TwitterizerException("Authorization Failed", Data);

                case HttpStatusCode.BadGateway:
                case HttpStatusCode.InternalServerError:
                    throw new TwitterizerException("Twitter is currently unavailable.", Data);

                case HttpStatusCode.ServiceUnavailable:
                    throw new TwitterizerException("Twitter is overloaded or you are being rate limited.", Data);

                default:
                    throw new TwitterizerException(wex.Message, Data, wex);
            }
        }

        /// <summary>
        /// Parses the response data.
        /// </summary>
        /// <param name="Data">The data.</param>
        /// <returns></returns>
        private TwitterRequestData ParseResponseData(TwitterRequestData Data)
        {
            if (Data == null || Data.Response == string.Empty)
                return null;

            XmlDocument ResultXmlDocument = new XmlDocument();
            ResultXmlDocument.LoadXml(Data.Response);

            if (ResultXmlDocument.DocumentElement != null)
                switch (ResultXmlDocument.DocumentElement.Name.ToLower())
                {
                    case "status":
                        Data.Statuses = new TwitterStatusCollection();
                        Data.Statuses.Add(ParseStatusNode(ResultXmlDocument.DocumentElement));
                        break;
                    case "statuses":
                        Data.Statuses = ParseStatuses(ResultXmlDocument.DocumentElement);
                        break;
                    case "users":
                        Data.Users = ParseUsers(ResultXmlDocument.DocumentElement);
                        break;
                    case "user":
                        Data.Users = new TwitterUserCollection();
                        Data.Users.Add(ParseUserNode(ResultXmlDocument.DocumentElement));
                        break;
                    case "direct_message":
                        Data.Statuses = new TwitterStatusCollection();
                        Data.Statuses.Add(ParseDirectMessageNode(ResultXmlDocument.DocumentElement));
                        break;
                    case "direct-messages":
                        Data.Statuses = ParseDirectMessages(ResultXmlDocument.DocumentElement);
                        break;
                    case "nilclasses":
                    case "nil-classes":
                        // do nothing, this seems to be a null response i.e. no messages since
                        break;
                    case "error":
                        throw new Exception("Error response from Twitter: " + ResultXmlDocument.DocumentElement.InnerText);
                    default:
                        throw new Exception("Invalid response from Twitter");
                }

            // Copy our rate limit values to the properties that are actually returned
            PropigateRateLimitDetails(Data);

            return Data;
        }

        /// <summary>
        /// Propigates the rate limit details into all data objects.
        /// </summary>
        /// <param name="Data">The data.</param>
        private static void PropigateRateLimitDetails(TwitterRequestData Data)
        {
            if (Data.RateLimit == null && Data.RateLimitRemaining == null && Data.RateLimitReset == null)
                return;

            if (Data.Statuses != null)
            {
                Data.Statuses.RateLimit = Data.RateLimit;
                Data.Statuses.RateLimitRemaining = Data.RateLimitRemaining;
                Data.Statuses.RateLimitReset = Data.RateLimitReset;

                foreach (TwitterStatus item in Data.Statuses)
                {
                    item.RateLimit = Data.RateLimit;
                    item.RateLimitRemaining = Data.RateLimitRemaining;
                    item.RateLimitReset = Data.RateLimitReset;
                }
            }

            if (Data.Users != null)
            {
                Data.Users.RateLimit = Data.RateLimit;
                Data.Users.RateLimitRemaining = Data.RateLimitRemaining;
                Data.Users.RateLimitReset = Data.RateLimitReset;

                foreach (TwitterUser item in Data.Users)
                {
                    item.RateLimit = Data.RateLimit;
                    item.RateLimitRemaining = Data.RateLimitRemaining;
                    item.RateLimitReset = Data.RateLimitReset;
                }
            }

        }

		#region Parse Statuses
        /// <summary>
        /// Parses multiple status nodes and returns a collection of status objects.
        /// </summary>
        /// <param name="Element">The element.</param>
        /// <returns></returns>
		private static TwitterStatusCollection ParseStatuses(XmlElement Element)
		{
			TwitterStatusCollection Collection = new TwitterStatusCollection();
			foreach (XmlElement Child in Element.GetElementsByTagName("status"))
			{
				Collection.Add(ParseStatusNode(Child));
			}

			return Collection;
		}

        /// <summary>
        /// Parses a single status node and returns a status object.
        /// </summary>
        /// <param name="Element">The element.</param>
        /// <returns></returns>
		private static TwitterStatus ParseStatusNode(XmlNode Element)
		{
			TwitterStatus Status = new TwitterStatus();
            Status.IsDirectMessage = false;

			if (Element == null) return null;

			//Mon May 12 15:56:07 +0000 2008
			Status.ID = Int64.Parse(Element["id"].InnerText);
			Status.Created = ParseDateString(Element["created_at"].InnerText);
			Status.Text = Element["text"].InnerText;
			Status.Source = Element["source"].InnerText;
			Status.IsTruncated = bool.Parse(Element["truncated"].InnerText);
			if (Element["in_reply_to_status_id"].InnerText != string.Empty)
				Status.InReplyToStatusID = Int64.Parse(Element["in_reply_to_status_id"].InnerText);
			if (Element["in_reply_to_user_id"].InnerText != string.Empty)
				Status.InReplyToUserID = int.Parse(Element["in_reply_to_user_id"].InnerText);

			// Fix for Issued #4
			bool isFavorited;
			bool.TryParse(Element["favorited"].InnerText, out isFavorited);
			Status.IsFavorited = isFavorited;

			Status.TwitterUser = ParseUserNode(Element["user"]);
            if (Element["sender"] != null)
                Status.TwitterUser = ParseUserNode(Element["sender"]);
            if (Element["recipient"] != null)
                Status.Recipient = ParseUserNode(Element["recipient"]);

			return Status;
		}
		#endregion

		#region Parse DirectMessages
        /// <summary>
        /// Parses multiple direct messages and returns a collection of statuses.
        /// </summary>
        /// <param name="Element">The element.</param>
        /// <returns></returns>
		private static TwitterStatusCollection ParseDirectMessages(XmlElement Element)
		{
			TwitterStatusCollection Collection = new TwitterStatusCollection();
			foreach (XmlElement Child in Element.GetElementsByTagName("direct_message"))
			{
				Collection.Add(ParseDirectMessageNode(Child));
			}

			return Collection;
		}

        /// <summary>
        /// Parses a single direct message node and returns a status object.
        /// </summary>
        /// <param name="Element">The element.</param>
        /// <returns></returns>
		private static TwitterStatus ParseDirectMessageNode(XmlNode Element)
		{
			if (Element == null) return null;

			TwitterStatus Status = new TwitterStatus();

            Status.IsDirectMessage = true;
			Status.ID = Int64.Parse(Element["id"].InnerText);
			Status.Created = ParseDateString(Element["created_at"].InnerText);
			Status.Text = Element["text"].InnerText;

			if (Element["favorited"] != null && (Element["in_reply_to_status_id"].InnerText != string.Empty))
				Status.IsFavorited = bool.Parse(Element["favorited"].InnerText);

            Status.TwitterUser = ParseUserNode(Element["sender"]);
			Status.RecipientID = int.Parse(Element["recipient_id"].InnerText);
            Status.Recipient = ParseUserNode(Element["recipient"]);
            
			return Status;
		}
		#endregion

		#region Parse Users
        /// <summary>
        /// Parses multiple users nodes and returns a collection of user objects.
        /// </summary>
        /// <param name="Element">The element.</param>
        /// <returns></returns>
		private static TwitterUserCollection ParseUsers(XmlElement Element)
		{
			if (Element == null) return null;

			TwitterUserCollection Collection = new TwitterUserCollection();
			foreach (XmlElement Child in Element.GetElementsByTagName("user"))
			{
				Collection.Add(ParseUserNode(Child));
			}

			return Collection;
		}

        /// <summary>
        /// Parses a single user node and returns a user object.
        /// </summary>
        /// <param name="Element">The element.</param>
        /// <returns></returns>
		private static TwitterUser ParseUserNode(XmlNode Element)
		{
			if (Element == null)
				return null;

			TwitterUser User = new TwitterUser();
			User.ID = int.Parse(Element["id"].InnerText);
			User.UserName = Element["name"].InnerText;
			User.ScreenName = Element["screen_name"].InnerText;
			User.Location = Element["location"].InnerText;
            User.Uri = Element["url"].InnerText;
            User.Description = Element["description"].InnerText;
            User.CreatedAt = ParseDateString(Element["created_at"].InnerText);
            User.IsVerified = bool.Parse(Element["verified"].InnerText);
			
            // Profile information
            User.ProfileImageUri = Element["profile_image_url"].InnerText;
            User.ProfileUri = Element["url"].InnerText;
            User.ProfileBackgroundColor = ColorTranslator.FromHtml(
                String.Concat("#", Element["profile_background_color"].InnerText));
            User.ProfileTextColor = ColorTranslator.FromHtml(
                String.Concat("#", Element["profile_text_color"].InnerText));
            User.ProfileLinkColor = ColorTranslator.FromHtml(
                String.Concat("#", Element["profile_link_color"].InnerText));
            User.ProfileSidebarFillColor = ColorTranslator.FromHtml(
                String.Concat("#", Element["profile_sidebar_fill_color"].InnerText));
            User.ProfileSidebarBorderColor = ColorTranslator.FromHtml(
                String.Concat("#", Element["profile_sidebar_border_color"].InnerText));
            User.ProfileBackgroundImageUri = Element["profile_background_image_url"].InnerText;
            User.ProfileBackgroundTile = bool.Parse(Element["profile_background_tile"].InnerText);

			User.IsProtected = bool.Parse(Element["protected"].InnerText);

            int utcOffset;
            if (int.TryParse(Element["utc_offset"].InnerText, out utcOffset))
                User.UTCOffset = utcOffset;

            
            
            User.TimeZone = Element["time_zone"].InnerText;

            if (!string.IsNullOrEmpty(Element["notifications"].InnerText))
                User.Notifications = bool.Parse(Element["notifications"].InnerText);
            if (!string.IsNullOrEmpty(Element["following"].InnerText))
                User.Following = bool.Parse(Element["following"].InnerText);

			// Get the number of followers
            if (Element["followers_count"] != null)
                User.NumberOfFollowers = int.Parse(Element["followers_count"].InnerText);
            else
                User.NumberOfFollowers = -1;

			// Get the number of friends
            if (Element["friends_count"] != null)
				User.NumberOfFriends = int.Parse(Element["friends_count"].InnerText);
			else
				User.NumberOfFriends = -1;
            
            // Get the number of statuses
            if (Element["statuses_count"] != null)
                User.NumberOfStatuses = int.Parse(Element["statuses_count"].InnerText);
            else
                User.NumberOfStatuses = -1;

            // If there is a status, parse it
			if (Element["status"] != null)
				User.Status = ParseStatusNode(Element["status"]);

			return User;
		}
		#endregion

        /// <summary>
        /// Parses a date string into a strongly typed DateTime object.
        /// </summary>
        /// <param name="DateString">The date string.</param>
        /// <returns></returns>
        /// <remarks>Format example: Wed Apr 08 20:30:04 +0000 2009</remarks>
		private static DateTime ParseDateString(string DateString)
		{
			Regex re = new Regex(@"(?<DayName>[^ ]+) (?<MonthName>[^ ]+) (?<Day>[^ ]{1,2}) (?<Hour>[0-9]{1,2}):(?<Minute>[0-9]{1,2}):(?<Second>[0-9]{1,2}) (?<TimeZone>[+-][0-9]{4}) (?<Year>[0-9]{4})");
			Match CreatedAt = re.Match(DateString);
			DateTime parsedDate = DateTime.Parse(
				string.Format(
					"{0} {1} {2} {3}:{4}:{5}",
					CreatedAt.Groups["MonthName"].Value,
					CreatedAt.Groups["Day"].Value,
					CreatedAt.Groups["Year"].Value,
					CreatedAt.Groups["Hour"].Value,
					CreatedAt.Groups["Minute"].Value,
					CreatedAt.Groups["Second"].Value));

			return parsedDate;
		}

        /// <summary>
        /// Returns the value of the element within the provided XmlNode, if the element exists. Returns String.Empty if not.
        /// </summary>
        /// <param name="Element">The element.</param>
        /// <param name="ElementName">Name of the element.</param>
        /// <returns></returns>
        private static string ElementValueIfExists(XmlNode Node, string ElementName)
        {
            if (!Node.HasChildNodes || Node[ElementName] == null)
                return string.Empty;

            return Node[ElementName].Value;
        }
	}
}
