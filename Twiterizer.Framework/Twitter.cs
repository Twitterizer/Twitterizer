using System;
using System.Web;

namespace Twitterizer.Framework
{
    public class Twitter
    {
        private readonly string userName;
        private readonly string password;

        public Twitter(string UserName, string Password)
        {
            userName = UserName;
            password = Password;
        }

        public TwitterStatus Update(string Status)
        {
            TwitterRequest Request = new TwitterRequest();
            TwitterRequestData Data = new TwitterRequestData();
            Data.UserName = userName;
            Data.Password = password;
            
            Data.ActionUri = new Uri(
                string.Format("http://twitter.com/statuses/update.xml?status={0}", HttpUtility.UrlEncode(Status)));

            Data = Request.PerformWebRequest(Data);

            return Data.Statuses[0];
        }

        public void Destroy(int ID)
        {
            TwitterRequest Request = new TwitterRequest();
            TwitterRequestData Data = new TwitterRequestData();
            Data.UserName = userName;
            Data.Password = password;

            Data.ActionUri = new Uri(
                string.Format("http://twitter.com/statuses/destroy/{0}.xml", ID));

            Request.PerformWebRequest(Data);
        }

       
        public TwitterUser Show(string id_or_ScreenName)
        {
            TwitterRequest Request = new TwitterRequest();
            TwitterRequestData Data = new TwitterRequestData();
            Data.UserName = userName;
            Data.Password = password;

            Data.ActionUri = new Uri(
                string.Format("http://twitter.com/users/show/{0}.xml", id_or_ScreenName));

            Data = Request.PerformWebRequest(Data, "GET");

            return Data.Users[0];
        }

        public TwitterStatusCollection FriendsTimeline()
        {
            TwitterRequest Request = new TwitterRequest();
            TwitterRequestData Data = new TwitterRequestData();
            Data.UserName = userName;
            Data.Password = password;

            Data.ActionUri = new Uri("http://twitter.com/statuses/friends_timeline.xml");

            Data = Request.PerformWebRequest(Data);

            return Data.Statuses;
        }

        public TwitterStatusCollection DirectMessages(ulong since_id)
        {
            TwitterRequest Request = new TwitterRequest();
            TwitterRequestData Data = new TwitterRequestData();
            Data.UserName = userName;
            Data.Password = password;

            Data.ActionUri = new Uri(
                string.Format("http://twitter.com/direct_messages.xml?since_id={0}", since_id));

            Data = Request.PerformWebRequest(Data,"GET");

            return Data.Statuses;
        }

        public TwitterStatusCollection DirectMessagesSent()
        {
            return DirectMessagesSent(0);
        }

        public TwitterStatusCollection DirectMessagesSent(ulong since_id)
        {
            TwitterRequest Request = new TwitterRequest();
            TwitterRequestData Data = new TwitterRequestData();
            Data.UserName = userName;
            Data.Password = password;

            Data.ActionUri = new Uri(
                string.Format("http://twitter.com/direct_messages/sent.xml?since_id={0}", since_id));

            Data = Request.PerformWebRequest(Data);

            return Data.Statuses;
        }

        public TwitterStatusCollection Archive()
        {
            return Archive(0);
        }
        public TwitterStatusCollection Archive(ulong since_id)
        {
            TwitterRequest Request = new TwitterRequest();
            TwitterRequestData Data = new TwitterRequestData();
            Data.UserName = userName;
            Data.Password = password;

            Data.ActionUri = new Uri(
                string.Format("http://twitter.com/account/archive.xml?since_id={0}", since_id));

            Data = Request.PerformWebRequest(Data);

            return Data.Statuses;
        }

        public TwitterUserCollection Friends()
        {
            return (Friends(1));
        }

        public TwitterUserCollection Friends(int page)
        {
            // page 0 == page 1 is the start
            TwitterRequest Request = new TwitterRequest();
            TwitterRequestData Data = new TwitterRequestData();
            Data.UserName = userName;
            Data.Password = password;
           
            Data.ActionUri = new Uri(
                    string.Format("http://twitter.com/statuses/friends.xml?page={0}", page));

            Data = Request.PerformWebRequest(Data);

            return Data.Users;
        }

        public TwitterStatusCollection UserTimeline()
        {
            TwitterRequest Request = new TwitterRequest();
            TwitterRequestData Data = new TwitterRequestData();

            Data.ActionUri = new Uri("http://twitter.com/statuses/user_timeline.xml");

            Data = Request.PerformWebRequest(Data);

            return Data.Statuses;
        }

        public TwitterStatusCollection PublicTimeline()
        {
            TwitterRequest Request = new TwitterRequest();
            TwitterRequestData Data = new TwitterRequestData();

            Data.ActionUri = new Uri("http://twitter.com/statuses/public_timeline.xml");

            Data = Request.PerformWebRequest(Data);

            return Data.Statuses;
        }

        public TwitterStatusCollection Replies()
        {
            TwitterRequest Request = new TwitterRequest();
            TwitterRequestData Data = new TwitterRequestData();
            Data.UserName = userName;
            Data.Password = password;

            Data.ActionUri = new Uri("http://twitter.com/statuses/replies.xml");

            Data = Request.PerformWebRequest(Data);

            return Data.Statuses;
        }

        public TwitterUserCollection Followers()
        {
            return (Followers(0));
        }

        public TwitterUserCollection Followers(int page)
        {
            TwitterRequest Request = new TwitterRequest();
            TwitterRequestData Data = new TwitterRequestData();
            Data.UserName = userName;
            Data.Password = password;

            
             Data.ActionUri = new Uri(
                    string.Format("http://twitter.com/statuses/followers.xml?page={0}", page));

            Data = Request.PerformWebRequest(Data);

            return Data.Users;
        }
    }
}
