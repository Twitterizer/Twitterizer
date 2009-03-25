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
using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.Xml;
using System.IO;
using Newtonsoft.Json.Linq;
using Twitterizer.Framework.Data_Transfer_Objects;
using JsonSerializer=Newtonsoft.Json.JsonSerializer;

namespace Twitterizer.Framework
{
	internal class TwitterRequest
	{
		public TwitterRequestData PerformWebRequest(TwitterRequestData Data)
		{
			PerformWebRequest(Data, "POST");

			return (Data);

		}

		public TwitterRequestData PerformWebRequest(TwitterRequestData Data, string HTTPMethod)
		{
			HttpWebRequest Request = (HttpWebRequest)WebRequest.Create(Data.ActionUri);


			Request.Method = HTTPMethod;

			StreamReader readStream;

			// Some limitations
			Request.MaximumAutomaticRedirections = 4;
			Request.MaximumResponseHeadersLength = 4;
			Request.ContentLength = 0;

			// Set our credentials
			Request.Credentials = new NetworkCredential(Data.UserName, Data.Password);

			HttpWebResponse Response;

			// Get the respon
			try
			{
				Response = (HttpWebResponse)Request.GetResponse();

				// Get the stream associated with the response.
				Stream receiveStream = Response.GetResponseStream();

				// Pipes the stream to a higher level stream reader with the required encoding format. 
				readStream = new StreamReader(receiveStream, Encoding.UTF8);

				Data.Response = readStream.ReadToEnd();
				Data = ParseResponseData(Data);

				Response.Close();
				readStream.Close();
			}
			catch (Exception ex)
			{
				throw new TwitterizerException(ex.Message, Data, ex);
			}

			return Data;
		}

		private TwitterRequestData ParseResponseData(TwitterRequestData Data)
		{
			if (Data == null || Data.Response == string.Empty)
				return null;

            if (Data.IsJSON)
                ParseJSONResponseData(Data);
            else
                ParseXmlResponseData(Data);

		    return Data;
		}

        /// <summary>
        /// Simple method which uses JSON.net's deserialization to convert the JSON response
        /// into a collection of our TwitterSearchResult objects
        /// </summary>
        /// <param name="data"></param>
	    private void ParseJSONResponseData(TwitterRequestData data)
	    {
            //JSON = JavaScript Object Notation
            try
            {
                JsonSerializer serializer = new JsonSerializer();
                JObject jsonObject = JObject.Parse(data.Response);

                //LINQ to JSON.
                var results = from o in jsonObject["results"].Children()
                              select (TwitterSearchResult)serializer.Deserialize(o.CreateReader(), typeof (TwitterSearchResult));
                data.SearchResults = results.ToList();
            }
            catch(Exception ex)
            {
                throw new TwitterizerException("Unable to Parse Search Results", data, ex);
            }

            /*
             * Sample twitter search results json:
             * [results] => Array
        (
            [0] => stdClass Object
                (
                    [text] => I read everyone's 'tweets' about #Fallout3 dlc 'The Pitt', and calm my panic - my game sits dusty on the shelf, unplayed - must play soon!
                    [to_user_id] => 
                    [from_user] => iLoxy
                    [id] => 1388505191
                    [from_user_id] => 3131419
                    [iso_language_code] => en
                    [source] => <a href="http://twitterfon.net/">TwitterFon</a>
                    [profile_image_url] => http://s3.amazonaws.com/twitter_production/profile_images/108656830/iLoxy_tile01_normal.png
                    [created_at] => Wed, 25 Mar 2009 15:31:07 +0000
                )

            [1] => stdClass Object
                (
                    [text] => Wishing I had time to play with the G.E.C.K., for Fallout3.
                    [to_user_id] => 
                    [from_user] => jeff_henderson
                    [id] => 1388495046
                    [from_user_id] => 3469262
                    [iso_language_code] => en
                    [source] => <a href="http://help.twitter.com/index.php?pg=kb.page&id=75">txt</a>
                    [profile_image_url] => http://s3.amazonaws.com/twitter_production/profile_images/70232418/jh_normal.JPG
                    [created_at] => Wed, 25 Mar 2009 15:29:16 +0000
                )
           )
             */


        }

	    private void ParseXmlResponseData(TwitterRequestData Data)
	    {
            try
            {
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
            }
            catch (Exception ex)
            {
                throw new TwitterizerException("Error Parsing Twitter Response.", Data, ex);
            }
	    }

