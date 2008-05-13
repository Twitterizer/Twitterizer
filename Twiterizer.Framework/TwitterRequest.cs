using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Xml;
using System.Globalization;

namespace Twitterizer.Framework
{
    public class TwitterRequest
    {
        public TwitterRequestData PerformWebRequest(TwitterRequestData Data)
        {
            HttpWebRequest Request = (HttpWebRequest)WebRequest.Create(Data.ActionUri);
            Request.Method = "POST";

            Stream receiveStream;
            StreamReader readStream;

            // Some limitations
            Request.MaximumAutomaticRedirections = 4;
            Request.MaximumResponseHeadersLength = 4;

            // Set our credentials
            Request.Credentials = new NetworkCredential(Data.UserName, Data.Password);

            HttpWebResponse Response = null;

            // Get the respon
            try
            {
                Response = (HttpWebResponse)Request.GetResponse();

                // Get the stream associated with the response.
                receiveStream = Response.GetResponseStream();

                // Pipes the stream to a higher level stream reader with the required encoding format. 
                readStream = new StreamReader(receiveStream, Encoding.UTF8);

                Data.Response = readStream.ReadToEnd();
                Data.Statuses = ParseResponseData(Data);

                Response.Close();
                readStream.Close();
            }
            catch (WebException wex)
            {
                Data.ResponseException = wex;
            }

            return Data;
        }

        private TwitterStatusCollection ParseResponseData(TwitterRequestData Data)
        {
            if (Data == null || Data.Response == string.Empty)
                return null;

            TwitterStatusCollection Collection = new TwitterStatusCollection();

            XmlDocument ResultXmlDocument = new XmlDocument();
            ResultXmlDocument.LoadXml(Data.Response);

            switch (ResultXmlDocument.DocumentElement.Name.ToLower())
            {
                case "status":
                    Collection.Add(ParseStatusNode(ResultXmlDocument.DocumentElement));
                    break;
            }

            return Collection;
        }

        private TwitterStatus ParseStatusNode(XmlElement Element)
        {
            TwitterStatus Status = new TwitterStatus();

            //Mon May 12 15:56:07 +0000 2008

            Status.Created = DateTime.ParseExact(Element["created_at"].InnerText, "ddd MMMM dd hh:mm:ss K yyyy", new CultureInfo("en-US"));

            return Status;
        }
    }
}
