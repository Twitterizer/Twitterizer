using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Runtime.Serialization.Json;
using System.IO;

namespace Twitterizer.Core
{
    public abstract class BaseCommand<T>
    {
        public bool IsValid { get; set; }
        public string Uri { get; set; }
        public string Method { get; set; }
        public string OAuthAccessToken { get; set; }
        public Dictionary<string,string> RequestParameters { get; set; }

        /// <summary>
        /// Inits this instance.
        /// </summary>
        public abstract void Init();

        /// <summary>
        /// Validates this instance.
        /// </summary>
        public abstract void Validate();

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseCommand&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="Method">The method.</param>
        /// <param name="Uri">The URI.</param>
        public BaseCommand(string Method, string Uri)
            : this(Method, Uri, string.Empty)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseCommand&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="Method">The method.</param>
        /// <param name="Uri">The URI.</param>
        /// <param name="OAuthAccessToken">The O auth access token.</param>
        public BaseCommand(string Method, string Uri, string OAuthAccessToken)
        {
            this.Uri = Uri;
            this.Method = Method;
            this.OAuthAccessToken = OAuthAccessToken;
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <returns></returns>
        public T ExecuteCommand()
        {
            DataContractJsonSerializer ds = new DataContractJsonSerializer(typeof(T));
            T resultObject = default(T);
            try
            {
                Stream responseStream = BuildRequest().GetResponse().GetResponseStream();
                resultObject = (T)ds.ReadObject(responseStream);
                responseStream.Close();
            }
            catch (WebException wex)
            {
                throw wex.ToTwitterizerException();
            }
            

            return resultObject;
        }
        
        /// <summary>
        /// Builds the request.
        /// </summary>
        private HttpWebRequest BuildRequest()
        {
            HttpWebRequest Request = (HttpWebRequest)HttpWebRequest.Create(BuildRequestUri());
            Request.Method = Method;
            Request.MaximumAutomaticRedirections = 4;
            Request.MaximumResponseHeadersLength = 4;
            Request.ContentLength = 0;

            return Request;
        }

        /// <summary>
        /// Builds the request URI.
        /// </summary>
        /// <param name="requestUri">The request URI.</param>
        /// <returns></returns>
        private string BuildRequestUri()
        {
            StringBuilder newRequestAddress = new StringBuilder(this.Uri);

            if (!this.Uri.Contains('?'))
                newRequestAddress.Append("?");
            else
                newRequestAddress.Append("&");

            foreach (KeyValuePair<string, string> pair in this.RequestParameters)
                newRequestAddress.AppendFormat("{0}={1}&", pair.Key, pair.Value);

            // Trim off the last ampersand
            newRequestAddress.Remove(newRequestAddress.Length - 1, 1);

            return newRequestAddress.ToString();
        }
    }
}
