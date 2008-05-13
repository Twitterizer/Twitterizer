using System;
using System.Collections.Generic;
using System.Text;

namespace Twitterizer.Framework
{
    public class TwitterizerException : Exception
    {
        private TwitterRequestData requestData;
        public TwitterRequestData RequestData
        {
            get { return requestData; }
            set { requestData = value; }
        }

        public TwitterizerException(string Message, TwitterRequestData RequestData)
            : base(Message)
        {
            requestData = RequestData;
        }

        public TwitterizerException(string Message, TwitterRequestData RequestData, Exception InnerException)
            : base(Message, InnerException)
        {
            requestData = RequestData;
        }
    }
}
