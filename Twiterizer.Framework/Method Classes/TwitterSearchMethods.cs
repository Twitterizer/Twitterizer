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
        TweetedFromUser,
        TweetReferencesUser
    }
    public class TwitterSearchMethods
    {
        public void SetParameters(TwitterParameters parameters)
        {
            throw new NotImplementedException();
        }
        public void ClearParameters(TwitterParameters parameters)
        {
            throw new NotImplementedException();
        }

        public IList<TwitterSearchResult> Search(string text)
        {
            throw new NotImplementedException();
        }

        public IList<TwitterSearchResult> Search(TwitterSearchTerm searchTerm)
        {
            throw new NotImplementedException();
        }

        public IList<TwitterSearchResult> Search(IList<TwitterSearchTerm> searchTerms)
        {
            throw new NotImplementedException();
        }
    }



}
