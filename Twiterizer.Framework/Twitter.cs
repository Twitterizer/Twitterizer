using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Web;

namespace Twitterizer.Framework
{
    public class Twitter
    {
        private string userName;
        private string password;

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
                string.Format(ConfigurationManager.AppSettings["Twitterizer.Framework.Update"],
                  HttpUtility.UrlEncode(Status)));

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
                string.Format(ConfigurationManager.AppSettings["Twitterizer.Framework.Destroy"],
                  ID));

            Data = Request.PerformWebRequest(Data);
        }

       
        public TwitterUser Show(string id_or_ScreenName)
        {
            TwitterRequest Request = new TwitterRequest();
            TwitterRequestData Data = new TwitterRequestData();
            Data.UserName = userName;
            Data.Password = password;

            Data.ActionUri = new Uri(
                string.Format(ConfigurationManager.AppSettings["Twitterizer.Framework.Show"],
                  id_or_ScreenName));

            Data = Request.PerformWebRequest(Data, "GET");

            return Data.Users[0];
        }

        public TwitterStatusCollection FriendsTimeline()
        {
            TwitterRequest Request = new TwitterRequest();
            TwitterRequestData Data = new TwitterRequestData();
            Data.UserName = userName;
            Data.Password = password;

            Data.ActionUri = new Uri(ConfigurationManager.AppSettings["Twitterizer.Framework.FriendsTimeLine"]);

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
                string.Format(ConfigurationManager.AppSettings["Twitterizer.Framework.Messages"],
                since_id));

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
                string.Format(ConfigurationManager.AppSettings["Twitterizer.Framework.MessagesSent"],
                since_id));

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
                string.Format(ConfigurationManager.AppSettings["Twitterizer.Framework.Archive"],
                since_id));

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
                    string.Format(ConfigurationManager.AppSettings["Twitterizer.Framework.Friends"], page));

            Data = Request.PerformWebRequest(Data);

            return Data.Users;
        }

        public TwitterStatusCollection UserTimeline()
        {
            TwitterRequest Request = new TwitterRequest();
            TwitterRequestData Data = new TwitterRequestData();

            Data.ActionUri = new Uri(ConfigurationManager.AppSettings["Twitterizer.Framework.UserTimeLine"]);

            Data = Request.PerformWebRequest(Data);

            return Data.Statuses;
        }

        public TwitterStatusCollection PublicTimeline()
        {
            TwitterRequest Request = new TwitterRequest();
            TwitterRequestData Data = new TwitterRequestData();

            Data.ActionUri = new Uri(ConfigurationManager.AppSettings["Twitterizer.Framework.PublicTimeLine"]);

            Data = Request.PerformWebRequest(Data);

            return Data.Statuses;
        }

        public TwitterStatusCollection Replies()
        {
            TwitterRequest Request = new TwitterRequest();
            TwitterRequestData Data = new TwitterRequestData();
            Data.UserName = userName;
            Data.Password = password;

            Data.ActionUri = new Uri(ConfigurationManager.AppSettings["Twitterizer.Framework.Replies"]);

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
                    string.Format(ConfigurationManager.AppSettings["Twitterizer.Framework.Followers"], page));

            Data = Request.PerformWebRequest(Data);

            return Data.Users;
        }
    }
}
