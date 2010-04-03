using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Twitterizer
{
    public static class TwitterSearch
    {
        public static TwitterSearchResultCollection Search(string query)
        {
            Commands.SearchCommand command = new Twitterizer.Commands.SearchCommand(null, query);

            TwitterSearchResultCollection results =
                Core.CommandPerformer<TwitterSearchResultWrapper>.PerformAction(command).Results;

            return results;
        }
    }
}
