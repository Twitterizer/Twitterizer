namespace Twitterizer2.TestCases
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
            ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap
            {
                ExeConfigFilename = Path.Combine(Environment.CurrentDirectory, "TestSettings.config")
            };

            System.Configuration.Configuration config = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);

            OAuthTokens tokens = new OAuthTokens();
            tokens.AccessToken = config.AppSettings.Settings["AccessToken"].Value;
            tokens.AccessTokenSecret = config.AppSettings.Settings["AccessTokenSecret"].Value;
            tokens.ConsumerKey = config.AppSettings.Settings["ConsumerKey"].Value;
            tokens.ConsumerSecret = config.AppSettings.Settings["ConsumerSecret"].Value;

            return tokens;
        }
    }
}
