using System;
using System.Collections.Generic;
using NUnit.Framework;
using Twitterizer;
using Twitterizer.Streaming;

namespace Twitterizer2.TestCases
{
    [TestFixture]
    public class TwitterStreamTests
    {
        TwitterStream stream;

        [Test]
        [Category("Streaming")]
        public void FilterTest()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            stream = new TwitterStream(tokens);
            stream.OnStatusReceived += new TwitterStatusReceivedHandler(stream_OnStatus);

            FilterStreamOptions options = new FilterStreamOptions();
            options.Track.Add("twit_er_izer");
            options.Track.Add("twitterizer");
            
            stream.StartFilterStream(options);

            for (int i = 0; i < 1000; i++)
            {
                System.Threading.Thread.Sleep(100);    
            }
            
            stream.EndStream();
        }

        void stream_OnStatus(TwitterStatus status)
        {
            Console.WriteLine(string.Format("Created: @{0}: {1}", status.User.ScreenName, status.Text));
        }

        void stream_OnStatusDeleted(TwitterStatus status)
        {
            Console.WriteLine(string.Format("Deleted: id = {0}", status.Id));
        }

        void stream_OnFriendsReceived(List<decimal> friendIds)
        {
            foreach (decimal id in friendIds)
            {
                Console.WriteLine(string.Format("Friend: {0}", id));
            }
        }

        [Test]
        [Category("Streaming")]
        public void UserStreamTest()
        {
            OAuthTokens tokens = Configuration.GetTokens();
            stream = new TwitterStream(tokens);
            stream.OnStatusReceived += new TwitterStatusReceivedHandler(stream_OnStatus);
            stream.OnFriendsReceived += new TwitterFriendsReceivedHandler(stream_OnFriendsReceived);
            stream.OnStatusDeleted +=new TwitterStatusDeletedHandler(stream_OnStatusDeleted);

            stream.StartUserStream();

            for (int i = 0; i < 1000; i++)
            {
                System.Threading.Thread.Sleep(100);
            }

            stream.EndStream();
        }
    }
}
