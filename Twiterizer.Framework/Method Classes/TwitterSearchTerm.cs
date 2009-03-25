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


        public string ToWebString()
        {
            return string.Format("q={0}{1}", TypeParse(SearchType), SearchText);
        }

        private string TypeParse(TwitterSearchType type)
        {
            switch (type)
            {
                case TwitterSearchType.TextSearch:
                    return string.Empty;
                case TwitterSearchType.TweetedFromUser:
                    return "from";
                case TwitterSearchType.TweetedToUser:
                    return "to";
            }
            return string.Empty;
        }
    }
}