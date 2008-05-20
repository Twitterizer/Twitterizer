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

        public bool Destroy(int ID)
        {
            TwitterRequest Request = new TwitterRequest();
            TwitterRequestData Data = new TwitterRequestData();
            Data.UserName = userName;
            Data.Password = password;

            Data.ActionUri = new Uri(
                string.Format(ConfigurationManager.AppSettings["Twitterizer.Framework.Destroy"],
                  ID));

            Data = Request.PerformWebRequest(Data);

            return true;
        }

        public TwitterStatus Show(int ID)
        {
            TwitterRequest Request = new TwitterRequest();
            TwitterRequestData Data = new TwitterRequestData();
            Data.UserName = userName;
            Data.Password = password;

            Data.ActionUri = new Uri(
                string.Format(ConfigurationManager.AppSettings["Twitterizer.Framework.Show"],
                  ID));

            Data = Request.PerformWebRequest(Data);

            return Data.Statuses[0];
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

        public TwitterUserCollection Friends()
        {
            TwitterRequest Request = new TwitterRequest();
            TwitterRequestData Data = new TwitterRequestData();
            Data.UserName = userName;
            Data.Password = password;
            Data.ActionUri = new Uri(ConfigurationManager.AppSettings["Twitterizer.Framework.Friends"]);

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
            TwitterRequest Request = new TwitterRequest();
            TwitterRequestData Data = new TwitterRequestData();
            Data.UserName = userName;
            Data.Password = password;

            Data.ActionUri = new Uri(ConfigurationManager.AppSettings["Twitterizer.Framework.Followers"]);

            Data = Request.PerformWebRequest(Data);

            return Data.Users;
        }
    }
}
