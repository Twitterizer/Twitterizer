namespace Twitterizer.Entities
{
    using System;

    [Serializable]
    public class TwitterEntity
    {
        internal TwitterEntity()
        {
        }

        public long StartIndex { get; set; }
        public long EndIndex { get; set; }
    }
}
