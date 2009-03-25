namespace Twitterizer.Framework.Method_Classes
{
    public class TwitterSearchTerm
    {
        public TwitterSearchTerm(TwitterSearchType type, string searchText )
        {
            SearchType = type;
            SearchText = searchText;
        }
        public TwitterSearchType SearchType { get; private set; }
        public string SearchText { get; private set; }
    }
}