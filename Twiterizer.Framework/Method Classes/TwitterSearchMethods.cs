using System;
using System.Collections.Generic;
using System.Text;
using Twitterizer.Framework.Data_Transfer_Objects;

namespace Twitterizer.Framework.Method_Classes
{
        public enum TwitterSearchType
    {
        TextSearch,
        TweetedToUser,
        TweetedFromUser
    }
    public class TwitterSearchMethods
    {
        private readonly string _userName;
        private readonly string _password;
        private TwitterParameters _parameters;

        public TwitterSearchMethods(string userName, string password)
        {
            _userName = userName;
            _password = password;
        }


        public void SetParameters(TwitterParameters parameters)
        {
            if (parameters == null) throw new ArgumentNullException("parameters");
            _parameters = parameters;
        }

        public void ClearParameters()
        {
            _parameters = null;
        }

        public IList<TwitterSearchResult> Search(string text)
        {
            TwitterSearchTerm term = new TwitterSearchTerm(TwitterSearchType.TextSearch, text);
            return Search(term);
        }

        public IList<TwitterSearchResult> Search(TwitterSearchTerm searchTerm)
        {
            IList<TwitterSearchTerm> terms = new List<TwitterSearchTerm>();
            terms.Add(searchTerm);
            return Search(terms);
        }

        public IList<TwitterSearchResult> Search(IList<TwitterSearchTerm> searchTerms)
        {
            TwitterRequest Request = new TwitterRequest();
            TwitterRequestData Data = new TwitterRequestData();
            Data.UserName = _userName;
            Data.Password = _password;

            string baseURL = "http://search.twitter.com/search.json";


            string actionUri = (_parameters == null ? baseURL : _parameters.BuildActionUri(baseURL));

            foreach (TwitterSearchTerm t in searchTerms)
                actionUri = string.Format("{0}&{1}", actionUri, t.ToWebString());

            Data.ActionUri = new Uri(actionUri);
            Data.IsJSON = true;

            Data = Request.PerformWebRequest(Data);

            return Data.SearchResults;
        }
    }



}
