using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Twitterizer;
using Twitterizer.Streaming;
using System.Threading;
using System.Diagnostics;
using NUnit.Framework;

namespace Twitterizer2.TestCases
{
    [TestFixture]
    public class TwitterStreamTests
    {
        TwitterStream stream;
        int Countdown = 0;

        [Test]
        [Category("Streaming")]
        public void FilterTest()
        {
            Countdown = 50;

            stream = new TwitterStream("XXX", "XXX");
            stream.OnStatus += new TwitterStatusReceivedHandler(stream_OnStatus);

            FilterStreamOptions options = new FilterStreamOptions();
            options.Track.Add("twit_er_izer");
            options.Track.Add("twitterizer");
            
            stream.StartFilterStream(options);
            

            System.Threading.Thread.Sleep(10000);
        }

        void stream_OnStatus(TwitterStatus status)
        {
            if (Countdown == 0)
                stream.EndStream();
            else
                Countdown--;

            Console.WriteLine(string.Format("@{0}: {1}", status.User.ScreenName, status.Text));
        }
    }
}