	    #region Parse Statuses
		private TwitterStatusCollection ParseStatuses(XmlElement Element)
		{
			TwitterStatusCollection Collection = new TwitterStatusCollection();
			foreach (XmlElement Child in Element.GetElementsByTagName("status"))
			{
				Collection.Add(ParseStatusNode(Child));
			}

			return Collection;
		}

		private TwitterStatus ParseStatusNode(XmlNode Element)
		{
			TwitterStatus Status = new TwitterStatus();

			if (Element == null) return null;

			//Mon May 12 15:56:07 +0000 2008
			Status.ID = int.Parse(Element["id"].InnerText);
			Status.Created = ParseDateString(Element["created_at"].InnerText);
			Status.Text = Element["text"].InnerText;
			Status.Source = Element["source"].InnerText;
			Status.IsTruncated = bool.Parse(Element["truncated"].InnerText);
			if (Element["in_reply_to_status_id"].InnerText != string.Empty)
				Status.InReplyToStatusID = int.Parse(Element["in_reply_to_status_id"].InnerText);
			if (Element["in_reply_to_user_id"].InnerText != string.Empty)
				Status.InReplyToUserID = int.Parse(Element["in_reply_to_user_id"].InnerText);

			// Fix for Issued #4
			bool isFavorited;
			bool.TryParse(Element["favorited"].InnerText, out isFavorited);
			Status.IsFavorited = isFavorited;

			Status.TwitterUser = ParseUserNode(Element["user"]);

			return Status;
		}
		#endregion

		#region Parse DirectMessages
		private static TwitterStatusCollection ParseDirectMessages(XmlElement Element)
		{
			TwitterStatusCollection Collection = new TwitterStatusCollection();
			foreach (XmlElement Child in Element.GetElementsByTagName("direct_message"))
			{
				Collection.Add(ParseDirectMessageNode(Child));
			}

			return Collection;
		}

		private static TwitterStatus ParseDirectMessageNode(XmlNode Element)
		{
			if (Element == null) return null;

			TwitterStatus Status = new TwitterStatus();

			Status.ID = int.Parse(Element["id"].InnerText);
			Status.Created = ParseDateString(Element["created_at"].InnerText);
			Status.Text = Element["text"].InnerText;

			if (Element["favorited"] != null && (Element["in_reply_to_status_id"].InnerText != string.Empty))
				Status.IsFavorited = bool.Parse(Element["favorited"].InnerText);

			Status.TwitterUser = new TwitterUser();
			Status.TwitterUser.ScreenName = Element["sender_screen_name"].InnerText;
			Status.TwitterUser.ID = int.Parse(Element["sender_id"].InnerText);
			Status.RecipientID = int.Parse(Element["recipient_id"].InnerText);




			return Status;
		}
		#endregion

		#region Parse Users
		private TwitterUserCollection ParseUsers(XmlElement Element)
		{
			if (Element == null) return null;

			TwitterUserCollection Collection = new TwitterUserCollection();
			foreach (XmlElement Child in Element.GetElementsByTagName("user"))
			{
				Collection.Add(ParseUserNode(Child));
			}

			return Collection;
		}

		private TwitterUser ParseUserNode(XmlNode Element)
		{
			if (Element == null)
				return null;

			TwitterUser User = new TwitterUser();
			User.ID = int.Parse(Element["id"].InnerText);
			User.UserName = Element["name"].InnerText;
			User.ScreenName = Element["screen_name"].InnerText;
			User.Location = Element["location"].InnerText;
			User.Description = Element["description"].InnerText;
			User.IsProtected = bool.Parse(Element["protected"].InnerText);
			User.NumberOfFollowers = int.Parse(Element["followers_count"].InnerText);

			Uri ProfileImageUri;
			if (Uri.TryCreate(Element["profile_image_url"].InnerText,
			UriKind.RelativeOrAbsolute, out ProfileImageUri))
				User.ProfileImageUri = ProfileImageUri;

			Uri ProfileUri;
			if (Uri.TryCreate(Element["url"].InnerText, UriKind.RelativeOrAbsolute, out ProfileUri))
				User.ProfileUri = ProfileUri;

			if (Element["friends_count"] != null)
				User.Friends_count = int.Parse(Element["friends_count"].InnerText);
			else
				User.Friends_count = -1;        // flag that we don't know, which is different than having zero friends

			if (Element["status"] != null)
				User.Status = ParseStatusNode(Element["status"]);

			return User;
		}
		#endregion

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
	}
}
