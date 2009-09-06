using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace Twitterizer
{
    public class TwitterizerException : WebException
    {
        /// <summary>
        /// Gets the response that the remote host returned.
        /// </summary>
        /// <value></value>
        /// <returns>If a response is available from the Internet resource, a <see cref="T:System.Net.WebResponse"/> instance that contains the error response from an Internet resource; otherwise, null.</returns>
        new public WebResponse Response
        {
            get
            {
                if (this.InnerException == null)
                    return null;

                return (this.InnerException as WebException).Response;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TwitterizerException"/> class.
        /// </summary>
        public TwitterizerException()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TwitterizerException"/> class.
        /// </summary>
        /// <param name="wex">The wex.</param>
        public TwitterizerException(WebException wex)
            : base(wex.Message, wex)
        {

        }
    }
}
