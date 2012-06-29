namespace Twitterizer2.MSTestCases
{
    using Twitterizer;
    using System.Configuration;
    using System;
    using System.IO;

    public static class Configuration
    {
        /// <summary>
        /// Gets the tokens.
        /// </summary>
        /// <returns></returns>
        public static OAuthTokens GetTokens()
        {
            OAuthTokens tokens = new OAuthTokens();
            tokens.AccessToken = ConfigurationManager.AppSettings["AccessToken"];
            tokens.AccessTokenSecret = ConfigurationManager.AppSettings["AccessTokenSecret"];
            tokens.ConsumerKey = ConfigurationManager.AppSettings["ConsumerKey"];
            tokens.ConsumerSecret = ConfigurationManager.AppSettings["ConsumerSecret"];

            return tokens;
        }
    }
}